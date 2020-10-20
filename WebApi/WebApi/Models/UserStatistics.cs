using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApi.Models
{
    public class UserStatistics
    {
        public string NameOfNode { get; private set; }
        public DateTime DateTimeOfLastStatistics { get; private set; }
        public string VersionOfClient { get; private set; }
        public string TypeOfDevice { get; private set; }

        public UserStatistics(string nameOfNode, DateTime dateTime, string versionOfClient, string typeOfDevice)
        {
            this.NameOfNode = nameOfNode;
            this.DateTimeOfLastStatistics = dateTime;
            this.VersionOfClient = versionOfClient;
            this.TypeOfDevice = typeOfDevice;
        }

        public UserStatistics(string nameOfNode, string versionOfClient, string typeOfDevice) :
            this(nameOfNode, DateTime.Now, versionOfClient, typeOfDevice)
        { }

        public UserStatistics() : this("", DateTime.MinValue, "", "") { }
    }
}
