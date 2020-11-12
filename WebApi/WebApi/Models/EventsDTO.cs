using System;
using System.ComponentModel.DataAnnotations;

namespace Infotecs.WebApi.Models
{
    public class EventsDTO
    {
        [DataType(DataType.DateTime)] public DateTime Date { get; set; }
        [MaxLength(50)] public string Name { get; set; }
    }
}
