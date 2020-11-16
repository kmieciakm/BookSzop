using DatabaseManager.Models;
using ShopService.Models;
using ShopService.StoreManagement;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xunit;
using Xunit.Priority;

namespace ShopService_UnitTests.TestCases
{
    [TestCaseOrderer(PriorityOrderer.Name, PriorityOrderer.Assembly)]
    [DefaultPriority(0)]
    public class StoreManagementTestCase
    {
        private IStoreManagementService _StoreManagementService { get; }

        public StoreManagementTestCase(IStoreManagementService storeManagementService)
        {
            _StoreManagementService = storeManagementService;
        }

        [Fact, Priority(-2)]
        public void GetAllBooks()
        {
            Assert.Equal(2, _StoreManagementService.GetAllBooks().Count);
        }

        [Fact, Priority(-2)]
        public void GetAllBookBundles()
        {
            Assert.Equal(2, _StoreManagementService.GetAllBookBundles().Count);
        }

        [Fact, Priority(-1)]
        public void RegisterBook()
        {
            var testBook = new Book()
            {
                Title = "TestTitle123",
                Author = "TestAuthor123"
            };
            _StoreManagementService.RegisterBook(testBook);
        
            Assert.Equal(3, _StoreManagementService.GetAllBooks().Count);
        }

        [Fact]
        public void RemoveBook()
        {
            var testBook = new Book()
            {
                Title = "TestTitle123",
                Author = "TestAuthor123"
            };

            _StoreManagementService.RegisterBook(testBook);
            _StoreManagementService.RemoveBook(3);

            Assert.Equal(2, _StoreManagementService.GetAllBooks().Count);
        }

        [Fact, Priority(-1)]
        public void RegisterBookBundle()
        {
            var testBookBundle = new BookBundle()
            {
                BookId = 2,
                Quantity = 15,
                Price = 15
            };
            _StoreManagementService.RegisterBookBundle(testBookBundle);

            Assert.Equal(3, _StoreManagementService.GetAllBookBundles().Count);
        }

        [Fact]
        public void RemoveBookBundle()
        {
            var testBookBundle = new BookBundle()
            {
                BookId = 2,
                Quantity = 15,
                Price = 15
            };
            _StoreManagementService.RegisterBookBundle(testBookBundle);
            _StoreManagementService.RemoveBookBundle(3);

            Assert.Equal(2, _StoreManagementService.GetAllBookBundles().Count);
        }
    }
}
