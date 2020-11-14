using DatabaseManager.Models.Abstracts;
using DatabaseManager.Models.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
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

        public ICollection<Event> Events { get; set; }

        [NotMapped]
        public List<Event> Orders {
            get {
                return Events.Where(e => e.EventType == EventType.Order).ToList();
            }
        }
        [NotMapped]
        public List<Event> Refunds
        {
            get
            {
                return Events.Where(e => e.EventType == EventType.Refund).ToList();
            }
        }
        [NotMapped]
        public List<Book> OwnedBooks {
            get
            {
                return Orders
                    .Select(order => order.BookBundles)
                    .Except(
                        Refunds.Select(refund => refund.BookBundles))
                    .SelectMany(states => states)
                    .Select(state => state.Book)
                    .ToList();
            }
        }
    }
}
