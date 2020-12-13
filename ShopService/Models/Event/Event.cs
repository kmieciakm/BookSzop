using ShopService.Models.BookModel;
using System;
using System.Collections.Generic;
using System.Text;

namespace ShopService.Models.EventModel
{
    class Event : IEvent
    {
        public int EventId { get; set; }
        public DateTime PlacedDate { get; set; }
        public int UserId { get; set; }
        public IEnumerable<IBook> OrderedBooks { get; set; }
        public double Bill { get; set; }
        public int BookAmount { get; set; }
    }
}
