using Infotecs.WebApi.Models;
using Serilog;
using System.Collections.Generic;
using System.Threading.Tasks;
using WebApi.Repositories;

namespace Infotecs.WebApi.Services
{
    public class EventsService
    {
        private readonly ILogger logger = null;
        private readonly IUnitOfWork repository = null;

        /// <summary>
        /// Initializes a new instance of the <see cref="StatisticsService"/> class.
        /// </summary>
        /// <param name="logger">Интерфейс системы логирования.</param>
        /// <param name="repository">Интерфейс базы данных.</param>
        /// 
        public EventsService(IUnitOfWork repository, ILogger logger)
        {
            this.logger = logger;
            this.repository = repository;
        }

        public async Task<int> CreateEventsAsync(List<Events> events)
        {
            UserStatistics foundStatistics = await repository.Statistics.GetAsync(events[0].ID);

            if (foundStatistics != null)
            {
                _ = await repository.Events.DeleteAsync(events[0].ID);

                await repository.Events.CreateAsync(events);

                return 200;
            }
            else
            {
                return 404;
            }
        }

        public async Task<int> UpdateEvent(List<Events> events)
        {
            _ = await repository.Events.UpdateAsync(events);

            return 200;
        }

        public async Task<List<Events>> GetEventsAsync(string ID)
        {
            this.logger.Debug("Запрос списка событий {@Events}", ID);

            List<Events> events = await repository.Events.GetAsync(ID);

            return events;
        }

        public async Task<int> DeleteEventsAsync(string ID)
        {
            List<Events> foundEvents = await repository.Events.GetAsync(ID);

            if (foundEvents != null)
            {
                _ = await repository.Events.DeleteAsync(ID);

                return 200;
            }
            else
            {
                return 404;
            }
        }
    }
}
