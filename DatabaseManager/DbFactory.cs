using DatabaseManager.Repository.Contracts;
using DatabaseManager.Repository.Database;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Text;

namespace DatabaseManager
{
    public class DbFactory
    {
        public static DbContextBase CreateSQLiteDb()
        {
            var db = new SQLiteDbContext();
            db.Database.EnsureCreated();
            return db;
        }

        public static IUserRepository CreateUserRepository(DbContextBase dbContext)
            => new UserRepository(dbContext);

        public static IEventsRepository CreateEventsRepository(DbContextBase dbContext)
            => new EventsRepository(dbContext);

        public static IBookRepository CreateBookRepository(DbContextBase dbContext)
            => new BookRepository(dbContext);

        public static IBookBundleRepositiory CreateBookBundleRepository(DbContextBase dbContext)
            => new BookBundleRepositiory(dbContext);

        public static IBookOrderRepository CreateBookOrderRepository(DbContextBase dbContext)
            => new BookOrderRepository(dbContext);
    }
}
