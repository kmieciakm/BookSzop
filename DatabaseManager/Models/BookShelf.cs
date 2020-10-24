using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace DatabaseManager.Models
{
    public class BookShelf
    {
        [Key]
        public int Id { get; set; }
        [ForeignKey("UserFK")]
        public User User { get; set; }
        public int UserFK { get; set; }
        public ICollection<Book> Books { get; set; }
    }
}
