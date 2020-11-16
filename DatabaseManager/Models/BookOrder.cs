using DatabaseManager.Models.Abstracts;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace DatabaseManager.Models
{
    [Serializable]
    public class BookOrder : EntityBase
    {
        [Range(0, 1000)]
        public int Quantity { get; set; }

        [ForeignKey("BookBundleId")]
        public BookBundle BookBundle { get; set; }
        public int BookBundleId { get; set; }

        public override bool Equals(object obj)
        {
            return obj is BookOrder order &&
                   Quantity == order.Quantity &&
                   BookBundle.Equals(order.BookBundle) &&
                   BookBundleId == order.BookBundleId;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Quantity, BookBundle, BookBundleId);
        }
    }
}
