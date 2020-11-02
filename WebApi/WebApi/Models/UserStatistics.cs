// <copyright file="UserStatistics.cs" company="Infotecs">
// Copyright (c) Infotecs. All rights reserved.
// </copyright>

namespace Infotecs.WebApi.Models
{
    using System;

    /// <summary>
    /// Класс хранения пользовательской статистики.
    /// </summary>
    public class UserStatistics
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="UserStatistics"/> class.
        /// </summary>
        /// <param name="nameOfNode">Имя узла.</param>
        /// <param name="dateTime">Дата последней статистики.</param>
        /// <param name="versionOfClient">Версия приложения клиента.</param>
        /// <param name="typeOfDevice">Тип устройства клиента.</param>
        public UserStatistics(
            string nameOfNode,
            DateTime dateTime,
            string versionOfClient,
            string typeOfDevice)
        {
            this.NameOfNode = nameOfNode;
            this.DateTimeOfLastStatistics = dateTime.ToString();
            this.VersionOfClient = versionOfClient;
            this.TypeOfDevice = typeOfDevice;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="UserStatistics"/> class.
        /// </summary>
        /// <param name="nameOfNode">Имя узла.</param>
        /// <param name="versionOfClient">Версия приложения клиента.</param>
        /// <param name="typeOfDevice">Тип устройства клиента.</param>
        public UserStatistics(string nameOfNode, string versionOfClient, string typeOfDevice)
            : this(nameOfNode, DateTime.Now, versionOfClient, typeOfDevice) { }

        /// <summary>
        /// Initializes a new instance of the <see cref="UserStatistics"/> class.
        /// </summary>
        public UserStatistics()
            : this(string.Empty, DateTime.MinValue, string.Empty, string.Empty) { }
        /// <summary>
        /// Имя узла. Используется как уникальный идентификатор.
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
