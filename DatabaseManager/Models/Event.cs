﻿using DatabaseManager.Models.Abstracts;
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

        public ICollection<BookOrder> OrderedBooks { get; set; }

        [NotMapped]
        public double Bill {
            get {
                return OrderedBooks.Sum(bookOrder => bookOrder.BookBundle.Price * bookOrder.Quantity);
            }
        }
        [NotMapped]
        public int BookAmount
        {
            get {
                return OrderedBooks.Sum(bundle => bundle.Quantity);
            }
        }

        public override bool Equals(object obj)
        {
            return obj is Event @event &&
                   PlacedDate == @event.PlacedDate &&
                   EventType == @event.EventType &&
                   OrderedBooks.SequenceEqual(@event.OrderedBooks);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(PlacedDate, EventType, OrderedBooks);
        }
    }
}
