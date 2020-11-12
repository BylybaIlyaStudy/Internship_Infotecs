// <copyright file="UserStatisticsDTO.cs" company="Infotecs">
// Copyright (c) Infotecs. All rights reserved.
// </copyright>

using System.Collections.Generic;

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
        public string ID { get; set; }

        /// <summary>
        /// Имя узла.
        /// </summary>
        public string name { get; set; }

        /// <summary>
        /// Дата последней статистики.
        /// </summary>
        public string date { get; set; }

        /// <summary>
        /// Версия клиентского приложения.
        /// </summary>
        public string version { get; set; }

        /// <summary>
        /// Тип клиентского устройства.
        /// </summary>
        public string os { get; set; }

        public List<EventsDTO> EventsDTO { get; set; }
    }
}
