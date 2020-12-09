using ShopService.Models.BookModel;
using System;
using System.Collections.Generic;
using System.Text;

namespace ShopService.Models.EventModel
{
    public interface IEvent
    {
        DateTime PlacedDate { get; set; }
        int UserId { get; set; }
        IEnumerable<IBook> OrderedBooks { get; set; }
        double Bill { get; set; }
        int BookAmount { get; set; }
    }
}
