using Infotecs.WebApi.Models;
using Infotecs.WebApi.Services;
using Mapster;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApi.Repositories;

namespace Infotecs.WebApi.Controllers
{
    /// <summary>
    /// Класс контроллера для обработки REST запросов и 
    /// взаимодействия с пользовательской статистикой.
    /// </summary>
    [Route("api/events/[controller]")]
    [ApiController]
    public class EventsController : Controller
    {
        private readonly StatisticsService statisticsService = null;
        private readonly EventsService eventsService = null;

        IHubContext<WebApiHub> hubContext;

        /// <summary>
        /// Конструктор для привязки системы логирования и базы данных.
        /// </summary>
        /// <param name="logger">Интерфейс системы логирования.</param>
        /// <param name="repository">Интерфейс базы данных.</param>
        public EventsController(ILogger logger, IUnitOfWork repository, IHubContext<WebApiHub> hubContext)
        {
            statisticsService = new StatisticsService(repository, logger);
            eventsService = new EventsService(repository, logger);

            this.hubContext = hubContext;
        }

        /// <summary>
        /// Запрос списка всех статистик.
        /// </summary>
        /// <param name="ID">ID пользователя для получаемой статистики.</param>
        /// <returns>Список всех статиситк.</returns>
        [HttpGet("{ID}")]
        public async Task<List<EventsDTO>> GetAsync(string ID)
        {
            List<EventsDTO> userStatisticsDTO = (await eventsService.GetEventsAsync(ID)).Adapt<List<EventsDTO>>();

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
        public async Task<IActionResult> PostAsync(string ID, [FromBody] List<EventsDTO> eventsDTOs)
        {
            List<Events> events = eventsDTOs.Adapt<List<Events>>();

            if (events != null)
            {
                foreach (var e in events)
                {
                    e.ID = ID;
                }
            }

            var status = await eventsService.CreateEventsAsync(events);

            System.Console.WriteLine("send update events " + ID);
            await hubContext.Clients.All.SendAsync("update events " + ID);

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
            var status = await eventsService.DeleteEventsAsync(ID);

            System.Console.WriteLine("send update events " + ID);
            await hubContext.Clients.All.SendAsync("update events " + ID);

            return StatusCode(status);
        }
    }
}
