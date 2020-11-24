// <copyright file="UserStatisticsController.cs" company="Infotecs">
// Copyright (c) Infotecs. All rights reserved.
// </copyright>

using System.Collections.Generic;
using Mapster;
using Microsoft.AspNetCore.Mvc;
using Serilog;
using Infotecs.WebApi.Models;
using Infotecs.WebApi.Services;
using WebApi.Repositories;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;

namespace Infotecs.WebApi.Controllers
{
    /// <summary>
    /// Класс контроллера для обработки REST запросов и 
    /// взаимодействия с пользовательской статистикой.
    /// </summary>
    [Route("api/statistics/[controller]")]
    [ApiController]
    public class UserStatisticsController : Controller
    {
        private readonly StatisticsService statisticsService = null;
        IHubContext<WebApiHub> hubContext;

        /// <summary>
        /// Конструктор для привязки системы логирования и базы данных.
        /// </summary>
        /// <param name="logger">Интерфейс системы логирования.</param>
        /// <param name="repository">Интерфейс базы данных.</param>
        public UserStatisticsController(ILogger logger, IUnitOfWork repository, IHubContext<WebApiHub> hubContext)
        {
            statisticsService = new StatisticsService(repository, logger);

            this.hubContext = hubContext;
        }

        /// <summary>
        /// Запрос списка всех статистик.
        /// </summary>
        /// <returns>Список всех статиситк.</returns>
        [HttpGet]
        public async Task<List<UserStatisticsDTO>> GetAsync()
        {
            List<UserStatisticsDTO> userStatisticsDTOS = (await statisticsService.GetStatisticsAsync()).Adapt<List<UserStatisticsDTO>>();

            if (userStatisticsDTOS != null)
            {
                foreach (var statistics in userStatisticsDTOS)
                {
                    statistics.EventsDTO = (await statisticsService.GetStatisticsAsync(statistics.ID)).Events.Adapt<List<EventsDTO>>();
                }

                foreach (var statistics in userStatisticsDTOS)
                {
                    System.Console.WriteLine(">>" + statistics.ID);
                }
            }

            return userStatisticsDTOS;
        }

        /// <summary>
        /// Запрос списка всех статистик.
        /// </summary>
        /// <param name="ID">ID пользователя для получаемой статистики.</param>
        /// <returns>Список всех статиситк.</returns>
        [HttpGet("{ID}")]
        public async Task<UserStatisticsDTO> GetAsync(string ID)
        {
            UserStatisticsDTO userStatisticsDTO = (await statisticsService.GetStatisticsAsync(ID)).Adapt<UserStatisticsDTO>();
            if (userStatisticsDTO != null)
            {
                userStatisticsDTO.EventsDTO = (await statisticsService.GetStatisticsAsync(ID)).Events.Adapt<List<EventsDTO>>();
            }

            return userStatisticsDTO;
        }

        /// <summary>
        /// Отправляет в репозиторий запрос на добавление статистики и возвращает результат.
        /// </summary>
        /// <param name="DTO">Пользовтельскаяа статистика.</param>
        /// <returns>
        /// Результат добавления статистикти:
        /// 200 - создана новая запись статистики;
        /// 404 - ошибка создания статистики: пользоавтель с таким ID не существует;
        /// 412 - ошибка создания статистики: статистика с такими данными уже существует;
        /// 418 - ошибка создания статистики: непредвиденная ошибка.
        /// </returns>
        [HttpPost]
        public async Task<IActionResult> PostAsync([FromBody]UserStatisticsDTO DTO)
        {
            UserStatistics statistics = DTO.Adapt<UserStatistics>();
            statistics.Events = DTO.EventsDTO.Adapt<List<Events>>();

            foreach (var e in statistics.Events)
            {
                e.ID = statistics.ID;
            }

            var status = await statisticsService.CreateStatisticsAsync(statistics);

            await hubContext.Clients.All.SendAsync("update");

            return StatusCode(status);
        }

        /// <summary>
        /// Отправляет в репозиторий запрос на удаление статистики и возвращает результат.
        /// </summary>
        /// <param name="ID">ID пользователя для удаляемой статистики.</param>
        /// <returns>
        /// Результат удаления статистикти:
        /// 200 - запись статистики удалена;
        /// 404 - ошибка удаления статистики: пользоавтель или статистика с такими данными не существует;
        /// 418 - ошибка удаления статистики: непредвиденная ошибка.
        [HttpDelete]
        public async Task<IActionResult> DeleteAsync(string ID)
        {
            var status = await statisticsService.DeleteStatisticsAsync(ID);

            await hubContext.Clients.All.SendAsync("update");

            return StatusCode(status);
        }
    }
}
