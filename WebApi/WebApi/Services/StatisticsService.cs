using Infotecs.WebApi.Models;
using Serilog;
using System.Collections.Generic;
using System.Threading.Tasks;
using WebApi.Repositories;

namespace Infotecs.WebApi.Services
{
    /// <summary>
    /// Класс для работы с репозиторием пользовательской статистики.
    /// </summary>
    public class StatisticsService
    {
        private readonly ILogger logger = null;
        private readonly IUnitOfWork repository = null;

        /// <summary>
        /// Initializes a new instance of the <see cref="StatisticsService"/> class.
        /// </summary>
        /// <param name="logger">Интерфейс системы логирования.</param>
        /// <param name="repository">Интерфейс базы данных.</param>
        /// 
        public StatisticsService(IUnitOfWork repository, ILogger logger)
        {
            this.logger = logger;
            this.repository = repository;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="statistics">Обьект статистики, который нужно добавить в репозиторий.</param>
        /// <returns></returns>
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

        public async Task<int> CreateStatisticsAsync(UserStatistics statistics)
        {
            Users foundUser = await repository.Users.GetAsync(statistics.ID);

            if (foundUser != null)
            {
                _ = await repository.Statistics.DeleteAsync(statistics.ID);
                _ = await repository.Events.DeleteAsync(statistics.ID);

                await repository.Statistics.CreateAsync(statistics);
                await repository.Events.CreateAsync(statistics.Events);

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

            List<UserStatistics> statistics = this.repository.Statistics.GetList();

            if (statistics != null)
            {
                foreach (var stat in statistics)
                {
                    stat.Events = this.repository.Events.Get(stat.ID);
                }
            }

            return statistics;
        }

        public async Task<List<UserStatistics>> GetStatisticsAsync()
        {
            this.logger.Debug("Запрос списка статистик");

            List<UserStatistics> statistics = await repository.Statistics.GetListAsync();

            if (statistics != null)
            {
                foreach (var stat in statistics)
                {
                    stat.Events = await repository.Events.GetAsync(stat.ID);
                }
            }

            return statistics;
        }

        public UserStatistics GetStatistics(string ID)
        {
            this.logger.Debug("Запрос списка статистик {@Users}", ID);

            UserStatistics statistics = this.repository.Statistics.Get(ID);

            if (statistics != null)
            {
                statistics.Events = this.repository.Events.Get(statistics.ID);
            }

            return statistics;
        }

        public async Task<UserStatistics> GetStatisticsAsync(string ID)
        {
            this.logger.Debug("Запрос списка статистик {@Users}", ID);

            UserStatistics statistics = await repository.Statistics.GetAsync(ID);

            if (statistics != null)
            {
                statistics.Events = await repository.Events.GetAsync(statistics.ID);
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

        public async Task<int> DeleteStatisticsAsync(string ID)
        {
            Users foundUser = await repository.Users.GetAsync(ID);

            if (foundUser != null)
            {
                UserStatistics foundStatistics = await repository.Statistics.GetAsync(ID);

                if (foundStatistics != null)
                {
                    await repository.Statistics.DeleteAsync(ID);
                    await repository.Events.DeleteAsync(ID);

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
