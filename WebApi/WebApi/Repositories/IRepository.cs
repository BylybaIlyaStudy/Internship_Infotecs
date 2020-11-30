// <copyright file="IRepository.cs" company="Infotecs">
// Copyright (c) Infotecs. All rights reserved.
// </copyright>

using System.Collections.Generic;
using System.Threading.Tasks;

namespace Infotecs.WebApi.Repositories
{
    /// <summary>
    /// Интерфейс для взаимодействия с базой данных.
    /// </summary>
    public interface IRepository<T> where T : class
    {
        /// <summary>
        /// Метод получает из базы данных список всех объектов.
        /// </summary>
        /// <returns>Список всех объектов.</returns>
        List<T> GetList();

        Task<List<T>> GetListAsync();

        /// <summary>
        /// Метод получает из базы данных объект,
        /// ID которого передаётся в параметрах.
        /// </summary>
        /// <param name="ID">ID объекта, который нужно получить.</param>
        /// <returns>Обьект с ID "ID".</returns>
        T Get(string ID);

        Task<T> GetAsync(string ID);

        /// <summary>
        /// Метод создаёт объект в базе данных.
        /// </summary>
        /// <param name="item">Объект.</param>
        /// <returns>Статус создания объекта: 0.</returns>
        int Create(T item);

        Task<int> CreateAsync(T item);

        Task<int> UpdateAsync(T item);

        /// <summary>
        /// Метод удаляет объект из базы данных.
        /// </summary>
        /// <param name="ID">ID объекта.</param>
        /// <returns>Статус удаления объекта: 0.</returns>
        int Delete(string ID);

        Task<int> DeleteAsync(string ID);
    }
}
