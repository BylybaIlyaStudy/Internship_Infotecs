// <copyright file="IRepository.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
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
        List<UserStatistics> GetUsersList();

        /// <summary>
        /// Метод получает из базы данных данные по одному пользователю,
        /// имя которого передаётся в параметрах.
        /// </summary>
        /// <param name="name">Имя пользователя, статистику которого нужно получить.</param>
        /// <returns>Обьект пользовательской статистики для пользователя с именем "name".</returns>
        UserStatistics GetUser(string name);

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
        bool Create(UserStatistics user);

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
        bool Update(UserStatistics user);

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
        bool Delete(string name);
    }
}
