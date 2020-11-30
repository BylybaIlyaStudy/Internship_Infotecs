using Dapper;
using Infotecs.WebApi.Models;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Infotecs.WebApi.Repositories
{
    /// <summary>
    /// Репозиторий для работы с базой данных событй.
    /// </summary>
    public class EventsRepository : IRepository<List<Events>>
    {
        private readonly NpgsqlConnection connection = null;

        /// <summary>
        /// Initializes a new instance of the <see cref="EventsRepository"/> class.
        /// </summary>
        /// <param name="connection">Подключение к бд.</param>
        public EventsRepository(NpgsqlConnection connection)
        {
            this.connection = connection;
        }

        /// <summary>
        /// Метод добавляет в бд список событий.
        /// </summary>
        /// <param name="item">Список событий.</param>
        /// <returns>
        /// Статус добавления списка событий: 0.
        /// </returns>
        public int Create(List<Events> item)
        {
            if (item != null)
            {
                foreach (var e in item)
                {
                    string sqlQuery = "INSERT INTO Events (ID, Name, Date) VALUES(@ID, @Name, @Date)";
                    connection.Execute(sqlQuery, e);
                }
            }

            return 0;
        }

        /// <summary>
        /// Метод асинхронно добавляет в бд список событий.
        /// </summary>
        /// <param name="item">Список событий.</param>
        /// <returns>Статус добавления списка событий: 0.</returns>
        public async Task<int> CreateAsync(List<Events> item)
        {
            if (item != null)
            {
                foreach (var e in item)
                {
                    string sqlQuery = "INSERT INTO Events (ID, Name, Date) VALUES(@ID, @Name, @Date)";
                    await connection.ExecuteAsync(sqlQuery, e);
                }
            }

            return 0;
        }

        public async Task<int> UpdateAsync(List<Events> item)
        {
            string sqlQuery = "UPDATE Events SET Description = @Description WHERE Name = @Name AND ID = @ID";

            foreach (var i in item)
            {
                Console.WriteLine(i.ID);
                Console.WriteLine(i.Description);
            }

            foreach (var i in item)
            {
                await connection.ExecuteAsync(sqlQuery, i);
            }

            return 0;
        }

        /// <summary>
        /// Удаление событий из бд по ID.
        /// </summary>
        /// <param name="ID">ID пользователя, которому пренадлежат события.</param>
        /// <returns>
        /// Статус удаления списка событий: 0.
        /// </returns>
        public int Delete(string ID)
        {
            string sqlQuery = "DELETE FROM Events WHERE (ID = @ID)";
            connection.Execute(sqlQuery, new { ID });

            return 0;
        }

        /// <summary>
        /// Асинхронное удаление событий из бд по ID.
        /// </summary>
        /// <param name="ID">ID событий, которые необходимо удалить.</param>
        /// <returns>Статус удаления событий: 0.</returns>
        public async Task<int> DeleteAsync(string ID)
        {
            string sqlQuery = "DELETE FROM Events WHERE (ID = @ID)";
            await connection.ExecuteAsync(sqlQuery, new { ID });

            return 0;
        }

        /// <summary>
        /// Получение списка событий по ID.
        /// </summary>
        /// <param name="ID">ID пользователя, которому пренадлежат события.</param>
        /// <returns>
        /// Список событий с совпадающим ID.
        /// </returns>
        public List<Events> Get(string ID)
        {
            return connection.Query<Events>("SELECT * FROM Events WHERE (ID = @ID)", new { ID }).ToList();
        }

        /// <summary>
        /// Асинхронное получение списка событий по ID.
        /// </summary>
        /// <param name="ID">ID событий.</param>
        /// <returns>
        /// Список событий с совпадающим ID.
        /// </returns>
        public async Task<List<Events>> GetAsync(string ID)
        {
            IEnumerable<Events> foundEvents = await connection.QueryAsync<Events>("SELECT * FROM Events WHERE (ID = @ID)", new { ID });

            return foundEvents.ToList();
        }

        /// <summary>
        /// Получение списка списков всех событий. Не реализовано.
        /// </summary>
        /// <returns>
        /// Список списков всех событий сортированный по ID.
        /// </returns>
        public List<List<Events>> GetList()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Асинхронное получение списка списков всех событий. Не реализовано.
        /// </summary>
        /// <returns>
        /// Список списков всех событий сортированный по ID.
        /// </returns>
        public Task<List<List<Events>>> GetListAsync()
        {
            throw new NotImplementedException();
        }
    }
}
