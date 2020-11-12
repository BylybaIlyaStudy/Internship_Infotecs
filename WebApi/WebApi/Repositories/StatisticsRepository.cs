using Dapper;
using Infotecs.WebApi.Models;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Infotecs.WebApi.Repositories
{
    public class StatisticsRepository : IRepository<UserStatistics>
    {
        private readonly NpgsqlConnection connection = null;

        public StatisticsRepository(NpgsqlConnection connection)
        {
            this.connection = connection;
        }

        public int Create(UserStatistics item)
        {
            string sqlQuery = "INSERT INTO Statistics (ID, Name, Date, Version, OS) VALUES(@ID, @Name, @Date, @Version, @OS)";
            connection.Execute(sqlQuery, item);

            return 0;
        }

        public int Delete(string ID)
        {
            string sqlQuery = "DELETE FROM Statistics WHERE (ID = @ID)";
            connection.Execute(sqlQuery, new { ID = ID });

            return 0;
        }

        public UserStatistics Get(string ID)
        {
            UserStatistics foundStatistics = connection.Query<UserStatistics>("SELECT * FROM Statistics WHERE (ID = @ID)", new { ID = ID }).FirstOrDefault();

            return foundStatistics;
        }

        public List<UserStatistics> GetList()
        {
            List<UserStatistics> foundStatistics = connection.Query<UserStatistics>("SELECT * FROM Statistics").ToList();

            return foundStatistics;
        }
    }
}
