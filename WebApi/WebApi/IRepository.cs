using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApi.Models;

namespace WebApi
{
    public interface IRepository : IDisposable
    {
        List<UserStatistics> GetUsersList();
        UserStatistics GetUser(string name);
        bool Create(UserStatistics user);
        bool Update(UserStatistics user);
        bool Delete(string name);
    }
}
