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
using System.Text.Json;

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
        private readonly ILogger logger = null;
        private readonly IUnitOfWork repository = null;
        
        private readonly StatisticsService statisticsService = null;

        /// <summary>
        /// Конструктор для привязки системы логирования и базы данных.
        /// </summary>
        /// <param Name="logger">Интерфейс системы логирования.</param>
        /// <param Name="repository">Интерфейс базы данных.</param>
        public UserStatisticsController(ILogger logger, IUnitOfWork repository)
        {
            this.logger = logger;
            this.repository = repository;

            
            statisticsService = new StatisticsService(repository, logger);
        }

        /// <summary>
        /// Запрос списка всех статистик.
        /// </summary>
        /// <returns>Список всех статиситк.</returns>
        [HttpGet]
        public List<UserStatisticsDTO> Get()
        {
            List<UserStatisticsDTO> userStatisticsDTOS = statisticsService.GetStatistics().Adapt<List<UserStatisticsDTO>>();

            if (userStatisticsDTOS != null)
            {
                foreach (var statistics in userStatisticsDTOS)
                {
                    statistics.EventsDTO = statisticsService.GetStatistics(statistics.ID).Events.Adapt<List<EventsDTO>>();
                }

            }

            return userStatisticsDTOS;
        }

        /// <summary>
        /// Запрос списка всех статистик.
        /// </summary>
        /// <returns>Список всех статиситк.</returns>
        [HttpGet("{ID}")]
        public UserStatisticsDTO Get(string ID)
        {
            UserStatisticsDTO userStatisticsDTO = statisticsService.GetStatistics(ID).Adapt<UserStatisticsDTO>();
            if (userStatisticsDTO != null)
            {
                userStatisticsDTO.EventsDTO = statisticsService.GetStatistics(ID).Events.Adapt<List<EventsDTO>>();
            }

            return userStatisticsDTO;
        }

        /// <summary>
        /// Отправляет в репозиторий запрос на добавление статистики и возвращает результат.
        /// </summary>
        /// <param Name="DTO">Пользовательская статистика.</param>
        /// <returns>
        /// Результат добавления статистикти:
        /// Ok - создана новая запись статистики;
        /// NotFound - ошибка создания статистики: пользоавтель с таким ID не существует;
        /// 412 - ошибка создания статистики: статистика с такими данными уже существует;
        /// 418 - ошибка создания статистики: непредвиденная ошибка.
        /// </returns>
        [HttpPost]
        public IActionResult Post([FromBody]UserStatisticsDTO DTO)
        {
            UserStatistics statistics = DTO.Adapt<UserStatistics>();
            statistics.Events = DTO.EventsDTO.Adapt<List<Events>>();

            foreach (var e in statistics.Events)
            {
                e.ID = statistics.ID;
            }

            return StatusCode(statisticsService.CreateStatistics(statistics));
        }

        /// <summary>
        /// Отправляет в репозиторий запрос на удаление статистики и возвращает результат.
        /// </summary>
        /// <param Name="DTO">Удаляемая статистика.</param>
        /// <returns>
        /// Результат удаления статистикти:
        /// Ok - запись статистики удалена;
        /// NotFound - ошибка удаления статистики: пользоавтель или статистика с такими данными не существует;
        /// 418 - ошибка удаления статистики: непредвиденная ошибка.
        [HttpDelete]
        public IActionResult Delete(string ID)
        {
            return StatusCode(statisticsService.DeleteStatistics(ID));
        }
    }
}
