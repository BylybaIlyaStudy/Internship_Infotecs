using System;

namespace Infotecs.WebApi.Models
{
    /// <summary>
    /// Класс модели событий.
    /// </summary>
    public class Events
    {
        /// <summary>
        /// ID пользователя, к которому относится событие.
        /// </summary>
        public string ID { get; set; }
        
        /// <summary>
        /// Дата события.
        /// </summary>
        public DateTime Date { get; set; }
        
        /// <summary>
        /// Название события.
        /// </summary>
        public string Name { get; set; }

        public string Description { get; set; }
    }
}
