using DatabaseManager.Models;
using DatabaseManager.Repository.Contracts;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DatabaseManager.Repository.Database
{
    public class BookShelfRepository : RepositoryBase<BookShelf>, IBookShelfRepository
    {
        public BookShelfRepository(DbContext dbContext) : base(dbContext)
        {
        }

        public List<Book> GetBooksByUser(int userId)
        {
            var bookshelves = _DbContext.Set<BookShelf>();
            var users = _DbContext.Set<User>();

            var booksAndUsers = users
                .Join(
                    bookshelves,
                    user => user.BookShelfFK,
                    bookshelf => bookshelf.Id,
                    (user, bookshelf) => new
                    {
                        UserId = user.Id,
                        BookList = bookshelf.Books
                    }

                ).ToList();

            var specificUserBooks = booksAndUsers.FirstOrDefault(books => books.UserId == userId).BookList.ToList();

            return specificUserBooks;
        }
    }
}
