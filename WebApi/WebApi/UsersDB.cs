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

        /// <summary>
        /// Метод создаёт в базе данных наличие пользователя, создаёт его в случае отсутствия и 
        /// создаёт новую запись о пользовательской статистике.
        /// </summary>
        /// <param name="statistics">
        /// Данные о пользовательской статистике, 
        /// которые нужно внести в базу.
        /// </param>
        /// <returns>
        /// Статус создания новой записи:
        /// 1 - создана запись о новом пользователе и добавлена статистика о нём;
        /// 0 - добавлена статистика о существующем пользователе.
        /// </returns>
        public int Create(UserStatisticsDTO statistics)
        {
            Users user = connection.Query<Users>("SELECT * FROM NewUsers WHERE username = @nameOfNode", statistics).FirstOrDefault();

            //UserStatistics foundUser = connection.Query<UserStatistics>("SELECT * FROM Users WHERE nameOfNode = @nameOfNode", new { nameOfNode = user.NameOfNode }).FirstOrDefault();

            int status = 0;
            if (user == null)
            {
                connection.Execute("INSERT INTO NewUsers (UserName) VALUES (@nameOfNode);", statistics);
                status = 1;
            }

            string sqlQuery = "INSERT INTO Users (nameOfNode, DateTimeOfLastStatistics, versionOfClient, typeOfDevice) VALUES(@nameOfNode, @DateTimeOfLastStatistics, @versionOfClient, @typeOfDevice);";
            connection.Execute(sqlQuery, statistics);

            return status;
        }

        /// <summary>
        /// Метод обновляет в базе данных запись о пользовательской статистике.
        /// </summary>
        /// <param name="statistics">
        /// Данные о пользовательской статистике, 
        /// которые нужно внести в базу.
        /// </param>
        /// <returns>
        /// Статус обновления записи:
        /// 1 - запись успешно обновлена,
        /// 0 - обновить запись не удалось.
        /// </returns>
        public int Update(UserStatisticsDTO statistics)
        {
            Users user = connection.Query<Users>("SELECT * FROM NewUsers WHERE UserName = @nameOfNode", statistics).FirstOrDefault();

            //UserStatisticsDTO foundUser = connection.Query<UserStatisticsDTO>("SELECT * FROM NewUsers WHERE UserName = @UserName", new { UserName = user.NameOfNode }).FirstOrDefault();

            if (user != null)
            {
                string sqlQuery = "UPDATE Users SET DateTimeOfLastStatistics = @DateTimeOfLastStatistics, VersionOfClient = @VersionOfClient, TypeOfDevice = @TypeOfDevice WHERE nameOfNode = @nameOfNode";
                connection.Execute(sqlQuery, statistics);

                return 1;
            }

            return 0;
        }

        /// <summary>
        /// Метод удаляет из базы данных запись о пользовательской статистике.
        /// </summary>
        /// <param name="statistics">
        /// Данные, которые необходимо удалить.
        /// </param>
        /// <returns>
        /// Статус удаления записи:
        /// Кол-во удалённых записей.
        /// </returns>
        public int Delete(UserStatisticsDTO statistics)
        {
            //Users foundUser = connection.Query<Users>("SELECT * FROM NewUsers WHERE UserName = @UserName", new { UserName = name }).FirstOrDefault();

            string sqlQuery = "DELETE FROM Users WHERE (nameOfNode = @nameOfNode AND DateTimeOfLastStatistics = @DateTimeOfLastStatistics AND versionOfClient = @versionOfClient AND typeOfDevice = @typeOfDevice)";
            return connection.Execute(sqlQuery, statistics);
        }

        /// <summary>
        /// Удаляет хранилище данных о пользователях.
        /// </summary>
        public virtual void Dispose()
        {
            connection.Close();
        }

        /// <summary>
        /// Метод получает из базы данных данные по одному пользователю,
        /// имя которого передаётся в параметрах.
        /// </summary>
        /// <param name="name">Имя пользователя, статистику которого нужно получить.</param>
        /// <returns>
        /// Обьект пользовательской статистики для пользователя с именем "name", если такой существует,
        /// Иначе null.
        /// </returns>
        public List<UserStatisticsDTO> GetUser(string name)
        {
            return null;
        }

        /// <summary>
        /// Метод получает из базы данных список всех пользователей.
        /// </summary>
        /// <returns>Список всех пользователей.</returns>
        public List<UserStatisticsDTO> GetUsersList()
        {
            List<UserStatisticsDTO> users = connection.Query<UserStatisticsDTO>("SELECT * FROM Users").ToList();
            
            return users;
        }
    }
}
