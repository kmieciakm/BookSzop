using DatabaseManager.Repository.Contracts;
using DatabaseManager.Repository.Database;
using System;
using System.Collections.Generic;
using System.Text;

namespace DatabaseManager
{
    public static class DbFactory
    {
        public static DbContextBase CreateSQLiteDb()
            => new SQLiteDbContext();

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
