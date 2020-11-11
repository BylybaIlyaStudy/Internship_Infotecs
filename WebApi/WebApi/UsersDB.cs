// <copyright file="UsersDB.cs" company="Infotecs">
// Copyright (c) Infotecs. All rights reserved.
// </copyright>

using System.Collections.Generic;
using Infotecs.WebApi.Models;
using Dapper;
using System.Linq;
using Npgsql;
using Microsoft.Extensions.Configuration;

namespace Infotecs.WebApi
{
    /// <summary>
    /// Класс для работы с базой данных.
    /// </summary>
    public class UsersDB : IRepository
    {
        /// <summary>
        /// Подключение к базе данных.
        /// </summary>
        private readonly NpgsqlConnection connection = new NpgsqlConnection(Program.Configuration.GetConnectionString("DefaultConnection"));

        public int CreateStatistics(UserStatistics statistics)
        {
            string sqlQuery = "INSERT INTO Statistics (ID, name, date, version, os) VALUES(@ID, @name, @date, @version, @os)";
            connection.Execute(sqlQuery, statistics);

            if (statistics.Events != null)
            {
                foreach (var _event in statistics.Events)
                {
                    _event.UserID = statistics.UserID;

                    sqlQuery = "INSERT INTO Events (ID, name, date) VALUES(@ID, @name, @date)";
                    connection.Execute(sqlQuery, _event);
                }
            }

            return 0;
        }

        public int CreateUser(Users user)
        {
            string sqlQuery = "INSERT INTO Users (name, ID) VALUES(@name, @ID);";
            connection.Execute(sqlQuery, user);

            return 0;
        }

        public int DeleteStatistics(string ID)
        {
            string sqlQuery = "DELETE FROM Statistics WHERE (ID = @ID)";
            connection.Execute(sqlQuery, new { ID = ID });

            sqlQuery = "DELETE FROM Events WHERE (ID = @ID)";
            connection.Execute(sqlQuery, new { ID = ID });

            return 0;
        }

        public int DeleteUser(string ID)
        {
            string sqlQuery = "DELETE FROM Events WHERE (ID = @ID)";
            connection.Execute(sqlQuery, new { ID = ID });

            sqlQuery = "DELETE FROM Statistics WHERE (ID = @ID);";
            connection.Execute(sqlQuery, new { ID = ID });

            sqlQuery = "DELETE FROM Users WHERE (ID = @ID);";
            connection.Execute(sqlQuery, new { ID = ID });

            return 0;
        }

        /// <summary>
        /// Удаляет подключение к бд.
        /// </summary>
        public void Dispose()
        {
            connection.Close();
        }

        /// <summary>
        /// Получает список всех статистик.
        /// </summary>
        /// <returns>Список всех статистик.</returns>
        public List<UserStatistics> GetStatisticsList()
        {
            List<UserStatistics> foundStatistics = connection.Query<UserStatistics>("SELECT * FROM Statistics").ToList();

            if (foundStatistics != null)
            {
                foreach (var statistics in foundStatistics)
                {
                    statistics.Events = connection.Query<Events>("SELECT * FROM Events WHERE (ID = @ID)", statistics).ToList();
                }
            }

            return foundStatistics;
        }

        public UserStatistics GetStatistics(string ID)
        {
            UserStatistics foundStatistics = connection.Query<UserStatistics>("SELECT * FROM Statistics WHERE (ID = @ID)", new { ID = ID }).FirstOrDefault();

            if (foundStatistics != null)
            {
                foundStatistics.Events = connection.Query<Events>("SELECT * FROM Events WHERE (ID = @ID)", foundStatistics).ToList();
            }

            return foundStatistics;
        }

        /// <summary>
        /// Получает пользователя по ID.
        /// </summary>
        /// <param name="ID">ID пользователя.</param>
        /// <returns>Пользователь.</returns>
        public Users GetUser(string ID)
        {
            Users foundUser = connection.Query<Users>("SELECT * FROM Users WHERE ID = @ID", new { ID = ID }).FirstOrDefault();

            return foundUser;
        }

        /// <summary>
        /// Получает список всех пользователей.
        /// </summary>
        /// <returns>Список всех пользователей.</returns>
        public List<Users> GetUsersList()
        {
            List<Users> foundUsers = connection.Query<Users>("SELECT * FROM Users").ToList();

            return foundUsers;
        }
    }
}
