using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ShopService.Models.BookBundleModel;
using ShopService.Models.BookModel;
using ShopService.Models.EventModel;

namespace ShopService.Models
{
    static class Mapper
    {
        public static Book DatabaseBookToServiceBook(DatabaseManager.Models.Book book)
        {
            return new Book()
            {
                Id = book.Id,
                Title = book.Title,
                Author = book.Author
            };
        }

        public static DatabaseManager.Models.Book ServiceBookToDatabaseBook(IBook book)
        {
            return new DatabaseManager.Models.Book()
            {
                Id = book.Id,
                Title = book.Title,
                Author = book.Author
            };
        }

        public static BookBundle DatabaseBookBundleToServiceBookBundle(DatabaseManager.Models.BookBundle bundle)
        {
            return new BookBundle()
            {
                Id = bundle.Id,
                Book = DatabaseBookToServiceBook(bundle.Book),
                BookId = bundle.BookId,
                Price = bundle.Price,
                Quantity = bundle.Quantity
            };
        }

        public static DatabaseManager.Models.BookBundle ServiceBookBundleToDatabaseBookBundle(IBookBundle bundle)
        {
            return new DatabaseManager.Models.BookBundle()
            {
                Id = bundle.Id,
                Book = bundle.Book == null ? null : ServiceBookToDatabaseBook(bundle.Book),
                BookId = bundle.BookId,
                Price = bundle.Price,
                Quantity = bundle.Quantity
            };
        }

        public static Event DatabaseEventToServiceEvent(DatabaseManager.Models.Event @event)
        {
            return new Event()
            {
                EventId = @event.Id,
                Bill = @event.Bill,
                UserId = @event.UserId,
                PlacedDate = @event.PlacedDate,
                BookAmount = @event.BookAmount,
                OrderedBooks = @event.OrderedBooks
                                .Select(bookOrder => bookOrder.BookBundle)
                                .Select(bundle => DatabaseBookToServiceBook(bundle.Book))
            };
        }
    }
}
