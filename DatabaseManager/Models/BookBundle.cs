using DatabaseManager.Models.Abstracts;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace DatabaseManager.Models
{
    [Serializable]
    public class BookBundle : EntityBase
    {
        [ForeignKey("BookId")]
        public Book Book { get; set; }
        public int BookId { get; set; }
        public int Quantity { get; set; }
        [Range(0, 10000)]
        public double Price { get; set; }
        public bool IsAvailable { get; set; } = true;

        public override bool Equals(object obj)
        {
            return obj is BookBundle bundle &&
                   Book.Equals(bundle.Book) &&
                   BookId == bundle.BookId &&
                   Quantity == bundle.Quantity &&
                   Price == bundle.Price;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Book, BookId, Quantity, Price);
        }
    }
}
