// <copyright file="UserStatisticsController.cs" company="Infotecs">
// Copyright (c) Infotecs. All rights reserved.
// </copyright>

namespace Infotecs.WebApi.Controllers
{
    using System.Collections.Generic;
    using System.Text.Json;
    using Mapster;
    using Microsoft.AspNetCore.Mvc;
    using Serilog;
    using Infotecs.WebApi.Models;

    /// <summary>
    /// Класс контроллера для обработки REST запросов и 
    /// взаимодействия с пользовательской статистикой.
    /// </summary>
    [Route("api/[controller]")]
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
        /// Обработка GET запроса списка из всех пользователей. 
        /// Метод получает из базы данных список всех пользователей,
        /// конвертирует его в список DTO, сериализует в JSON и возвращает.
        /// </summary>
        /// <returns>JSON строка со списком DTO всех пользователей.</returns>
        [HttpGet]
        public List<UserStatisticsDTO> Get()
        {
            this.logger.Debug("Запрос списка статистик");

            List<UserStatisticsDTO> users3 = this.repository.GetUsersList();
            //List<UserStatisticsDTO> users4 = users3.Adapt<List<UserStatisticsDTO>>();

            return users3;
        }

        /// <summary>
        /// Обработка POST запроса для добавления пользователя.
        /// Метод принимает DTO пользователя, конвертирует его в модель и добавляет в базу данных.
        /// </summary>
        /// <param name="user">DTO пользовательской статистики.</param>
        /// <returns>Результат выполнения запроса.</returns>
        [HttpPost]
        public IActionResult Post([FromBody]UserStatisticsDTO user)
        {
            this.logger.Debug("Запрос на добавление статистики {@User}", user);

            //UserStatistics newUser = user.Adapt<UserStatistics>();

            //System.Console.WriteLine(newUser.DateTimeOfLastStatistics);

            int status = this.repository.Create(user);
            if (status == 1)
            {
                this.logger.Debug("создана запись о новом пользователе {@Name} и добавлена статистика о нём {@User}.", new { Name = user.NameOfNode }, user);
            }
            else
            {
                this.logger.Error("добавлена статистика {@User} о существующем пользователе {@Name}", user, new { Name = user.NameOfNode });
            }
            return Ok(user);
        }

        /// <summary>
        /// Обработка PUT запроса для обновления пользователя.
        /// Метод принимает DTO пользователя, конвертирует его в модель и обновляет запись в базе данных.
        /// </summary>
        /// <param name="user">DTO пользовательской статистики.</param>
        /// <returns>Результат выполнения запроса.</returns>
        [HttpPut]
        public IActionResult Put([FromBody]UserStatisticsDTO user)
        {
            this.logger.Debug("Запрос на обновление данных о статистике {@User}", user);

            //UserStatistics newUser = user.Adapt<UserStatistics>();

            int status = this.repository.Update(user);
            if (status == 1)
            {
                this.logger.Debug("Запрос на обновление данных о статистике {@User} подтверждён.", user);
                return Ok(user);
            }
            else
            {
                this.logger.Error("Запрос на обновление данных о статистике {@User} отклонён. Статистики с таким данными не существует.", user);
                return NotFound(user);
            }
        }

        /// <summary>
        /// Обработка DELETE запроса для удаления пользователя.
        /// Метод принимает DTO пользователя, конвертирует его в модель и удаляет запись с
        /// именем этого пользователя в базе данных.
        /// </summary>
        /// <param name="user">DTO пользовательской статистики.</param>
        /// <returns>Результат выполнения запроса.</returns>
        [HttpDelete]
        public IActionResult Delete([FromBody] UserStatisticsDTO user)
        {
            this.logger.Debug("Запрос на удаление статистики {@User}", User);

            int status = this.repository.Delete(user);
            if (status >= 1)
            {
                this.logger.Debug("Запрос на удаление статистики {@User} подтверждён.", User);
                return Ok(user);
            }
            else
            {
                this.logger.Error("Запрос на удаление статистики {@User} отклонён. Статистики с таким данными не существует.", User);
                return NotFound(user);
            }
        }
    }
}
