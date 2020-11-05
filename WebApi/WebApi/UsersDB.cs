// <copyright file="UsersDB.cs" company="Infotecs">
// Copyright (c) Infotecs. All rights reserved.
// </copyright>

using System.Collections.Generic;
using Infotecs.WebApi.Models;
using Dapper;
using System.Linq;
using Npgsql;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;

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
        /// Метод пытается создать запись статистики. 
        /// Проверяет наличие пользователя, которому добавляется статистика и ищет возможные копии. 
        /// Возвращает код результата выполнения.
        /// </summary>
        /// <param name="statistics">Статистика.</param>
        /// <returns>
        /// Код результата выполнения:
        /// 0 - создана новая запись статистики;
        /// 1 - ошибка создания статистики: пользоавтель с таким ID не существует;
        /// 2 - ошибка создания статистики: статистика с такими данными уже существует.
        /// </returns>
        public int CreateStatistics(UserStatistics statistics)
        {
            Users foundUser = connection.Query<Users>("SELECT * FROM Users WHERE (ID = @UserID AND NameOfNode = @NameOfNode)", statistics).FirstOrDefault();

            if (foundUser == null)
            {
                return 1;
            }
            else
            {
                string sqlQuery = "SELECT * FROM Statistics WHERE (DateTimeOfLastStatistics = @DateTimeOfLastStatistics AND VersionOfClient = @VersionOfClient AND TypeOfDevice = @TypeOfDevice AND UserID = @UserID)";
                UserStatistics foundStatistics = connection.Query<UserStatistics>(sqlQuery, statistics).FirstOrDefault();

                if (foundStatistics != null)
                {
                    return 2;
                }
                else
                {
                    sqlQuery = "INSERT INTO Statistics (UserID, nameOfNode, DateTimeOfLastStatistics, versionOfClient, typeOfDevice, statisticsID) VALUES(@UserID, @NameOfNode, @DateTimeOfLastStatistics, @versionOfClient, @typeOfDevice, @statisticsID)";
                    connection.Execute(sqlQuery, statistics);

                    if (statistics.Events != null)
                    {
                        foreach (var _event in statistics.Events)
                        {
                            _event.StatisticsID = statistics.statisticsID;

                            sqlQuery = "INSERT INTO Events (statisticsID, name, date) VALUES(@statisticsID, @name, @date)";
                            connection.Execute(sqlQuery, _event);
                        }
                    }

                    return 0;
                }
            }
        }

        /// <summary>
        /// Метод пытается создать запись пользователя. 
        /// Проверяет уникальность ID пользователя. 
        /// Возвращает код результата выполнения.
        /// </summary>
        /// <param name="user">Пользователь.</param>
        /// <returns>
        /// Код результата выполнения:
        /// 0 - создана новая запись пользователя;
        /// 1 - ошибка создания пользователя: пользоавтель с таким ID уже существует.
        /// </returns>
        public int CreateUser(Users user)
        {
            Users foundUser = connection.Query<Users>("SELECT * FROM Users WHERE ID = @ID", user).FirstOrDefault();

            if (foundUser != null)
            {
                return 1;
            }
            else
            {
                string sqlQuery = "INSERT INTO Users (nameOfNode, ID) VALUES(@nameOfNode, @ID);";
                connection.Execute(sqlQuery, user);

                return 0;
            }
        }

        /// <summary>
        /// Метод пытается удалить запись статистики. 
        /// Проверяет наличие пользователя, для которого удаляется статистика и 
        /// проверяет наличие статистики. 
        /// Возвращает код результата выполнения.
        /// </summary>
        /// <param name="statistics">Статистика.</param>
        /// <returns>
        /// Код результата выполнения:
        /// 0 - запись статистики удалена;
        /// 1 - ошибка удаления статистики: пользоавтель с такими данными не существует;
        /// 2 - ошибка удаления статистики: статистика с такими данными не существует.
        /// </returns>
        public int DeleteStatistics(UserStatistics statistics)
        {
            Users foundUser = connection.Query<Users>("SELECT * FROM Users WHERE (ID = @UserID AND NameOfNode = @NameOfNode)", statistics).FirstOrDefault();

            if (foundUser == null)
            {
                return 1;
            }
            else
            {
                string sqlQuery = "SELECT * FROM Statistics WHERE (DateTimeOfLastStatistics = @DateTimeOfLastStatistics AND VersionOfClient = @VersionOfClient AND TypeOfDevice = @TypeOfDevice AND UserID = @UserID)";
                UserStatistics foundStatistics = connection.Query<UserStatistics>(sqlQuery, statistics).FirstOrDefault();
                   
                if (foundStatistics == null)
                {
                    return 2;
                }
                else
                {
                    sqlQuery = "DELETE FROM Statistics WHERE (UserID = @UserID AND DateTimeOfLastStatistics = @DateTimeOfLastStatistics AND versionOfClient = @versionOfClient AND typeOfDevice = @typeOfDevice)";
                    connection.Execute(sqlQuery, statistics);

                    sqlQuery = "DELETE FROM Events WHERE (statisticsID = @statisticsID)";
                    connection.Execute(sqlQuery, statistics);

                    return 0;
                }
            }
        }

        /// <summary>
        /// Метод пытается удалить запись пользователя, а также все его записи статистики. 
        /// Проверяет наличие пользователя. 
        /// Возвращает код результата выполнения.
        /// </summary>
        /// <param name="user">Пользователь.</param>
        /// <returns>
        /// Код результата выполнения:
        /// 0 - запись пользователя удалена;
        /// 1 - ошибка удаления пользователя: пользоавтель с такими данными не существует.
        /// </returns>
        public int DeleteUser(Users user)
        {
            Users foundUser = connection.Query<Users>("SELECT * FROM Users WHERE (ID = @ID AND NameOfNode = @NameOfNode)", user).FirstOrDefault();

            if (foundUser == null)
            {
                return 1;
            }
            else
            {
                string sqlQuery = "DELETE FROM Users WHERE ID = @ID;";
                connection.Execute(sqlQuery, user);

                List<UserStatistics> foundStatistics = connection.Query<UserStatistics>("SELECT * FROM Statistics").ToList();

                foreach (var statistic in foundStatistics)
                {
                    sqlQuery = "DELETE FROM Events WHERE (statisticsID = @statisticsID)";
                    connection.Execute(sqlQuery, statistic);
                }

                sqlQuery = "DELETE FROM Statistics WHERE UserID = @ID;";
                connection.Execute(sqlQuery, user);

                return 0;
            }
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
                    statistics.Events = connection.Query<Events>("SELECT * FROM Events WHERE (statisticsID = @statisticsID)", statistics).ToList();
                }
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
            Users foundUser = connection.Query<Users>("SELECT * FROM Users WHERE ID = @UserID", new { UserID = ID }).FirstOrDefault();

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

        /// <summary>
        /// Метод пытается обновить запись статистики. 
        /// Проверяет наличие пользователя, для которого обновляется статистика и 
        /// проверяет наличие статистики. Проверяет совпадение пользовательских данных
        /// в обновляемой и новой статистиках.
        /// Возвращает код результата выполнения.
        /// </summary>
        /// <param name="statistics">Статистика, которую нужно обновить.</param>
        /// <param name="newStatistics">Новая статистика.</param>
        /// <returns>
        /// Код результата выполнения:
        /// 0 - запись статистики обновлена;
        /// 1 - ошибка обновления статистики: пользоавтель с такими данными не существует;
        /// 2 - ошибка обновления статистики: статистика с такими данными не существует;
        /// 3 - ошибка обновления статистики: ID пользователя обновляемой статистики не совпадает с ID пользователя новой статистики;
        /// 4 - ошибка обновления статистики: имя пользователя обновляемой статистики не совпадает с именем пользователя новой статистики.
        /// </returns>
        public int UpdateStatistics(UserStatistics statistics, UserStatistics newStatistics)
        {
            Users foundUser = connection.Query<Users>("SELECT * FROM Users WHERE (ID = @UserID AND NameOfNode = @NameOfNode)", statistics).FirstOrDefault();

            if (foundUser == null)
            {
                return 1;
            }
            else
            {
                string sqlQuery = "SELECT * FROM Statistics WHERE (DateTimeOfLastStatistics = @DateTimeOfLastStatistics AND VersionOfClient = @VersionOfClient AND TypeOfDevice = @TypeOfDevice AND UserID = @UserID)";
                UserStatistics foundStatistics = connection.Query<UserStatistics>(sqlQuery, statistics).FirstOrDefault();

                if (foundStatistics == null)
                {
                    return 2;
                }
                else
                {
                    if (statistics.UserID != newStatistics.UserID)
                    {
                        return 3;
                    }
                    else
                    {
                        if (statistics.NameOfNode != newStatistics.NameOfNode) 
                        {
                            return 4;
                        }
                        else 
                        {
                            sqlQuery = "UPDATE Statistics SET DateTimeOfLastStatistics = @DateTimeOfLastStatistics, VersionOfClient = @VersionOfClient, TypeOfDevice = @TypeOfDevice statisticsID = @statisticsID WHERE UserID = @UserID";
                            connection.Execute(sqlQuery, newStatistics);

                            sqlQuery = "DELETE FROM Events WHERE (statisticsID = @statisticsID)";
                            connection.Execute(sqlQuery, statistics);

                            if (statistics.Events != null)
                            {
                                foreach (var _event in statistics.Events)
                                {
                                    _event.StatisticsID = statistics.statisticsID;

                                    sqlQuery = "INSERT INTO Events (statisticsID, name, date) VALUES(@statisticsID, @name, @date)";
                                    connection.Execute(sqlQuery, _event);
                                }
                            }

                            return 0; 
                        }

                    }
                }
            }
        }

        /// <summary>
        /// Метод пытается обновить запись пользователя, а также все его записи статистики. 
        /// Проверяет наличие пользователя. 
        /// Возвращает код результата выполнения.
        /// </summary>
        /// <param name="user">Пользователь.</param>
        /// <returns>
        /// Код результата выполнения:
        /// 0 - запись пользователя обновлена;
        /// 1 - ошибка обновления пользователя: пользоавтель с таким ID не существует.
        /// </returns>
        public int UpdateUser(Users user)
        {
            Users foundUser = connection.Query<Users>("SELECT * FROM Users WHERE ID = @ID", user).FirstOrDefault();

            if (foundUser == null)
            {
                return 1;
            }
            else
            {
                string sqlQuery = "UPDATE Users SET nameOfNode = @NameOfNode WHERE ID = @ID";
                connection.Execute(sqlQuery, user);

                sqlQuery = "UPDATE Statistics SET nameOfNode = @NameOfNode WHERE UserID = @ID;";
                connection.Execute(sqlQuery, user);

                return 0;
            }
        }
    }
}
