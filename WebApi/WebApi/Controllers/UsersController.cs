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
    /// взаимодействия с данными пользователей.
    /// </summary>
    [Route("api/users/[controller]")]
    [ApiController]
    public class UsersController : Controller
    {
        private readonly UserService userService = null;
        IHubContext<WebApiHub> hubContext;
        /// <summary>
        /// Конструктор для привязки системы логирования и базы данных.
        /// </summary>
        /// <param name="logger">Интерфейс системы логирования.</param>
        /// <param name="repository">Интерфейс базы данных.</param>
        public UsersController(ILogger logger, IUnitOfWork repository, IHubContext<WebApiHub> hubContext)
        {
            userService = new UserService(repository, logger);
            
            this.hubContext = hubContext;
        }

        /// <summary>
        /// Запрос из репозитория списка всех пользователей.
        /// </summary>
        /// <returns>Список всех пользователей.</returns>
        [HttpGet]
        public async Task<List<UsersDTO>> GetAsync()
        {
            List<UsersDTO> usersDTOS = (await userService.GetUsersListAsync()).Adapt<List<UsersDTO>>();

            return usersDTOS;
        }

        /// <summary>
        /// Запрос из репозитория пользователя по ID.
        /// </summary>
        /// <param name="ID">ID пользователя.</param>
        /// <returns>Список всех пользователей.</returns>
        [HttpGet("{ID}")]
        public async Task<UsersDTO> GetAsync(string ID)
        {
            UsersDTO usersDTO = (await userService.GetUserAsync(ID)).Adapt<UsersDTO>();

            return usersDTO;
        }

        /// <summary>
        /// Отправляет в репозиторий запрос на добавление пользователя и возвращает результат.
        /// </summary>
        /// <param name="usersDTO">Пользователь.</param>
        /// <returns>
        /// Результат добавления пользователя:
        /// Ok - создана новая запись пользователя;
        /// 412 - ошибка создания пользователя: пользователб с таким ID уже существует;
        /// 418 - ошибка создания пользователя: непредвиденная ошибка.
        /// </returns>
        [HttpPost]
        public async Task<IActionResult> PostAsync([FromBody] UsersDTO usersDTO)
        {
            Users user = usersDTO.Adapt<Users>();

            var status = await userService.CreateUserAsync(user);

            System.Console.WriteLine("send update users");
            await hubContext.Clients.All.SendAsync("update users");

            return StatusCode(status);
        }
        [HttpPut]
        public async Task<IActionResult> PutAsync([FromBody] UsersDTO usersDTO)
        {
            Users user = usersDTO.Adapt<Users>();

            var status = await userService.UpdateUserAsync(user);

            System.Console.WriteLine("send update users");
            await hubContext.Clients.All.SendAsync("update users");

            return StatusCode(status);
        }

        /// <summary>
        /// Отправляет в репозиторий запрос на удаление пользователя и возвращает результат.
        /// </summary>
        /// <param name="ID">ID пользователя, которого необходимо удалить.</param>
        /// <returns>
        /// Результат удаления пользователя:
        /// Ok - запись пользователя удалена;
        /// NotFound - ошибка удаления пользователя: пользоавтель с таким ID не существует;
        /// 418 - ошибка удаления пользователя: непредвиденная ошибка.
        /// </returns>
        [HttpDelete]
        public async Task<IActionResult> DeleteAsync(string ID)
        {
            var status = await userService.DeleteUserAsync(ID);

            System.Console.WriteLine("send update users");
            await hubContext.Clients.All.SendAsync("update users");

            return StatusCode(status);
        }
    }
}
