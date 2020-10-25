using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Text;

namespace DatabaseManager
{
    public class DbContextFactory : IDisposable
    {
        private DbConnection _connection;

        private DbContextOptions<MockDbContext> CreateOptions()
        {
            return new DbContextOptionsBuilder<MockDbContext>()
                .UseSqlite(_connection).Options;
        }

        public MockDbContext CreateMockDbContext()
        {
            if (_connection == null)
            {
                _connection = new SqliteConnection("DataSource=file::memory:?cache=shared");
                _connection.Open();

                var options = CreateOptions();
                using (var context = new MockDbContext(options))
                {
                    context.Database.EnsureCreated();
                    context.SeedData();
                    context.SaveChanges();
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
