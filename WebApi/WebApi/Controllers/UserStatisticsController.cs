using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebApi.Models;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserStatisticsController : ControllerBase
    {
        private List<UserStatistics> userStatistics = new List<UserStatistics>();

        public UserStatisticsController()
        {
            userStatistics.Add(new UserStatistics("ivan", DateTime.Now, "1.0.0", "android"));
            userStatistics.Add(new UserStatistics("pavel", DateTime.Now, "1.0.0", "android"));
        }

        [HttpGet]
        public string Get()
        {
            return JsonSerializer.Serialize<List<UserStatistics>>(userStatistics);
        }

        [HttpPost]
        public ActionResult<UserStatistics> Post(UserStatistics user)
        {
            if (user == null)
            {
                return BadRequest();
            }

            Console.WriteLine(">> " + user.NameOfNode);
            Console.WriteLine(">> " + user.DateTimeOfLastStatistics);
            Console.WriteLine(">> " + user.VersionOfClient);
            Console.WriteLine(">> " + user.TypeOfDevice);

            userStatistics.Add(user);
            //userStatistics.Add(JsonSerializer.Deserialize<UserStatistics>(user));

            return Ok(user);
        }

        [HttpPut]
        public ActionResult<string> Put(string user)
        {
            if (user == null)
            {
                return BadRequest();
            }

            UserStatistics newUser = JsonSerializer.Deserialize<UserStatistics>(user);

            if (!userStatistics.Exists(x => x.NameOfNode == newUser.NameOfNode))
            {
                return NotFound();
            }

            userStatistics[userStatistics.FindIndex(x => x.NameOfNode == newUser.NameOfNode)] = newUser;

            return Ok(user);
        }
    }
}
