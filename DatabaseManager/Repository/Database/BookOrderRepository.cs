using DatabaseManager.Models;
using DatabaseManager.Repository.Contracts;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DatabaseManager.Repository.Database
{
    class BookOrderRepository : RepositoryBase<BookOrder>, IBookOrderRepository
    {
        public BookOrderRepository(DbContextBase dbContext) : base(dbContext)
        {
        }

        public override BookOrder FindById(int id)
        {
            return _DbContext.Orders
                .Include(order => order.BookBundle)
                .ThenInclude(b => b.Book)
                .FirstOrDefault(order => order.Id == id);
        }

        public override ICollection<BookOrder> FindAll()
        {
            return _DbContext.Orders
              .Include(order => order.BookBundle)
              .ThenInclude(b => b.Book)
              .ToList();
        }
    }
}
