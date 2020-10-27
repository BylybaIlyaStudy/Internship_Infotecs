using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebApi.Models;
using WebApi;
using Serilog;
using Mapster;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserStatisticsController : Controller
    {
        private readonly ILogger _logger = null;
        private readonly IRepository Repository = null;

        public UserStatisticsController(ILogger logger, IRepository repository)
        {
            _logger = logger;
            Repository = repository;
        }

        [HttpGet]
        public string Get()
        {
            _logger.Debug("Запрос списка пользователей");

            List<UserStatistics> users3 = Repository.GetUsersList();
            List<UserStatisticsDTO> users4 = users3.Adapt<List<UserStatisticsDTO>>();

            return JsonSerializer.Serialize<List<UserStatisticsDTO>>(users4);
        }

        [HttpPost]
        public void Post([FromBody]UserStatisticsDTO user)
        {
            _logger.Debug("Запрос на добавление пользователя {@user}", user);

            UserStatistics newUser = user.Adapt<UserStatistics>();

            bool status = Repository.Create(newUser);
            if (status)
            {
                _logger.Debug("Запрос на добавление пользователя {@user} подтверждён.", user);
            }
            else
            {
                _logger.Error("Запрос на добавление пользователя {@user} отклонён. Пользователь с таким именем уже существует.", user);
            }
        }

        [HttpPut]
        public void Put([FromBody]UserStatisticsDTO user)
        {
            _logger.Debug("Запрос на обновление данных о пользователе {@user}", user);

            UserStatistics newUser = user.Adapt<UserStatistics>();

            bool status = Repository.Update(newUser);
            if (status)
            {
                _logger.Debug("Запрос на обновление данных о пользователе {@user} подтверждён.", user);
            }
            else
            {
                _logger.Error("Запрос на обновление данных о пользователе {@user} отклонён. Пользователя с таким именем не существует.", user);
            }
        }

        [HttpDelete]
        public void Delete([FromBody]UserStatisticsDTO user)
        {
            _logger.Debug("Запрос на удаление пользователя {@user}", user);

            bool status = Repository.Delete(user.NameOfNode);
            if (status)
            {
                _logger.Debug("Запрос на удаление пользователя {@user} подтверждён.", user);
            }
            else
            {
                _logger.Error("Запрос на удаление пользователя {@user} отклонён. Пользователя с таким именем не существует.", user);
            }
        }
    }
}
