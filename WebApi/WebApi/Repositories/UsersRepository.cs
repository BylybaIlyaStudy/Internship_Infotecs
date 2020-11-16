using Dapper;
using Infotecs.WebApi.Models;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Infotecs.WebApi.Repositories
{
    public class UsersRepository : IRepository<Users>
    {
        private readonly NpgsqlConnection connection = null;

        public UsersRepository(NpgsqlConnection connection)
        {
            this.connection = connection;
        }

        public int Create(Users item)
        {
            string sqlQuery = "INSERT INTO Users (Name, ID) VALUES(@Name, @ID);";
            connection.Execute(sqlQuery, item);

            return 0;
        }

        public int Delete(string ID)
        {
            string sqlQuery = "DELETE FROM Users WHERE (ID = @ID);";
            connection.Execute(sqlQuery, new { ID = ID });

            return 0;
        }

        public Users Get(string ID)
        {
            Users foundUser = connection.Query<Users>("SELECT * FROM Users WHERE ID = @ID", new { ID = ID }).FirstOrDefault();

            return foundUser;
        }

        public List<Users> GetList()
        {
            List<Users> foundUsers = connection.Query<Users>("SELECT * FROM Users").ToList();

            return foundUsers;
        }
    }
}
