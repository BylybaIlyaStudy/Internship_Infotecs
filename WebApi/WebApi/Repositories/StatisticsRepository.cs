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
    /// Репозиторий для работы с базой данных статистик.
    /// </summary>
    public class StatisticsRepository : IRepository<UserStatistics>
    {
        private readonly NpgsqlConnection connection = null;

        /// <summary>
        /// Initializes a new instance of the <see cref="EventsRepository"/> class.
        /// </summary>
        /// <param name="connection">Подключение к бд.</param>
        public StatisticsRepository(NpgsqlConnection connection)
        {
            this.connection = connection;
        }

        /// <summary>
        /// Метод добавляет в бд статистику.
        /// </summary>
        /// <param name="item">Статистика.</param>
        /// <returns>
        /// Статус добавления статистики: 0.
        /// </returns>
        public int Create(UserStatistics item)
        {
            string sqlQuery = "INSERT INTO Statistics (ID, Name, Date, Version, OS) VALUES(@ID, @Name, @Date, @Version, @OS)";
            connection.Execute(sqlQuery, item);

            return 0;
        }

        /// <summary>
        /// Метод асинхронно добавляет в бд статистику.
        /// </summary>
        /// <param name="item">Статистика.</param>
        /// <returns>Статус добавления статистики: 0.</returns>
        public async Task<int> CreateAsync(UserStatistics item)
        {
            string sqlQuery = "INSERT INTO Statistics (ID, Name, Date, Version, OS) VALUES(@ID, @Name, @Date, @Version, @OS)";
            await connection.ExecuteAsync(sqlQuery, item);

            return 0;
        }

        /// <summary>
        /// Удаление статистики из бд по ID.
        /// </summary>
        /// <param name="ID">ID пользователя, которому пренадлежит статистика.</param>
        /// <returns>
        /// Статус удаления статистики: 0.
        /// </returns>
        public int Delete(string ID)
        {
            string sqlQuery = "DELETE FROM Statistics WHERE (ID = @ID)";
            connection.Execute(sqlQuery, new { ID });

            return 0;
        }

        /// <summary>
        /// Асинхронное удаление статистики из бд по ID.
        /// </summary>
        /// <param name="ID">ID статистики, которую необходимо удалить.</param>
        /// <returns>Статус удаления статистики: 0.</returns>
        public async Task<int> DeleteAsync(string ID)
        {
            string sqlQuery = "DELETE FROM Statistics WHERE (ID = @ID)";
            await connection.ExecuteAsync(sqlQuery, new { ID });

            return 0;
        }

        /// <summary>
        /// Получение статистики по ID.
        /// </summary>
        /// <param name="ID">ID пользователя, которому пренадлежит статистика.</param>
        /// <returns>
        /// Статистика с совпадающим ID.
        /// </returns>
        public UserStatistics Get(string ID)
        {
            UserStatistics foundStatistics = connection.Query<UserStatistics>("SELECT * FROM Statistics WHERE (ID = @ID)", new { ID }).FirstOrDefault();

            return foundStatistics;
        }

        /// <summary>
        /// Асинхронное получение статистики по ID.
        /// </summary>
        /// <param name="ID">ID статистики.</param>
        /// <returns>
        /// Статистика с совпадающим ID.
        /// </returns>
        public async Task<UserStatistics> GetAsync(string ID)
        {
            IEnumerable<UserStatistics> foundStatistics = await connection.QueryAsync<UserStatistics>("SELECT * FROM Statistics WHERE (ID = @ID)", new { ID });

            return foundStatistics.FirstOrDefault();
        }

        /// <summary>
        /// Получение списка всех статистик.
        /// </summary>
        /// <returns>
        /// Список всех статистик.
        /// </returns>
        public List<UserStatistics> GetList()
        {
            List<UserStatistics> foundStatistics = connection.Query<UserStatistics>("SELECT * FROM Statistics").ToList();

            return foundStatistics;
        }

        /// <summary>
        /// Асинхронное получение списка всех статистик.
        /// </summary>
        /// <returns>
        /// Список всех статистик.
        /// </returns>
        public async Task<List<UserStatistics>> GetListAsync()
        {
            IEnumerable<UserStatistics> foundStatistics = await connection.QueryAsync<UserStatistics>("SELECT * FROM Statistics");

            return foundStatistics.ToList();
        }
    }
}
