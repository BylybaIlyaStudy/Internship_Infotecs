using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text.Json;
using System.Threading.Tasks;

namespace Infotecs.SPA_blazor.Data
{
    public class UserStatisticsService
    {
        public List<UserStatistics> GetUserStatistics()
        {
            //List<UserStatistics> userStatistics = null;

            WebRequest request = WebRequest.Create("https://localhost:5001/api/userstatistics");
            WebResponse response = request.GetResponse();

            StreamReader stream = new StreamReader(response.GetResponseStream());
            string json = stream.ReadToEnd();

            List<UserStatistics> users = JsonSerializer.Deserialize<List<UserStatistics>>(json);

            return users;
        }
    }
}
