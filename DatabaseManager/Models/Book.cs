using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace DatabaseManager.Models
{
    public class Book : EntityBase
    {
        [MaxLength(128)]
        public string Title { get; set; }
        [MaxLength(128)]
        public string Author { get; set; }
        [Range(0, 1000)]
        public float Price { get; set; }
    }
}
