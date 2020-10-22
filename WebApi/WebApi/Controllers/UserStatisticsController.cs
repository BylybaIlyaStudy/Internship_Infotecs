using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebApi.Models;
using WebApi;
using Microsoft.Extensions.Logging;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserStatisticsController : ControllerBase
    {
        //private List<UserStatistics> userStatistics = new List<UserStatistics>();
        ILogger<UserStatisticsController> _logger = null;

        public UserStatisticsController(ILogger<UserStatisticsController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public string Get()
        {
            _logger.LogInformation("Запрос списка пользователей");

            List<UserStatisticsDTO> users = (from user in Program.DB.GetUsersList()
                                             select new UserStatisticsDTO()
                                             {
                                                 NameOfNode = user.NameOfNode,
                                                 DateTimeOfLastStatistics = user.DateTimeOfLastStatistics,
                                                 VersionOfClient = user.VersionOfClient,
                                                 TypeOfDevice = user.TypeOfDevice
                                             }).ToList();
            return JsonSerializer.Serialize<List<UserStatisticsDTO>>(users);
        }

        [HttpPost]
        public void Post([FromBody]UserStatisticsDTO user)
        {
            //if (user == null)
            //{
            //    return BadRequest();
            //}

            //Console.WriteLine(">> " + user.NameOfNode);
            //Console.WriteLine(">> " + user.DateTimeOfLastStatistics);
            //Console.WriteLine(">> " + user.VersionOfClient);
            //Console.WriteLine(">> " + user.TypeOfDevice);
            _logger.LogInformation($"Запрос на добавление пользователя {user.NameOfNode}");
            UserStatistics newUsser = new UserStatistics(user.NameOfNode, Convert.ToDateTime(user.DateTimeOfLastStatistics), user.VersionOfClient, user.TypeOfDevice);
            bool status = Program.DB.Create(newUsser);
            if (status)
            {
                _logger.LogInformation(($"Запрос на добавление пользователя {user.NameOfNode} подтверждён."));
            }
            else
            {
                _logger.LogError(($"Запрос на добавление пользователя {user.NameOfNode} отклонён. Пользователь с таким именем уже существует."));
            }
            //userStatistics.Add(JsonSerializer.Deserialize<UserStatistics>(user));

            //return Ok(user);
        }

        [HttpPut]
        public void Put([FromBody]UserStatisticsDTO user)
        {
            //if (user == null)
            //{
            //    return BadRequest();
            //}

            //UserStatistics newUser = JsonSerializer.Deserialize<UserStatistics>(user);

            //if (!Program.DB.Users.Exists(x => x.NameOfNode == newUser.NameOfNode))
            //{
            //    return NotFound();
            //}
            _logger.LogInformation($"Запрос на обновление данных о пользователе {user.NameOfNode}");
            UserStatistics newUsser = new UserStatistics(user.NameOfNode, Convert.ToDateTime(user.DateTimeOfLastStatistics), user.VersionOfClient, user.TypeOfDevice);
            bool status = Program.DB.Update(newUsser);
            if (status)
            {
                _logger.LogInformation(($"Запрос на обновление данных о пользователе {user.NameOfNode} подтверждён."));
            }
            else
            {
                _logger.LogError(($"Запрос на обновление данных о пользователе {user.NameOfNode} отклонён. Пользователя с таким именем не существует."));
            }
            //return Ok(user);
        }

        [HttpDelete]
        public void Delete([FromBody]UserStatisticsDTO user)
        {
            //if (user == null)
            //{
            //    return BadRequest();
            //}

            //UserStatistics newUser = JsonSerializer.Deserialize<UserStatistics>(user);

            //if (!Program.DB.Users.Exists(x => x.NameOfNode == newUser.NameOfNode))
            //{
            //    return NotFound();
            //}
            _logger.LogInformation($"Запрос на удаление пользователя {user.NameOfNode}");

            bool status = Program.DB.Delete(user.NameOfNode);
            if (status)
            {
                _logger.LogInformation(($"Запрос на удаление пользователя {user.NameOfNode} подтверждён."));
            }
            else
            {
                _logger.LogError(($"Запрос на удаление пользователя {user.NameOfNode} отклонён. Пользователя с таким именем не существует."));
            }
            //return Ok(user);
        }
    }
}
