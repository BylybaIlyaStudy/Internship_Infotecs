using System;
using System.Collections.Generic;
using System.Linq;
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
        public IEnumerable<UserStatistics> Get()
        {
            return userStatistics;
        }

        [HttpPost]
        public ActionResult<UserStatistics> Post(UserStatistics user)
        {
            if (user == null)
            {
                return BadRequest();
            }

            userStatistics.Add(user);
            return Ok(user);
        }
    }
}
