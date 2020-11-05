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
    /// взаимодействия с данными пользователей.
    /// </summary>
    [Route("api/users/[controller]")]
    [ApiController]
    public class UsersController : Controller
    {
        private readonly ILogger logger = null;
        private readonly IRepository repository = null;

        /// <summary>
        /// Конструктор для привязки системы логирования и базы данных.
        /// </summary>
        /// <param name="logger">Интерфейс системы логирования.</param>
        /// <param name="repository">Интерфейс базы данных.</param>
        public UsersController(ILogger logger, IRepository repository)
        {
            this.logger = logger;
            this.repository = repository;
        }

        /// <summary>
        /// Запрос из репозитория списка всех пользователей.
        /// </summary>
        /// <returns>Список всех пользователей.</returns>
        [HttpGet]
        public List<Users> Get()
        {
            this.logger.Debug("Запрос списка пользователей");

            return this.repository.GetUsersList();
        }

        /// <summary>
        /// Отправляет в репозиторий запрос на добавление пользователя и возвращает результат.
        /// </summary>
        /// <param name="DTO">Пользователь.</param>
        /// <returns>
        /// Результат добавления пользователя:
        /// Ok - создана новая запись пользователя;
        /// 412 - ошибка создания пользователя: пользователб с таким ID уже существует;
        /// 418 - ошибка создания пользователя: непредвиденная ошибка.
        /// </returns>
        [HttpPost]
        public IActionResult Post([FromBody] UsersDTO DTO)
        {
            Users user = DTO.Adapt<Users>();

            int status = this.repository.CreateUser(user);

            if (status == 0)
            {
                this.logger.Debug("создана запись о новом пользователе {@Users}", user);

                return Ok(DTO);
            }
            else
            {
                if (status == 1)
                {
                    this.logger.Error("ошибка создания пользователя {@Users}: пользоавтель с таким ID уже существует", user);

                    return StatusCode(412);
                }
            }

            this.logger.Fatal("ошибка создания пользователя {@Users}: непредвиденная ошибка", user);

            return StatusCode(418);
        }

        /// <summary>
        /// Отправляет в репозиторий запрос на обновление пользователя и возвращает результат.
        /// </summary>
        /// <param name="DTO">Пользователь.</param>
        /// <returns>
        /// Результат обновления пользователя:
        /// Ok - запись пользователя обновлена;
        /// NotFound - ошибка обновления пользователя: пользоавтель с таким ID не существует;
        /// 418 - ошибка обновления пользователя: непредвиденная ошибка.
        /// </returns>
        [HttpPut]
        public IActionResult Put([FromBody] UsersDTO DTO)
        {
            Users user = DTO.Adapt<Users>();

            int status = this.repository.UpdateUser(user);

            if (status == 0)
            {
                this.logger.Debug("запись о пользователе {@Users} обновлена", user);

                return Ok(DTO);
            }
            else
            {
                if (status == 1)
                {
                    this.logger.Error("ошибка обновления пользователя {@Users}: пользоавтель с таким ID не существует", user);

                    return NotFound(DTO);
                }
            }

            this.logger.Fatal("ошибка создания пользователя {@Users}: непредвиденная ошибка", user);

            return StatusCode(418);
        }

        /// <summary>
        /// Отправляет в репозиторий запрос на удаление пользователя и возвращает результат.
        /// </summary>
        /// <param name="DTO">Пользователь.</param>
        /// <returns>
        /// Результат удаления пользователя:
        /// Ok - запись пользователя удалена;
        /// NotFound - ошибка удаления пользователя: пользоавтель с таким ID не существует;
        /// 418 - ошибка удаления пользователя: непредвиденная ошибка.
        /// </returns>
        [HttpDelete]
        public IActionResult Delete([FromBody] UsersDTO DTO)
        {
            Users user = DTO.Adapt<Users>();

            int status = this.repository.DeleteUser(user);

            if (status == 0)
            {
                this.logger.Debug("запись о пользователе {@Users} удалена", user);

                return Ok(DTO);
            }
            else
            {
                if (status == 1)
                {
                    this.logger.Error("ошибка удаления пользователя {@Users}: пользоавтель с таким ID не существует", user);

                    return NotFound(DTO);
                }
            }

            this.logger.Fatal("ошибка создания пользователя {@Users}: непредвиденная ошибка", user);

            return StatusCode(418);
        }
    }
}
