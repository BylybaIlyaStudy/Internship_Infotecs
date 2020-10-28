// <copyright file="UserStatisticsDTO.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace WebApi.Models
{
    /// <summary>
    /// Класс DTO для передаче данных о пользователях.
    /// </summary>
    public class UserStatisticsDTO
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="UserStatisticsDTO"/> class.
        /// </summary>
        public UserStatisticsDTO() { }

        /// <summary>
        /// Initializes a new instance of the <see cref="UserStatisticsDTO"/> class.
        /// </summary>
        /// <param name="name">Имя узла.</param>
        /// <param name="date">Дата последней статистики.</param>
        /// <param name="version">Версия клиентского приложения.</param>
        /// <param name="type">Тип клиентского устройства.</param>
        public UserStatisticsDTO(string name, string date, string version, string type)
        {
            this.NameOfNode = name;
            this.DateTimeOfLastStatistics = date;
            this.VersionOfClient = version;
            this.TypeOfDevice = type;
        }

        /// <summary>
        /// Имя узла.
        /// </summary>
        public string NameOfNode { get; set; }

        /// <summary>
        /// Дата последней статистики.
        /// </summary>
        public string DateTimeOfLastStatistics { get; set; }

        /// <summary>
        /// Версия клиентского приложения.
        /// </summary>
        public string VersionOfClient { get; set; }

        /// <summary>
        /// Тип клиентского устройства.
        /// </summary>
        public string TypeOfDevice { get; set; }
    }
}
