using Mapster;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;
using System.Net;
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
            WebRequest request = WebRequest.Create("https://localhost:5001/api/statistics/UserStatistics");
            WebResponse response = request.GetResponse();

            StreamReader stream = new StreamReader(response.GetResponseStream());
            string json = stream.ReadToEnd();

            List<UserStatisticsDTO> usersDTO = JsonConvert.DeserializeObject<List<UserStatisticsDTO>>(json);

            //JsonConvert.SerializeObject();

            List<UserStatistics> users = usersDTO.Adapt<List<UserStatistics>>();

            return Task.FromResult(users);
        }
    }
}
