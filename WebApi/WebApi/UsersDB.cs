using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApi.Models;

namespace WebApi
{
    public class UsersDB : IRepository
    {
        private List<UserStatistics> users = new List<UserStatistics>();

        public bool Create(UserStatistics user)
        {
            if (!users.Exists(x => x.NameOfNode == user.NameOfNode))
            {
                users.Add(user);
                return true;
            }
            return false;
        }

        public bool Delete(string name)
        {
            if (users.Exists(x => x.NameOfNode == name))
            {
                users.Remove(users.Find(x => x.NameOfNode == name));
                return true;
            }
            return false;
        }

        public virtual void Dispose()
        {
            if (users != null)
            {
                if (users.Count > 0)
                {
                    users.Clear();
                }
                users = null;
            }
        }

        public UserStatistics GetUser(string name)
        {
            if (users.Exists(x => x.NameOfNode == name))
            {
                return users.Find(x => x.NameOfNode == name);
            }

            return null;
        }

        public List<UserStatistics> GetUsersList()
        {
            return users;
        }

        public bool Update(UserStatistics user)
        {
            if (users.Exists(x => x.NameOfNode == user.NameOfNode))
            {
                users[users.FindIndex(x => x.NameOfNode == user.NameOfNode)] = user;
                return true;
            }
            return false;
        }
    }
}
