using DatabaseManager.Models.Abstracts;
using DatabaseManager.Models.Enums;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;

namespace DatabaseManager.Models
{
    [Serializable]
    public class Event : EntityBase
    {
        public DateTime PlacedDate { get; set; }
        public EventType EventType { get; set; }

        [ForeignKey("UserId")]
        public User User { get; set; }
        public int UserId { get; set; }
        public ICollection<BookBundle> BookBundles { get; set; }

        [NotMapped]
        public double Bill { 
            get {
                return BookBundles.Sum(bundle => bundle.Quantity * bundle.Price);
            }
        }
        [NotMapped]
        public int BookAmount
        {
            get {
                return BookBundles.Sum(bundle => bundle.Quantity);
            }
        }
    }
}
