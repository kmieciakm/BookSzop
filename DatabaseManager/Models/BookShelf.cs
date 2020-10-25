﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace DatabaseManager.Models
{
    [Serializable]
    public class BookShelf : EntityBase
    {
        [ForeignKey("UserFK")]
        public User User { get; set; }
        public int? UserFK { get; set; }
        public ICollection<Book> Books { get; set; }
    }
}
