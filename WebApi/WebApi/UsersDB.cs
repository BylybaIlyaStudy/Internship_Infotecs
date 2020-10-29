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
    /// Класс для работы с базой данных о пользователях.
    /// </summary>
    public class UsersDB : IRepository
    {
        
        /// <summary>
        /// Подключение к базе данных.
        /// </summary>
        private readonly NpgsqlConnection connection = new NpgsqlConnection(Program.Configuration.GetConnectionString("DefaultConnection"));

        /// <summary>
        /// Метод создаёт в базе данных новую запись о пользовательской статистике.
        /// </summary>
        /// <param name="user">
        /// Данные о пользовательской статистике, 
        /// которые нужно внести в базу.
        /// </param>
        /// <returns>
        /// Статус создания новой записи:
        /// 1 - запись успешно создана,
        /// 0 - создать запись не удалось.
        /// </returns>
        public bool Create(UserStatistics user)
        {
            UserStatistics foundUser = connection.Query<UserStatistics>("SELECT * FROM Users WHERE nameOfNode = @nameOfNode", new { nameOfNode = user.NameOfNode }).FirstOrDefault();

            if (foundUser == null)
            {
                string sqlQuery = "INSERT INTO Users (nameOfNode, DateTimeOfLastStatistics, versionOfClient, typeOfDevice) VALUES(@nameOfNode, @DateTimeOfLastStatistics, @versionOfClient, @typeOfDevice);";
                connection.Execute(sqlQuery, user);
                return true;
            }

            return false;
        }

        /// <summary>
        /// Метод удаляет из базы данных запись о пользовательской статистике.
        /// </summary>
        /// <param name="name">
        /// Имя пользователя, данные о котором необходимо удалить.
        /// </param>
        /// <returns>
        /// Статус удаления записи:
        /// 1 - запись успешно удалена,
        /// 0 - удалить запись не удалось.
        /// </returns>
        public bool Delete(string name)
        {
            UserStatistics foundUser = connection.Query<UserStatistics>("SELECT * FROM Users WHERE nameOfNode = @nameOfNode", new { nameOfNode = name }).FirstOrDefault();
            
            if (foundUser != null)
            {
                string sqlQuery = "DELETE FROM Users WHERE nameOfNode = @nameOfNode";
                connection.Execute(sqlQuery, new { nameOfNode = name });
                
                return true;
            }

            return false;
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
        public UserStatistics GetUser(string name)
        {
            UserStatistics foundUser = connection.Query<UserStatistics>("SELECT * FROM Users WHERE nameOfNode = @nameOfNode", new { nameOfNode = name }).FirstOrDefault();

            return foundUser;
        }

        /// <summary>
        /// Метод получает из базы данных список всех пользователей.
        /// </summary>
        /// <returns>Список всех пользователей.</returns>
        public List<UserStatistics> GetUsersList()
        {
            List<UserStatistics> users = connection.Query<UserStatistics>("SELECT * FROM Users").ToList();
            
            return users;
        }

        /// <summary>
        /// Метод обновляет в базе данных запись о пользовательской статистике.
        /// </summary>
        /// <param name="user">
        /// Данные о пользовательской статистике, 
        /// которые нужно внести в базу.
        /// </param>
        /// <returns>
        /// Статус обновления записи:
        /// 1 - запись успешно обновлена,
        /// 0 - обновить запись не удалось.
        /// </returns>
        public bool Update(UserStatistics user)
        {
            UserStatistics foundUser = connection.Query<UserStatistics>("SELECT * FROM Users WHERE nameOfNode = @nameOfNode", new { nameOfNode = user.NameOfNode }).FirstOrDefault();

            if (foundUser != null)
            {
                string sqlQuery = "UPDATE Users SET nameOfNode = @NameOfNode, DateTimeOfLastStatistics = @DateTimeOfLastStatistics, VersionOfClient = @VersionOfClient, TypeOfDevice = @TypeOfDevice WHERE nameOfNode = @nameOfNode";
                connection.Execute(sqlQuery, user);

                return true;
            }

            return false;
        }
    }
}
