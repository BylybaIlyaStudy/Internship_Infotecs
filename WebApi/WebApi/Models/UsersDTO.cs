using System;

namespace Infotecs.WebApi.Models
{
    /// <summary>
    /// Класс DTO для передаче данных о пользователях.
    /// </summary>
    public class UsersDTO
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="UsersDTO"/> class.
        /// </summary>
        public UsersDTO() { }

        public UsersDTO(string Name)
        {
            this.Name = Name;
        }

        /// <summary>
        /// Уникальный идентификатор пользователя.
        /// </summary>
        public string ID { get; set; }

        /// <summary>
        /// Имя пользователя.
        /// </summary>
        public string Name { get; set; }
    }
}
