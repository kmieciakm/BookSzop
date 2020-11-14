using DatabaseManager.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ShopService_UnitTests.MockDb
{
    public class MockDbContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Event> Orders { get; set; }
        public DbSet<Book> Books { get; set; }

        public MockDbContext(DbContextOptions<MockDbContext> options) : base(options)
        {
        }

        private List<Book> _Books = new List<Book>()
        {
            new Book()
            {
                Title = "War and Peace",
                Author = "Leo Tolstoy"
            },
            new Book()
            {
                Title = "Master and Margarita",
                Author = "Mihail Bulhakov"
            }
        };

        public void SeedData()
        {
            SeedBooks();
            SeedUsers();
        }

        private void SeedBooks()
        {
            ClearSet(Books);
            Books.AddRange(_Books);
        }

        private void SeedUsers()
        {
            ClearSet(Users);
            Users.AddRange(
                new List<User>()
                {
                    new User() {
                        Id = 1,
                        FirstName = "Nimda",
                        LastName = "The Admin",
                        Login = "admin",
                        Password = "admin123",
                        AdminPermission = true
                    },
                    new User() {
                        Id = 2,
                        FirstName = "Resu",
                        LastName = "The User",
                        Login = "user",
                        Password = "user123",
                        AdminPermission = false
                    }
                }
            );
        }

        private void ClearSet<T>(DbSet<T> dbSet) where T : class
        {
            foreach (var entity in dbSet)
            {
                dbSet.Remove(entity);
            }
            SaveChanges();
        }
    }
}
