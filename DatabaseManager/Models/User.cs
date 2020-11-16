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
        public List<Event> Purchases {
            get {
                return Events?.Where(e => e.EventType == EventType.Purchase).ToList();
            }
        }
        [NotMapped]
        public List<Event> Refunds
        {
            get
            {
                return Events?.Where(e => e.EventType == EventType.Refund).ToList();
            }
        }
        [NotMapped]
        public List<Book> OwnedBooks {
            get
            {
                return Purchases
                    .Select(purchase => purchase.OrderedBooks)
                    .Except(
                        Refunds.Select(refund => refund.OrderedBooks))
                    .SelectMany(orders => orders)
                    .Select(order => order.BookBundle.Book)
                    .ToList();
            }
        }

        public override bool Equals(object obj)
        {
            return obj is User user &&
                   FirstName == user.FirstName &&
                   LastName == user.LastName &&
                   Login == user.Login &&
                   Password == user.Password &&
                   AdminPermission == user.AdminPermission &&
                   Events.SequenceEqual(user.Events);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(FirstName, LastName, Login, Password, AdminPermission, Events);
        }
    }
}
