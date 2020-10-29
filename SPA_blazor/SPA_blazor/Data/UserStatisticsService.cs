using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text.Json;
using System.Threading.Tasks;

namespace Infotecs.SPA_blazor.Data
{
    /// <summary>
    /// Класс для работы с пользовательской статистикой.
    /// </summary>
    public class UserStatisticsService
    {
        /// <summary>
        /// Асинхронно запрашивает пользовательскую статистику у сервера.
        /// </summary>
        /// <returns>Список пользоавтельских статистик.</returns>
        public Task<List<UserStatistics>> GetUserStatisticsAsync()
        {
            WebRequest request = WebRequest.Create("https://localhost:5001/api/userstatistics");
            WebResponse response = request.GetResponse();

            StreamReader stream = new StreamReader(response.GetResponseStream());
            string json = stream.ReadToEnd();

            List<UserStatistics> users = JsonSerializer.Deserialize<List<UserStatistics>>(json);

            return Task.FromResult(users);
        }
    }
}
