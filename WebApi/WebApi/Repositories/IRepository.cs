// <copyright file="IRepository.cs" company="Infotecs">
// Copyright (c) Infotecs. All rights reserved.
// </copyright>

using System.Collections.Generic;

namespace Infotecs.WebApi.Repositories
{
    /// <summary>
    /// Интерфейс для взаимодействия с базой данных.
    /// </summary>
    public interface IRepository<T> where T : class
    {
        /// <summary>
        /// Метод получает из базы данных список всех пользователей.
        /// </summary>
        /// <returns>Список всех пользователей.</returns>
        List<T> GetList();

        /// <summary>
        /// Метод получает из базы данных данные по одному пользователю,
        /// имя которого передаётся в параметрах.
        /// </summary>
        /// <param Name="Name">Имя пользователя, статистику которого нужно получить.</param>
        /// <returns>Обьект пользовательской статистики для пользователя с именем "Name".</returns>
        T Get(string ID);

        int Create(T item);

        int Delete(string ID);
    }
}
