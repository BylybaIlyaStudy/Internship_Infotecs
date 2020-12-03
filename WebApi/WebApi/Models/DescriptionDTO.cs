using System;
using System.ComponentModel.DataAnnotations;

namespace Infotecs.WebApi.Models
{
    public class DescriptionDTO
    {
        public string ID { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Date { get; set; }

        public string Level { get; set; }
    }
}
