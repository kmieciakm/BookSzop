using DatabaseManager.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace DatabaseManager.Repository.Contracts
{
    public interface IBookShelfRepository : IRepositoryBase<BookShelf>
    {
        List<Book> GetBooksByUser(int userId);
    }
}
