using ShopService.Models.BookOrderModel;
using System;
using System.Collections.Generic;
using System.Text;

namespace BookSzop.Models
{
    public class BookOrderCreate : IBookOrderCreate
    {
        public int BookBundleId { get; set; }
        public int Quantity { get; set; }
    }
}
