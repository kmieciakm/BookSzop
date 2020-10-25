using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace DatabaseManager.Models
{
    [Serializable]
    public class User : EntityBase
    {
        [MaxLength(128)]
        public string FirstName { get; set; }
        [MaxLength(128)]
        public string LastName { get; set; }
        [MinLength(3), MaxLength(128)]
        public string Login { get; set; }
        [MinLength(8), MaxLength(128)]
        public string Password { get; set; }
        public bool AdminPermission { get; set; }
        public ICollection<Order> Orders { get; set; }
        [ForeignKey("BookShelfFK")]
        public BookShelf BookShelf { get; set; }
        public int BookShelfFK { get; set; }
    }
}
