using Dapper;
using Infotecs.WebApi.Models;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Infotecs.WebApi.Repositories
{
    public class EventsRepository : IRepository<List<Events>>
    {
        private readonly NpgsqlConnection connection = null;

        public EventsRepository(NpgsqlConnection connection)
        {
            this.connection = connection;
        }

        public int Create(List<Events> item)
        {
            if (item != null)
            {
                foreach (var e in item)
                {
                    string sqlQuery = "INSERT INTO Events (ID, Name, Date) VALUES(@ID, @Name, @Date)";
                    connection.Execute(sqlQuery, e);
                }
            }

            return 0;
        }

        public int Delete(string ID)
        {
            string sqlQuery = "DELETE FROM Events WHERE (ID = @ID)";
            connection.Execute(sqlQuery, new { ID = ID });

            return 0;
        }

        public List<Events> Get(string ID)
        {
            return connection.Query<Events>("SELECT * FROM Events WHERE (ID = @ID)", new { ID = ID }).ToList();
        }

        public List<List<Events>> GetList()
        {
            throw new NotImplementedException();
        }
    }
}
