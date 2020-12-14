using DatabaseManager.Models.Abstracts;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;

namespace DatabaseManager.Models
{
    [Serializable]
    public class Book : EntityBase
    {
        [MaxLength(128)]
        public string Title { get; set; }
        [MaxLength(128)]
        public string Author { get; set; }
        public bool IsAvailable { get; set; } = true;

        public override bool Equals(object obj)
        {
            return obj is Book book &&
                   Title == book.Title &&
                   Author == book.Author;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Title, Author);
        }
    }
}
