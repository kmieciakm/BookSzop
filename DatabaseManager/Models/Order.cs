using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace DatabaseManager.Models
{
    [Serializable]
    public class Order : EntityBase
    {
        [ForeignKey("UserFK")]
        public User User { get; set; }
        public int UserFK { get; set; }
        public ICollection<Book> Books { get; set; }
        [Range(0, 100000)]
        public double Bill { get; set; }
        public OrderStatus Status { get; set; }
    }
}
