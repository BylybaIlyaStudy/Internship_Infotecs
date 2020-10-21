using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApi.Models;

namespace WebApi
{
    interface IRepository : IDisposable
    {
        List<UserStatistics> GetUsersList();
        UserStatistics GetUser(string name);
        void Create(UserStatistics user);
        void Update(UserStatistics user);
        void Delete(string name);
    }
}
