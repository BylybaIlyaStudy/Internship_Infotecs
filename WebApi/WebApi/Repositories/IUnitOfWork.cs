using Infotecs.WebApi.Models;
using Infotecs.WebApi.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApi.Repositories
{
    public interface IUnitOfWork
    {
        public IRepository<Users> Users { get; }
        public IRepository<UserStatistics> Statistics { get; }
        public IRepository<List<Events>> Events { get; }
    }
}
