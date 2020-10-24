using DatabaseManager.Models;
using DatabaseManager.Repository.Contracts;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace DatabaseManager.Repository
{
    public class BookRepository : RepositoryBase<Book>, IBookRepository
    {
        public BookRepository(DbContext dbContext) : base(dbContext)
        {
        }
    }
}
