using Infotecs.WebApi.Models;
using Serilog;
using System.Collections.Generic;
using WebApi.Repositories;

namespace Infotecs.WebApi.Services
{
    public class StatisticsService
    {
        private readonly ILogger logger = null;
        private readonly IUnitOfWork repository = null;

        public StatisticsService(IUnitOfWork repository, ILogger logger)
        {
            this.logger = logger;
            this.repository = repository;
        }

        public int CreateStatistics(UserStatistics statistics)
        {
            Users foundUser = repository.Users.Get(statistics.ID);

            if (foundUser != null)
            {
                _ = repository.Statistics.Delete(statistics.ID);
                _ = repository.Events.Delete(statistics.ID);

                repository.Statistics.Create(statistics);
                repository.Events.Create(statistics.Events);

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

            List<UserStatistics> statistics = repository.Statistics.GetList();

            if (statistics != null)
            {
                foreach (var stat in statistics)
                {
                    stat.Events = repository.Events.Get(stat.ID);
                }
            }

            return statistics;
        }

        public UserStatistics GetStatistics(string ID)
        {
            this.logger.Debug("Запрос списка статистик {@Users}", ID);

            UserStatistics statistics = repository.Statistics.Get(ID);

            if (statistics != null)
            {
                statistics.Events = repository.Events.Get(statistics.ID);
            }

            return statistics;
        }

        public int DeleteStatistics(string ID)
        {
            Users foundUser = repository.Users.Get(ID);

            if (foundUser != null)
            {
                UserStatistics foundStatistics = repository.Statistics.Get(ID);

                if (foundStatistics != null)
                {
                    repository.Statistics.Delete(ID);
                    repository.Events.Delete(ID);

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
