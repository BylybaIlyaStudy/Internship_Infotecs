using Infotecs.WebApi.Models;
using Serilog;
using System.Collections.Generic;

namespace Infotecs.WebApi.Services
{
    public class StatisticsService
    {
        private readonly ILogger logger = null;
        private readonly IRepository repository = null;

        public StatisticsService(IRepository repository, ILogger logger)
        {
            this.logger = logger;
            this.repository = repository;
        }

        public int CreateStatistics(UserStatistics statistics)
        {
            Users foundUser = repository.GetUser(statistics.ID);

            if (foundUser != null)
            {
                _ = repository.DeleteStatistics(statistics.ID);

                repository.CreateStatistics(statistics);

                return 200;
            }
            else
            {
                return 404;
            }
        }

        public List<UserStatistics> GetStatistics()
        {
            this.logger.Debug("Запрос списка статистик");

            List<UserStatistics> statistics = repository.GetStatisticsList();

            return statistics;
        }

        public UserStatistics GetStatistics(string ID)
        {
            this.logger.Debug("Запрос списка статистик {@Users}", ID);

            UserStatistics statistics = repository.GetStatistics(ID);

            return statistics;
        }

        public int DeleteStatistics(string ID)
        {
            Users foundUser = repository.GetUser(ID);

            if (foundUser != null)
            {
                UserStatistics foundStatistics = repository.GetStatistics(ID);

                if (foundStatistics != null)
                {
                    repository.DeleteStatistics(ID);

                    return 200;
                }
                else
                {
                    return 404;
                }
            }
            else
            {
                return 404;
            }
        }
    }
}
