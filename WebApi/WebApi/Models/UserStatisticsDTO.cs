// <copyright file="UserStatisticsDTO.cs" company="Infotecs">
// Copyright (c) Infotecs. All rights reserved.
// </copyright>

namespace Infotecs.WebApi.Models
{
    /// <summary>
    /// Класс DTO для передаче данных о статистике пользователей.
    /// </summary>
    public class UserStatisticsDTO
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="UserStatisticsDTO"/> class.
        /// </summary>
        public UserStatisticsDTO() { }

        /// <summary>
        /// ID узла.
        /// </summary>
        public string UserID { get; set; }

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
