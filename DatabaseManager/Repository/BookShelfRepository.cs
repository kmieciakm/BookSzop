using DatabaseManager.Models;
using DatabaseManager.Repository.Contracts;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace DatabaseManager.Repository
{
    public class BookShelfRepository : RepositoryBase<BookShelf>, IBookShelfRepository
    {
        public BookShelfRepository(DbContext dbContext) : base(dbContext)
        {
        }
    }
}
