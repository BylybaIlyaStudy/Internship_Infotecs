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

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserStatisticsController : Controller
    {
        //private List<UserStatistics> userStatistics = new List<UserStatistics>();
        private readonly ILogger _logger = null;
        IRepository Repository = /*Program.DB*/null;

        public UserStatisticsController(ILogger logger, IRepository repository)
        {
            _logger = logger;
            Repository = repository;
        }

        [HttpGet]
        public string Get()
        {
            //.Error("Запрос списка пользователей (serilog)");
            _logger.Debug("Запрос списка пользователей");

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
            
            _logger.Debug("Запрос на добавление пользователя {@user}", user);
            UserStatistics newUsser = new UserStatistics(user.NameOfNode, Convert.ToDateTime(user.DateTimeOfLastStatistics), user.VersionOfClient, user.TypeOfDevice);
            //bool status = Program.DB.Create(newUsser);
            bool status = Repository.Create(newUsser);
            if (status)
            {
                _logger.Debug("Запрос на добавление пользователя {@user} подтверждён.", user);
            }
            else
            {
                _logger.Error("Запрос на добавление пользователя {@user} отклонён. Пользователь с таким именем уже существует.", user);
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
            _logger.Debug("Запрос на обновление данных о пользователе {@user}", user);
            UserStatistics newUsser = new UserStatistics(user.NameOfNode, Convert.ToDateTime(user.DateTimeOfLastStatistics), user.VersionOfClient, user.TypeOfDevice);
            //bool status = Program.DB.Update(newUsser);
            bool status = Repository.Update(newUsser);
            if (status)
            {
                _logger.Debug("Запрос на обновление данных о пользователе {@user} подтверждён.", user);
            }
            else
            {
                _logger.Error("Запрос на обновление данных о пользователе {@user} отклонён. Пользователя с таким именем не существует.", user);
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
            _logger.Debug("Запрос на удаление пользователя {@user}", user);

            //bool status = Program.DB.Delete(user.NameOfNode);
            bool status = Repository.Delete(user.NameOfNode);
            if (status)
            {
                _logger.Debug("Запрос на удаление пользователя {@user} подтверждён.", user);
            }
            else
            {
                _logger.Error("Запрос на удаление пользователя {@user} отклонён. Пользователя с таким именем не существует.", user);
            }
            //return Ok(user);
        }
    }
}
