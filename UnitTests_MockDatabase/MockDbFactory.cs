using DatabaseManager;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Text;

namespace UnitTests_MockDatabase
{
    public class MockDbFactory : IDisposable
    {
        private DbConnection _connection;

        private DbContextOptions<MockDbContext> CreateOptions()
        {
            return new DbContextOptionsBuilder<MockDbContext>()
                .UseSqlite(_connection).Options;
        }

        public DbContextBase CreateMockDbContext()
        {
            if (_connection == null)
            {
                _connection = new SqliteConnection("DataSource=file::memory:");
                _connection.Open();

                var options = CreateOptions();
                using (var context = new MockDbContext(options))
                {
                    context.Database.EnsureCreated();
                    context.SeedData();
                }
            }

            var dbContext = new MockDbContext(CreateOptions());
            dbContext.SeedData();
            dbContext.SaveChanges();
            return dbContext;
        }

        public void Dispose()
        {
            if (_connection != null)
            {
                _connection.Dispose();
                _connection = null;
            }
        }
    }
}
