using DatabaseManager.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace DatabaseManager
{
    public class SQLiteDBContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        /// <summary>
        /// Orders and Refunds
        /// </summary>
        public DbSet<Event> Events { get; set; }
        /// <summary>
        /// Books descriptions
        /// </summary>
        public DbSet<Book> Catalogs { get; set; }
        /// <summary>
        /// Content of the shop, all available books with prices
        /// </summary>
        public DbSet<BookBundle> States { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
            => options.UseSqlite("Data Source = szopDatabase.db");
    }
}
