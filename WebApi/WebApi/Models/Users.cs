﻿namespace Infotecs.WebApi.Models
{
    /// <summary>
    /// Класс для хранения информации о пользователях.
    /// </summary>
    public class Users
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Users"/> class.
        /// </summary>
        public Users() { }

        /// <summary>
        /// Initializes a new instance of the <see cref="Users"/> class.
        /// </summary>
        /// <param name="name">Имя пользователя.</param>
        public Users(string name)
        {
            this.NameOfNode = name;
        }

        /// <summary>
        /// Уникальный идентификатор пользователя.
        /// </summary>
        public string ID { get; set; }

        /// <summary>
        /// Имя пользователя.
        /// </summary>
        public string NameOfNode { get; set; }
    }
}
