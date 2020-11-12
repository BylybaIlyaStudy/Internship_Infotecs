// <copyright file="UserStatisticsController.cs" company="Infotecs">
// Copyright (c) Infotecs. All rights reserved.
// </copyright>

using System.Collections.Generic;
using Mapster;
using Microsoft.AspNetCore.Mvc;
using Serilog;
using Infotecs.WebApi.Models;
using Infotecs.WebApi.Services;

namespace Infotecs.WebApi.Controllers
{
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
        private readonly UserService userService = null;

        /// <summary>
        /// Конструктор для привязки системы логирования и базы данных.
        /// </summary>
        /// <param name="logger">Интерфейс системы логирования.</param>
        /// <param name="repository">Интерфейс базы данных.</param>
        public UsersController(ILogger logger, IRepository repository)
        {
            this.logger = logger;
            this.repository = repository;

            userService = new UserService(repository, logger);
        }

        /// <summary>
        /// Запрос из репозитория списка всех пользователей.
        /// </summary>
        /// <returns>Список всех пользователей.</returns>
        [HttpGet]
        public List<UsersDTO> Get()
        {
            List<UsersDTO> usersDTOs = userService.GetUsersList().Adapt<List<UsersDTO>>();

            return usersDTOs;
        }

        /// <summary>
        /// Запрос из репозитория списка всех пользователей.
        /// </summary>
        /// <returns>Список всех пользователей.</returns>
        [HttpGet("{ID}")]
        public UsersDTO Get(string ID)
        {
            UsersDTO usersDTO = userService.GetUser(ID).Adapt<UsersDTO>();

            return usersDTO;
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
        public IActionResult Post([FromBody] UsersDTO usersDTO)
        {
            Users user = usersDTO.Adapt<Users>();

            return StatusCode(userService.CreateUser(user));
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
        public IActionResult Delete(string ID)
        {
            return StatusCode(userService.DeleteUser(ID));
        }
    }
}
