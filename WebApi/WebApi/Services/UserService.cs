﻿using Infotecs.WebApi.Models;
using Mapster;
using Serilog;
using System.Collections.Generic;
using System.Threading.Tasks;
using WebApi.Repositories;

namespace Infotecs.WebApi.Services
{
    /// <summary>
    /// Класс для работы с данными о пользователях.
    /// </summary>
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

        public async Task<List<Users>> GetUsersListAsync()
        {
            this.logger.Debug("Асинхронный запрос списка пользователей");

            List<Users> users = await this.repository.Users.GetListAsync();

            return users;
        }

        public Users GetUser(string ID)
        {
            this.logger.Debug("Запрос списка пользователей"); //TODO: log

            Users user = this.repository.Users.Get(ID);

            return user;
        }

        public async Task<Users> GetUserAsync(string ID)
        {
            this.logger.Debug("Асинхронный запрос списка пользователей"); //TODO: log

            Users user = await this.repository.Users.GetAsync(ID);

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

        public async Task<int> CreateUserAsync(Users user)
        {
            Users foundUserByID = await repository.Users.GetAsync(user.ID);
            Users foundUserByName = (await repository.Users.GetListAsync()).Find(x => x.Name == user.Name);

            if (foundUserByID == null && foundUserByName == null)
            {
                await repository.Users.CreateAsync(user);

                return 200;
            }
            else
            {
                return 412;
            }
        }

        public async Task<int> UpdateUserAsync(Users user)
        {
            Users foundUser = await repository.Users.GetAsync(user.ID);
            UserStatistics userStatistics = user.Adapt<UserStatistics>();
            Users foundUserByName = (await repository.Users.GetListAsync()).Find(x => x.Name == user.Name);

            if (foundUser != null)
            {
                if (foundUserByName == null)
                {
                    await repository.Users.UpdateAsync(user);
                    await repository.Statistics.UpdateAsync(userStatistics);
                }
                else
                {
                    return 412;
                }

                return 200;
            }
            else
            {
                return 404;
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

        public async Task<int> DeleteUserAsync(string ID)
        {
            Users foundUser = await repository.Users.GetAsync(ID);

            if (foundUser != null)
            {
                await repository.Users.DeleteAsync(ID);
                await repository.Statistics.DeleteAsync(ID);
                await repository.Events.DeleteAsync(ID);

                return 200;
            }
            else
            {
                return 404;
            }
        }
    }
}
