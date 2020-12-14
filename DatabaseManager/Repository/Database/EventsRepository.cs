using DatabaseManager.Models;
using DatabaseManager.Repository.Contracts;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DatabaseManager.Repository.Database
{
    class EventsRepository : RepositoryBase<Event>, IEventsRepository
    {
        public EventsRepository(DbContextBase dbContext) : base(dbContext)
        {
        }

        public override Event FindById(int id)
        {
            return _DbContext.Events
                 .Include(e => e.OrderedBooks)
                 .ThenInclude(o => o.BookBundle)
                 .ThenInclude(b => b.Book)
                 .FirstOrDefault(e => e.Id == id);
        }

        public override ICollection<Event> FindAll()
        {
            return _DbContext.Events
               .Include(e => e.OrderedBooks)
               .ThenInclude(o => o.BookBundle)
               .ThenInclude(b => b.Book)
               .ToList();
        }
    }
}
