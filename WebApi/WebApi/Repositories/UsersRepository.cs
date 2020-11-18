using Dapper;
using Infotecs.WebApi.Models;
using Npgsql;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Infotecs.WebApi.Repositories
{
    /// <summary>
    /// Репозиторий для работы с базой данных пользователей.
    /// </summary>
    public class UsersRepository : IRepository<Users>
    {
        private readonly NpgsqlConnection connection = null;

        /// <summary>
        /// Initializes a new instance of the <see cref="UsersRepository"/> class.
        /// </summary>
        /// <param name="connection">Подключение к бд.</param>
        public UsersRepository(NpgsqlConnection connection)
        {
            this.connection = connection;
        }

        /// <summary>
        /// Метод добавляет в бд пользователя.
        /// </summary>
        /// <param name="item">Пользователь.</param>
        /// <returns>
        /// Статус добавления пользователя: 0.
        /// </returns>
        public int Create(Users item)
        {
            string sqlQuery = "INSERT INTO Users (Name, ID) VALUES(@Name, @ID);";
            connection.Execute(sqlQuery, item);

            return 0;
        }

        /// <summary>
        /// Метод асинхронно добавляет в бд пользователя.
        /// </summary>
        /// <param name="item">Пользователь.</param>
        /// <returns>Статус добавления пользователя: 0.</returns>
        public async Task<int> CreateAsync(Users item)
        {
            string sqlQuery = "INSERT INTO Users (Name, ID) VALUES(@Name, @ID);";
            await connection.ExecuteAsync(sqlQuery, item);

            return 0;
        }

        /// <summary>
        /// Удаление пользователя из бд по ID.
        /// </summary>
        /// <param name="ID">ID пользователя, которого необходимо удалить.</param>
        /// <returns>
        /// Статус удаления пользователя: 0.
        /// </returns>
        public int Delete(string ID)
        {
            string sqlQuery = "DELETE FROM Users WHERE (ID = @ID);";
            connection.Execute(sqlQuery, new { ID });

            return 0;
        }

        /// <summary>
        /// Асинхронное удаление пользователя из бд по ID.
        /// </summary>
        /// <param name="ID">ID пользователя, которого необходимо удалить.</param>
        /// <returns>Статус удаления пользователя: 0.</returns>
        public async Task<int> DeleteAsync(string ID)
        {
            string sqlQuery = "DELETE FROM Users WHERE (ID = @ID);";
            await connection.ExecuteAsync(sqlQuery, new { ID });

            return 0;
        }

        /// <summary>
        /// Асинхронное удаление пользователя из бд по ID.
        /// </summary>
        /// <param name="ID">ID пользователя, которого необходимо удалить.</param>
        /// <returns>Статус удаления пользователя: 0.</returns>
        public async Task<int> DeleteAsync(string ID)
        {
            string sqlQuery = "DELETE FROM Users WHERE (ID = @ID);";
            await connection.ExecuteAsync(sqlQuery, new { ID = ID });

            return 0;
        }

        /// <summary>
        /// Получение пользователя по ID.
        /// </summary>
        /// <param name="ID">ID пользователя.</param>
        /// <returns>
        /// Пользователь с совпадающим ID.
        /// </returns>
        public Users Get(string ID)
        {
            Users foundUser = connection.Query<Users>("SELECT * FROM Users WHERE ID = @ID", new { ID }).FirstOrDefault();

            return foundUser;
        }

        /// <summary>
        /// Асинхронное получение пользователя по ID.
        /// </summary>
        /// <param name="ID">ID пользователя.</param>
        /// <returns>
        /// Пользователь с совпадающим ID.
        /// </returns>
        public async Task<Users> GetAsync(string ID)
        {
            IEnumerable<Users> foundUser = await connection.QueryAsync<Users>("SELECT * FROM Users WHERE ID = @ID", new { ID });

            return foundUser.FirstOrDefault();
        }

        /// <summary>
        /// Получение списка всех пользователей.
        /// </summary>
        /// <returns>
        /// Список всех пользователей.
        /// </returns>
        public List<Users> GetList()
        {
            List<Users> foundUsers = connection.Query<Users>("SELECT * FROM Users").ToList();

            return foundUsers;
        }

        /// <summary>
        /// Асинхронное получение списка всех пользователей.
        /// </summary>
        /// <returns>
        /// Список всех пользователей.
        /// </returns>
        public async Task<List<Users>> GetListAsync()
        {
            IEnumerable<Users> foundUsers = await connection.QueryAsync<Users>("SELECT * FROM Users");

            return foundUsers.ToList();
        }
    }
}
