using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Infotecs.WebApi.Models
{
    public class Users
    {
        public Users() { }
        public Users(string name)
        {
            this.UserName = name;
        }

        public int ID { get; set; }
        public string UserName { get; set; }
    }
}
