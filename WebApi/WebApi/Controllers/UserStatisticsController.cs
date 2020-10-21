using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebApi.Models;
using WebApi;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserStatisticsController : ControllerBase
    {
        //private List<UserStatistics> userStatistics = new List<UserStatistics>();

        public UserStatisticsController()
        {
        }

        [HttpGet]
        public string Get()
        {
            return JsonSerializer.Serialize<List<UserStatistics>>(Program.DB.GetUsersList());
        }

        [HttpPost]
        public void Post([FromBody]UserStatistics user)
        {
            //if (user == null)
            //{
            //    return BadRequest();
            //}

            //Console.WriteLine(">> " + user.NameOfNode);
            //Console.WriteLine(">> " + user.DateTimeOfLastStatistics);
            //Console.WriteLine(">> " + user.VersionOfClient);
            //Console.WriteLine(">> " + user.TypeOfDevice);

            Program.DB.Create(user);
            //userStatistics.Add(JsonSerializer.Deserialize<UserStatistics>(user));

            //return Ok(user);
        }

        [HttpPut]
        public void Put([FromBody]UserStatistics user)
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

            Program.DB.Update(user);

            //return Ok(user);
        }
    }
}
