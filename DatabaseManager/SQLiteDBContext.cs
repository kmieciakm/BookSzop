using DatabaseManager.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace DatabaseManager
{
    class SQLiteDbContext : DbContextBase
    {
        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            var appPath = System.Reflection.Assembly.GetExecutingAssembly().Location;
            var dbPath = Path.Combine(Path.GetDirectoryName(appPath), "szopDatabase.db");
            options.UseSqlite("Data Source =" + dbPath);
        }
             

        public override void SeedData()
        {
        }
    }
}
