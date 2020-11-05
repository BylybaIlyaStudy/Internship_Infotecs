// <copyright file="IRepository.cs" company="Infotecs">
// Copyright (c) Infotecs. All rights reserved.
// </copyright>

namespace Infotecs.WebApi
{
    using System;
    using System.Collections.Generic;
    using Infotecs.WebApi.Models;

    /// <summary>
    /// Интерфейс для взаимодействия с базой данных.
    /// </summary>
    public interface IRepository : IDisposable
    {
        /// <summary>
        /// Метод получает из базы данных список всех пользователей.
        /// </summary>
        /// <returns>Список всех пользователей.</returns>
        List<Users> GetUsersList();

        /// <summary>
        /// Метод получает из базы данных данные по одному пользователю,
        /// имя которого передаётся в параметрах.
        /// </summary>
        /// <param name="name">Имя пользователя, статистику которого нужно получить.</param>
        /// <returns>Обьект пользовательской статистики для пользователя с именем "name".</returns>
        Users GetUser(string name);

        List<UserStatistics> GetStatisticsList();

        int CreateUser(Users user);

        int CreateStatistics(UserStatistics statistics);

        int UpdateUser(Users user);

        int UpdateStatistics(UserStatistics statistics, UserStatistics newStatistics);

        int DeleteUser(Users user);

        int DeleteStatistics(UserStatistics statistics);
    }
}
