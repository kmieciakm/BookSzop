using DatabaseManager.Models;
using DatabaseManager.Repository.Contracts;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace DatabaseManager.Repository.Database
{
    public class BookOrderRepository : RepositoryBase<BookOrder>, IBookOrdersRepository
    {
        public BookOrderRepository(DbContext dbContext) : base(dbContext)
        {
        }
    }
}
