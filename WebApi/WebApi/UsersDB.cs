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
            Users user = connection.Query<Users>("SELECT * FROM Users WHERE username = @nameOfNode", statistics).FirstOrDefault();

            //UserStatistics foundUser = connection.Query<UserStatistics>("SELECT * FROM Users WHERE nameOfNode = @nameOfNode", new { nameOfNode = user.NameOfNode }).FirstOrDefault();

            int status = 0;
            if (user == null)
            {
                connection.Execute("INSERT INTO Users (UserName) VALUES (@nameOfNode);", statistics);
                status = 1;
            }

            string sqlQuery = "INSERT INTO Statistics (nameOfNode, DateTimeOfLastStatistics, versionOfClient, typeOfDevice) VALUES(@nameOfNode, @DateTimeOfLastStatistics, @versionOfClient, @typeOfDevice);";
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
        /// <param name="newStatistics">
        /// Данные о пользовательской статистике, 
        /// которые нужно изменить.
        /// </param>
        /// <returns>
        /// Статус обновления записи:
        /// 1 - запись успешно обновлена,
        /// 0 - обновить запись не удалось.
        /// </returns>
        public int Update(UserStatisticsDTO statistics, UserStatisticsDTO newStatistics)
        {
            Users user = connection.Query<Users>("SELECT * FROM Users WHERE UserName = @nameOfNode", statistics).FirstOrDefault();

            //UserStatisticsDTO foundUser = connection.Query<UserStatisticsDTO>("SELECT * FROM NewUsers WHERE UserName = @UserName", new { UserName = user.NameOfNode }).FirstOrDefault();

            if (user != null)
            {
                if (user.UserName == newStatistics.NameOfNode)
                {
                    string sqlQuery = "UPDATE Statistics SET DateTimeOfLastStatistics = @DateTimeOfLastStatistics, VersionOfClient = @VersionOfClient, TypeOfDevice = @TypeOfDevice WHERE nameOfNode = @nameOfNode";
                    connection.Execute(sqlQuery, newStatistics);

                    return 1;
                }
                else
                {
                    string sqlQuery = "UPDATE Users SET username = @newname WHERE username = @oldname";
                    connection.Execute(sqlQuery, new { newname = newStatistics.NameOfNode, oldname = statistics.NameOfNode });
                    
                    sqlQuery = "UPDATE Statistics SET nameOfNode = @newname WHERE nameOfNode = @oldname";
                    connection.Execute(sqlQuery, new { newname = newStatistics.NameOfNode, oldname = statistics.NameOfNode });

                    sqlQuery = "UPDATE Statistics SET DateTimeOfLastStatistics = @newstat.DateTimeOfLastStatistics, VersionOfClient = @newstat.VersionOfClient, TypeOfDevice = @newstat.TypeOfDevice WHERE (nameOfNode = @oldstat.nameOfNode AND DateTimeOfLastStatistics = @oldstat.DateTimeOfLastStatistics AND versionOfClient = @oldstat.versionOfClient AND typeOfDevice = @oldstat.typeOfDevice)";
                    connection.Execute(sqlQuery, new { newstat = newStatistics, oldstat = statistics });

                    return 2;
                }
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

            string sqlQuery = "DELETE FROM Statistics WHERE (nameOfNode = @nameOfNode AND DateTimeOfLastStatistics = @DateTimeOfLastStatistics AND versionOfClient = @versionOfClient AND typeOfDevice = @typeOfDevice)";
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
            List<UserStatisticsDTO> statistics = connection.Query<UserStatisticsDTO>("SELECT * FROM Statistics").ToList();
            
            return statistics;
        }
    }
}
