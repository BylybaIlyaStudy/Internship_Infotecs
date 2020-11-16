using System;
using System.ComponentModel.DataAnnotations;

namespace Infotecs.WebApi.Models
{
    /// <summary>
    /// Класс DTO для событий.
    /// </summary>
    public class EventsDTO
    {
        /// <summary>
        /// Дата события.
        /// </summary>
        [DataType(DataType.DateTime)] public DateTime Date { get; set; }
        
        /// <summary>
        /// Название события. Не более 50 символов.
        /// </summary>
        [MaxLength(50)] public string Name { get; set; }
    }
}
