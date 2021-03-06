﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace ShopService.Models.BookOrderModel
{
    class BookOrderCreate : IBookOrderCreate
    {
        public int Quantity { get; set; }
        public int BookBundleId { get; set; }

        public override bool Equals(object obj)
        {
            return obj is BookOrderCreate order &&
                   BookBundleId == order.BookBundleId &&
                   Quantity == order.Quantity;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(BookBundleId, Quantity);
        }
    }
}
