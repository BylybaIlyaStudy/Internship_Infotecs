﻿// <copyright file="UserStatistics.cs" company="Infotecs">
// Copyright (c) Infotecs. All rights reserved.
// </copyright>

namespace Infotecs.SPA_blazor.Data
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// Класс хранения пользовательской статистики.
    /// </summary>
    public class UserStatistics
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="UserStatistics"/> class.
        /// </summary>
        public UserStatistics() { }
        
        /// <summary>
        /// ID пользователя.
        /// </summary>
        public string ID { get; set; }

        /// <summary>
        /// Имя узла.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Дата последней статистики.
        /// </summary>
        public string Date { get; set; }

        /// <summary>
        /// Версия клиентского приложения.
        /// </summary>
        public string Version { get; set; }

        /// <summary>
        /// Тип клиентского устройства.
        /// </summary>
        public string OS { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public List<Events> Events { get; set; }
    }
}
