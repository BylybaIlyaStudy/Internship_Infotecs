// <copyright file="UserStatistics.cs" company="Infotecs">
// Copyright (c) Infotecs. All rights reserved.
// </copyright>

using System;
using System.Collections.Generic;

namespace Infotecs.WebApi.Models
{
    /// <summary>
    /// Класс хранения пользовательской статистики.
    /// </summary>
    public class UserStatistics
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="UserStatistics"/> class.
        /// </summary>
        public UserStatistics() {}
        
        /// <summary>
        /// ID пользователя.
        /// </summary>
        public string UserID { get; set; }

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

        public List<Events> Events { get; set; }
    }
}
