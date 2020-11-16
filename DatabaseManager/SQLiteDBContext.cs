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
        /// Purchases and Refunds
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
        /// <summary>
        /// Orders - element of single purchase that specifies the quantity and book bundle
        /// </summary>
        public DbSet<BookOrder> Orders { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
            => options.UseSqlite("Data Source = szopDatabase.db");
    }
}
