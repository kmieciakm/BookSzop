﻿using System;
using System.Collections.Generic;
using System.Text;

namespace BookSzop.Models
{
    public class BasketElement
    {
        public int BookBundleId { get; set; }
        public string BookTitle { get; set; }
        public double Price { get; set; }
        public int Quantity { get; set; } = 1;

        public void Increase() {
            Quantity++;
        }

        public void Decrease()
        {
            if (Quantity > 1)
            {
                Quantity--;
            }
        }
    }
}
