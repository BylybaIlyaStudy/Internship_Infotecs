// <copyright file="UsersDB.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace WebApi
{
    using System.Collections.Generic;
    using WebApi.Models;

    /// <summary>
    /// Класс для работы с базой данных о пользователях.
    /// </summary>
    public class UsersDB : IRepository
    {
        private List<UserStatistics> users = new List<UserStatistics>();

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
            if (!this.users.Exists(x => x.NameOfNode == user.NameOfNode))
            {
                this.users.Add(user);
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
            if (this.users.Exists(x => x.NameOfNode == name))
            {
                this.users.Remove(this.users.Find(x => x.NameOfNode == name));
                return true;
            }

            return false;
        }

        /// <summary>
        /// Удаляет хранилище данных о пользователях.
        /// </summary>
        public virtual void Dispose()
        {
            if (this.users != null)
            {
                if (this.users.Count > 0)
                {
                    this.users.Clear();
                }

                this.users = null;
            }
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
            if (this.users.Exists(x => x.NameOfNode == name))
            {
                return this.users.Find(x => x.NameOfNode == name);
            }

            return null;
        }

        /// <summary>
        /// Метод получает из базы данных список всех пользователей.
        /// </summary>
        /// <returns>Список всех пользователей.</returns>
        public List<UserStatistics> GetUsersList()
        {
            return this.users;
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
            if (this.users.Exists(x => x.NameOfNode == user.NameOfNode))
            {
                this.users[this.users.FindIndex(x => x.NameOfNode == user.NameOfNode)] = user;
                return true;
            }

            return false;
        }
    }
}
