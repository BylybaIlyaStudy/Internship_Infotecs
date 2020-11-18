// <copyright file="UsersDB.cs" company="Infotecs">
// Copyright (c) Infotecs. All rights reserved.
// </copyright>

using Npgsql;
using Microsoft.Extensions.Configuration;
using Infotecs.WebApi.Repositories;
using WebApi.Repositories;
using Infotecs.WebApi.Models;
using System.Collections.Generic;

namespace Infotecs.WebApi
{
    /// <summary>
    /// Класс для работы с базой данных.
    /// </summary>
    public class UnitOfWork : IUnitOfWork
    {
        /// <summary>
        /// Подключение к базе данных.
        /// </summary>
        public static readonly NpgsqlConnection connection = new NpgsqlConnection(Program.Configuration.GetConnectionString("DefaultConnection"));

        private IRepository<Users> users = null;
        private IRepository<UserStatistics> statistics = null;
        private IRepository<List<Events>> events = null;

        public IRepository<Users> Users
        {
            get
            {
                if (users == null)
                    users = new UsersRepository(connection);
                return users;
            }
        }

        public IRepository<UserStatistics> Statistics
        {
            get
            {
                if (statistics == null)
                    statistics = new StatisticsRepository(connection);
                return statistics;
            }
        }

        public IRepository<List<Events>> Events
        {
            get
            {
                if (events == null)
                {
                    events = new EventsRepository(connection);
                }
                return events;
            }
        }
    }
}
