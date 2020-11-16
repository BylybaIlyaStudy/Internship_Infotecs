using Infotecs.WebApi.Models;
using Serilog;
using System.Collections.Generic;
using WebApi.Repositories;

namespace Infotecs.WebApi.Services
{
    public class UserService
    {
        private readonly ILogger logger = null;
        private readonly IUnitOfWork repository = null;

        public UserService(IUnitOfWork repository, ILogger logger)
        {
            this.logger = logger;
            this.repository = repository;
        }

        public List<Users> GetUsersList()
        {
            this.logger.Debug("Запрос списка пользователей");

            List<Users> users = this.repository.Users.GetList();

            return users;
        }

        public Users GetUser(string ID)
        {
            this.logger.Debug("Запрос списка пользователей"); //TODO: log

            Users user = this.repository.Users.Get(ID);

            return user;
        }

        public int CreateUser(Users user) //TODO: log
        {
            Users foundUser = repository.Users.Get(user.ID);

            if (foundUser == null)
            {
                repository.Users.Create(user);

                return 200;
            }
            else
            {
                return 412;
            }
        }

        public int DeleteUser(string ID)
        {
            Users foundUser = repository.Users.Get(ID);

            if (foundUser != null)
            {
                repository.Users.Delete(ID);
                repository.Statistics.Delete(ID);
                repository.Events.Delete(ID);

                return 200;
            }
            else
            {
                return 404;
            }
        }
    }
}
