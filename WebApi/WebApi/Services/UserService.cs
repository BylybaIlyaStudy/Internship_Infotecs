using Infotecs.WebApi.Models;
using Serilog;
using System.Collections.Generic;

namespace Infotecs.WebApi.Services
{
    public class UserService
    {
        private readonly ILogger logger = null;
        private readonly IRepository repository = null;

        public UserService(IRepository repository, ILogger logger)
        {
            this.logger = logger;
            this.repository = repository;
        }

        public List<Users> GetUsersList()
        {
            this.logger.Debug("Запрос списка пользователей");

            List<Users> users = this.repository.GetUsersList();

            return users;
        }

        public Users GetUser(string ID)
        {
            this.logger.Debug("Запрос списка пользователей"); //TODO: log

            Users user = this.repository.GetUser(ID);

            return user;
        }

        public int CreateUser(Users user) //TODO: log
        {
            Users foundUser = repository.GetUser(user.ID);

            if (foundUser == null)
            {
                repository.CreateUser(user);

                return 200;
            }
            else
            {
                return 412;
            }
        }

        public int DeleteUser(string ID)
        {
            Users foundUser = repository.GetUser(ID);

            if (foundUser != null)
            {
                repository.DeleteUser(ID);

                return 200;
            }
            else
            {
                return 404;
            }
        }
    }
}
