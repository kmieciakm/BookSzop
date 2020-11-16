﻿using DatabaseManager.Models;
using DatabaseManager.Models.Enums;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UnitTests_MockDatabase
{
    public class MockDbContext : DbContext
    {
        public DbSet<Book> Books { get; set; }
        public DbSet<BookBundle> BookBundles { get; set; }
        public DbSet<Event> Events { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<BookOrder> Orders { get; set; }


        public MockDbContext(DbContextOptions<MockDbContext> options) : base(options)
        {
        }

        public void SeedData()
        {
            ClearDatabase();
            SeedBooks();
            SeedBookBundles();
            SeedBookOrders();
            SeedEvents();
            SeedUsers();
        }

        private void ClearDatabase()
        {
            ClearSet(Books);
            ClearSet(BookBundles);
            ClearSet(Orders);
            ClearSet(Events);
            ClearSet(Users);
        }

        private void SeedBooks()
        {
            var books = new List<Book>()
            {
                new Book()
                {
                    Id = 1,
                    Title = "War and Peace",
                    Author = "Leo Tolstoy"
                },
                new Book()
                {
                    Id = 2,
                    Title = "Master and Margarita",
                    Author = "Mihail Bulhakov"
                }
            };
            Books.AddRange(books);
            SaveChanges();
        }

        private void SeedBookBundles()
        {
            var bookBundles = new List<BookBundle>()
            {
                new BookBundle()
                {
                    Id = 1,
                    BookId = 1,
                    Book = Books.FirstOrDefault(book => book.Id == 1), 
                    Price = 42,
                    Quantity = 120
                },
                new BookBundle()
                {
                    Id = 2,
                    BookId = 2,
                    Book = Books.FirstOrDefault(book => book.Id == 2),
                    Price = 100,
                    Quantity = 45
                }
            };
            BookBundles.AddRange(bookBundles);
            SaveChanges();
        }

        private void SeedBookOrders()
        {
            var orders = new List<BookOrder>()
            {
                new BookOrder()
                {
                    Id = 1,
                    BookBundleId = 1,
                    BookBundle = BookBundles.FirstOrDefault(bookBundle => bookBundle.Id == 1),
                    Quantity = 1
                },
                new BookOrder()
                {
                    Id = 2,
                    BookBundleId = 2,
                    BookBundle = BookBundles.FirstOrDefault(bookBundle => bookBundle.Id == 2),
                    Quantity = 1
                }
            };
            Orders.AddRange(orders);
            SaveChanges();
        }

        private void SeedEvents()
        {
            var orderedAndRefundBooks = new List<BookOrder>()
            {
                Orders.FirstOrDefault(order => order.Id == 1),
                Orders.FirstOrDefault(order => order.Id == 2)
            };

            var events = new List<Event>()
            {
                new Event()
                {
                    Id = 1,
                    UserId = 10,
                    User = new User() { Id = 10 },
                    EventType = EventType.Order,
                    OrderedBooks = orderedAndRefundBooks
                },
                new Event()
                {
                    Id = 2,
                    UserId = 20,
                    User = new User() { Id = 20 },
                    EventType = EventType.Refund,
                    OrderedBooks = orderedAndRefundBooks
                },
                new Event()
                {
                    Id = 3,
                    UserId = 30,
                    User = new User() { Id = 30 },
                    EventType = EventType.Order,
                    OrderedBooks = new List<BookOrder>()
                    {
                        Orders.FirstOrDefault(order => order.Id == 1)
                    }
                }
            };
            Events.AddRange(events);
            SaveChanges();
        }

        private void SeedUsers()
        {
            var users = new List<User>()
            {
                new User()
                {
                    Id = 1,
                    FirstName = "Nimda",
                    LastName = "The Admin",
                    Login = "admin",
                    Password = "admin123",
                    AdminPermission = true
                },
                new User()
                {
                    Id = 2,
                    FirstName = "Resu",
                    LastName = "The User",
                    Login = "user",
                    Password = "user123",
                    AdminPermission = false
                }       
            };
            Users.AddRange(users);
            SaveChanges();

            UpdateEvents();
        }

        private void UpdateEvents()
        {
            Events.FirstOrDefault(eve => eve.Id == 1).User = Users.FirstOrDefault(user => user.Id == 2);
            Events.FirstOrDefault(eve => eve.Id == 1).UserId = Users.FirstOrDefault(user => user.Id == 2).Id;
            Events.FirstOrDefault(eve => eve.Id == 2).User = Users.FirstOrDefault(user => user.Id == 2);
            Events.FirstOrDefault(eve => eve.Id == 2).UserId = Users.FirstOrDefault(user => user.Id == 2).Id;
            Events.FirstOrDefault(eve => eve.Id == 3).User = Users.FirstOrDefault(user => user.Id == 2);
            Events.FirstOrDefault(eve => eve.Id == 3).UserId = Users.FirstOrDefault(user => user.Id == 2).Id;

            SaveChanges();
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
