﻿using DatabaseManager.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DatabaseManager
{
    public class MockDbContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<Book> Books { get; set; }
        public DbSet<BookShelf> BookShelves { get; set; }

        public MockDbContext(DbContextOptions<MockDbContext> options) : base(options)
        {
        }

        private List<Book> _Books = new List<Book>()
        {
            new Book()
            {
                Title = "War and Peace",
                Author = "Leo Tolstoy",
                Price = 420
            },
            new Book()
            {
                Title = "Master and Margarita",
                Author = "Mihail Bulhakov",
                Price = 69
            }
        };

        public void SeedData()
        {
            SeedBooks();
            SeedBookShelves();
            SeedUsers();
        }

        private void SeedBooks()
        {
            ClearSet(Books);
            Books.AddRange(_Books);
        }

        private void SeedBookShelves()
        {
            ClearSet(BookShelves);
            BookShelves.AddRange(
                new List<BookShelf>() {
                    new BookShelf() {
                       Id = 1,
                       Books = new List<Book>()
                    },
                    new BookShelf() {
                       Id = 2,
                       Books = _Books
                    },
                    new BookShelf() {
                        Id = 3,
                        Books = new List<Book>()
                    }
                }
             );
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
                        BookShelfFK = 1,
                        AdminPermission = true
                    },
                    new User() {
                        Id = 2,
                        FirstName = "Resu",
                        LastName = "The User",
                        Login = "user",
                        Password = "user123",
                        BookShelfFK = 2,
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
