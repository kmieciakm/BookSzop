using DatabaseManager.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace DatabaseManager
{
    class SQLiteDbContext : DbContextBase
    {
        protected override void OnConfiguring(DbContextOptionsBuilder options)
            => options.UseSqlite("Data Source = szopDatabase.db");

        public override void SeedData()
        {
        }
    }
}
