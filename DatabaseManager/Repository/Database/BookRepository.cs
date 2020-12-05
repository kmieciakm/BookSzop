using DatabaseManager.Models;
using DatabaseManager.Repository.Contracts;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace DatabaseManager.Repository.Database
{
    class BookRepository : RepositoryBase<Book>, IBookRepository
    {
        public BookRepository(DbContextBase dbContext) : base(dbContext)
        {
        }
    }
}
