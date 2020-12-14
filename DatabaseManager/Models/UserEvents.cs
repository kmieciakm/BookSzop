using DatabaseManager.Models.Abstracts;
using System;
using System.Collections.Generic;
using System.Text;

namespace DatabaseManager.Models
{
    [Serializable]
    class UserEvents : EntityBase
    {
        public int UserId { get; set; }
        public User User { get; set; }
        public int EventId { get; set; }
        public Event Event { get; set; }
    }
}
