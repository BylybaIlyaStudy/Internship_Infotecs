// <copyright file="UserStatisticsController.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace WebApi.Controllers
{
    using System.Collections.Generic;
    using System.Text.Json;
    using Mapster;
    using Microsoft.AspNetCore.Mvc;
    using Serilog;
    using WebApi.Models;

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
        public string Get()
        {
            this.logger.Debug("Запрос списка пользователей");

            List<UserStatistics> users3 = this.repository.GetUsersList();
            List<UserStatisticsDTO> users4 = users3.Adapt<List<UserStatisticsDTO>>();

            return JsonSerializer.Serialize<List<UserStatisticsDTO>>(users4);
        }

        /// <summary>
        /// Обработка POST запроса для добавления пользователя.
        /// Метод принимает DTO пользователя, конвертирует его в модель и добавляет в базу данных.
        /// </summary>
        /// <param name="user">DTO пользовательской статистики.</param>
        [HttpPost]
        public void Post([FromBody]UserStatisticsDTO user)
        {
            this.logger.Debug("Запрос на добавление пользователя {@User}", user);

            UserStatistics newUser = user.Adapt<UserStatistics>();

            bool status = this.repository.Create(newUser);
            if (status)
            {
                this.logger.Debug("Запрос на добавление пользователя {@User} подтверждён.", user);
            }
            else
            {
                this.logger.Error("Запрос на добавление пользователя {@User} отклонён. Пользователь с таким именем уже существует.", user);
            }
        }

        /// <summary>
        /// Обработка PUT запроса для обновления пользователя.
        /// Метод принимает DTO пользователя, конвертирует его в модель и обновляет запись в базе данных.
        /// </summary>
        /// <param name="user">DTO пользовательской статистики.</param>
        [HttpPut]
        public void Put([FromBody]UserStatisticsDTO user)
        {
            this.logger.Debug("Запрос на обновление данных о пользователе {@User}", user);

            UserStatistics newUser = user.Adapt<UserStatistics>();

            bool status = this.repository.Update(newUser);
            if (status)
            {
                this.logger.Debug("Запрос на обновление данных о пользователе {@User} подтверждён.", user);
            }
            else
            {
                this.logger.Error("Запрос на обновление данных о пользователе {@User} отклонён. Пользователя с таким именем не существует.", user);
            }
        }

        /// <summary>
        /// Обработка DELETE запроса для удаления пользователя.
        /// Метод принимает DTO пользователя, конвертирует его в модель и удаляет запись с
        /// именем этого пользователя в базе данных.
        /// </summary>
        /// <param name="user">DTO пользовательской статистики.</param>
        [HttpDelete]
        public void Delete([FromBody] UserStatisticsDTO user)
        {
            this.logger.Debug("Запрос на удаление пользователя {@User}", User);

            bool status = this.repository.Delete(user.NameOfNode);
            if (status)
            {
                this.logger.Debug("Запрос на удаление пользователя {@User} подтверждён.", User);
            }
            else
            {
                this.logger.Error("Запрос на удаление пользователя {@User} отклонён. Пользователя с таким именем не существует.", User);
            }
        }
    }
}
