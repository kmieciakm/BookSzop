using DatabaseManager.Models;
using DatabaseManager.Repository.Contracts;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace DatabaseManager.Repository.Database
{
    class EventsRepository : RepositoryBase<Event>, IEventsRepository
    {
        public EventsRepository(DbContext dbContext) : base(dbContext)
        {
        }
    }
}
