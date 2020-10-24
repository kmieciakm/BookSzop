using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace DatabaseManager.Models
{
    public class User
    {
        [Key]
        public int Id { get; set; }
        [MaxLength(128)]
        public string FirstName { get; set; }
        [MaxLength(128)]
        public string LastName { get; set; }
        [MinLength(3), MaxLength(128)]
        public string Login { get; set; }
        [MinLength(8), MaxLength(128)]
        public string Password { get; set; }
        [MinLength(8), MaxLength(128)]
        public bool AdminPermission { get; set; }
    }
}
