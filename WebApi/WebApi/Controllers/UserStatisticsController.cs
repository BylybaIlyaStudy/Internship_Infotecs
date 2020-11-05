// <copyright file="UserStatisticsController.cs" company="Infotecs">
// Copyright (c) Infotecs. All rights reserved.
// </copyright>

using System.Collections.Generic;
using Mapster;
using Microsoft.AspNetCore.Mvc;
using Serilog;
using Infotecs.WebApi.Models;

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
        private readonly IRepository repository = null;

        /// <summary>
        /// Конструктор для привязки системы логирования и базы данных.
        /// </summary>
        /// <param name="logger">Интерфейс системы логирования.</param>
        /// <param name="repository">Интерфейс базы данных.</param>
        public UserStatisticsController(ILogger logger, IRepository repository)
        {
            this.logger = logger;
            this.repository = repository;
        }

        /// <summary>
        /// Запрос списка всех статистик.
        /// </summary>
        /// <returns>Список всех статиситк.</returns>
        [HttpGet]
        public List<UserStatistics> Get()
        {
            this.logger.Debug("Запрос списка статистик");

            return repository.GetStatisticsList();
        }

        /// <summary>
        /// Отправляет в репозиторий запрос на добавление статистики и возвращает результат.
        /// </summary>
        /// <param name="DTO">Пользовательская статистика.</param>
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
            this.logger.Debug("Запрос на добавление статистики {@UserStatisticsDTO}", DTO);

            UserStatistics statistics = DTO.Adapt<UserStatistics>();

            int status = repository.CreateStatistics(statistics);

            if (status == 0)
            {
                this.logger.Debug("создана новая запись статистики {@UserStatisticsDTO}", DTO);

                return Ok(DTO);
            }
            else
            {
                if (status == 1)
                {
                    this.logger.Error("ошибка создания статистики {@UserStatisticsDTO}: пользоавтель с таким ID не существует", DTO);

                    return NotFound(DTO);
                }
                if (status == 2)
                {
                    this.logger.Error("ошибка создания статистики {@UserStatisticsDTO}: статистика с такими данными уже существует", DTO);

                    return StatusCode(412);
                }
            }

            this.logger.Fatal("ошибка создания статистики {@UserStatisticsDTO}: непредвиденная ошибка", DTO);

            return StatusCode(418);
        }

        /// <summary>
        /// Отправляет в репозиторий запрос на обновление статистики и возвращает результат.
        /// </summary>
        /// <param name="oldDTO">Статистика, которую нужно обновить ([FromBody]).</param>
        /// <param name="newDTO">Статистика, на которую нужно обновить ([FromQuery]).</param>
        /// <returns>
        /// Результат обновления статистикти:
        /// Ok - запись статистики обновлена;
        /// NotFound - ошибка обновления статистики: пользоавтель с таким ID или статистика с такими данными не существует;
        /// 412 - ошибка обновления статистики: пользовательские данные не совпадают;
        /// 418 - ошибка обновления статистики: непредвиденная ошибка.
        [HttpPut]
        public IActionResult Put([FromBody] UserStatisticsDTO oldDTO, [FromQuery] UserStatisticsDTO newDTO)
        {
            this.logger.Debug("Запрос на обновление статистики {@OldUserStatisticsDTO} => {@NewUserStatisticsDTO}", oldDTO, newDTO);

            UserStatistics oldStatistics = oldDTO.Adapt<UserStatistics>();
            UserStatistics newStatistics = newDTO.Adapt<UserStatistics>();

            int status = repository.UpdateStatistics(oldStatistics, newStatistics);

            if (status == 0)
            {
                this.logger.Debug("запись статистики обновлена {@OldUserStatisticsDTO} => {@NewUserStatisticsDTO}", oldDTO, newDTO);

                return Ok(newDTO);
            }
            else
            {
                if (status == 1)
                {
                    this.logger.Error("ошибка обновления статистики {@OldUserStatisticsDTO} => {@NewUserStatisticsDTO}: пользоавтель с таким ID не существует", oldDTO, newDTO);

                    return NotFound(oldDTO);
                }
                if (status == 2)
                {
                    this.logger.Error("ошибка обновления статистики {@OldUserStatisticsDTO} => {@NewUserStatisticsDTO}: статистика с такими данными не существует", oldDTO, newDTO);

                    return NotFound(oldDTO);
                }
                if (status == 3)
                {
                    this.logger.Error("ошибка обновления статистики {@OldUserStatisticsDTO} => {@NewUserStatisticsDTO}: ID пользователя обновляемой статистики не совпадает с ID пользователя новой статистики", oldDTO, newDTO);

                    return StatusCode(412);
                }
                if (status == 4)
                {
                    this.logger.Error("ошибка обновления статистики {@OldUserStatisticsDTO} => {@NewUserStatisticsDTO}: имя пользователя обновляемой статистики не совпадает с именем пользователя новой статистики", oldDTO, newDTO);

                    return StatusCode(412);
                }
            }

            this.logger.Fatal("ошибка создания обновления {@OldUserStatisticsDTO} => {@NewUserStatisticsDTO}: непредвиденная ошибка", oldDTO, newDTO);

            return StatusCode(418);
        }

        /// <summary>
        /// Отправляет в репозиторий запрос на удаление статистики и возвращает результат.
        /// </summary>
        /// <param name="DTO">Удаляемая статистика.</param>
        /// <returns>
        /// Результат удаления статистикти:
        /// Ok - запись статистики удалена;
        /// NotFound - ошибка удаления статистики: пользоавтель или статистика с такими данными не существует;
        /// 418 - ошибка удаления статистики: непредвиденная ошибка.
        [HttpDelete]
        public IActionResult Delete([FromBody] UserStatisticsDTO DTO)
        {
            this.logger.Debug("Запрос на удаление статистики {@UserStatisticsDTO}", DTO);

            UserStatistics statistics = DTO.Adapt<UserStatistics>();

            int status = repository.DeleteStatistics(statistics);

            if (status == 0)
            {
                this.logger.Debug("удалена запись статистики {@UserStatisticsDTO}", DTO);

                return Ok(DTO);
            }
            else
            {
                if (status == 1)
                {
                    this.logger.Error("ошибка удаления статистики {@UserStatisticsDTO}: пользоавтель с такими данными не существует", DTO);

                    return NotFound(DTO);
                }
                if (status == 2)
                {
                    this.logger.Error("ошибка удаления статистики {@UserStatisticsDTO}: статистика с такими данными не существует", DTO);

                    return NotFound(DTO);
                }
            }

            this.logger.Fatal("ошибка удаления статистики {@UserStatisticsDTO}: непредвиденная ошибка", DTO);

            return StatusCode(418);
        }
    }
}
