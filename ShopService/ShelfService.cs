using DatabaseManager.Repository.Database;
using System;
using System.Collections.Generic;
using System.Text;
using DatabaseManager.Repository.Contracts;
using DatabaseManager.Models;

namespace ShopService
{
    public class ShelfService : IShelfService
    {
        private IBookShelfRepository _BookShelfRepository { get; }

        public ShelfService(IBookShelfRepository bookShelfRepository)
        {
            _BookShelfRepository = bookShelfRepository;
        }

        public List<Book> GetUserBooks(int userId)
        {
            var books = _BookShelfRepository.GetBooksByUser(userId);
            return books;
        }
    }
}
