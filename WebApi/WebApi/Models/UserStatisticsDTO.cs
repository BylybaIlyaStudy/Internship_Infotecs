using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApi.Models
{
    public class UserStatisticsDTO
    {
        public string NameOfNode { get; set; }
        public string DateTimeOfLastStatistics { get; set; }
        public string VersionOfClient { get; set; }
        public string TypeOfDevice { get; set; }

        public UserStatisticsDTO() {}

        public UserStatisticsDTO(string name, string date, string version, string type) 
        {
            this.NameOfNode = name;
            this.DateTimeOfLastStatistics = date;
            this.VersionOfClient = version;
            this.TypeOfDevice = type;
        }
    }
}
