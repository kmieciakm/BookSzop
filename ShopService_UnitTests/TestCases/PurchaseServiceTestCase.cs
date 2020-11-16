using DatabaseManager.Models;
using ShopService.Models;
using ShopService.Purchase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xunit;

namespace ShopService_UnitTests.TestCases
{
    public class PurchaseServiceTestCase
    {
        private IPurchaseService _PurchaseService { get; }

        public PurchaseServiceTestCase(IPurchaseService purchaseService)
        {
            _PurchaseService = purchaseService;
        }

        #region GetAll
        [Fact]
        public void GetAllOrders()
        {
            Assert.Equal(3, _PurchaseService.GetAllOrders().Count);
        }

        [Fact]
        public void GetAllRefunds()
        {
            Assert.Single(_PurchaseService.GetAllRefunds());
        }
        #endregion

        #region GetOrdersOfUser
        [Fact]
        public void GetOrdersOfUser_UserWithPlacedOrders()
        {
            Assert.Equal(2, _PurchaseService.GetOrdersOfUser(2).Count);
        }

        [Fact]
        public void GetOrdersOfUser_UserWithNoOrders()
        {
            Assert.False(_PurchaseService.GetOrdersOfUser(1).Any());
        }

        [Fact]
        public void GetOrdersOfUser_WrongUserId()
        {
            Assert.False(_PurchaseService.GetOrdersOfUser(999).Any());
        }
        #endregion

        #region GetRefundsOfUser
        [Fact]
        public void GetRefundsOfUser_UserWithRefunds()
        {
            Assert.Single(_PurchaseService.GetRefundsOfUser(2));
        }

        [Fact]
        public void GetRefundsOfUser_UserWithNoRefunds()
        {
            Assert.False(_PurchaseService.GetRefundsOfUser(1).Any());
        }

        [Fact]
        public void GetRefundsOfUser_WrongUserId()
        {
            Assert.False(_PurchaseService.GetRefundsOfUser(999).Any());
        }
        #endregion

        #region PlaceOrder
        [Fact]
        public void PlaceOrder_WrongUserId()
        {
            var ordersBooks = new List<BookOrderCreate>()
            {
                new BookOrderCreate()
                {
                    BookBundleId = 1,
                    Quantity = 2
                }
            };

            Assert.Throws<PurchaseException>(() => _PurchaseService.PlaceOrder(999, ordersBooks));
        }

        [Fact]
        public void PlaceOrder_WrongBookBundleId()
        {
            var ordersBooks = new List<BookOrderCreate>()
            {
                new BookOrderCreate()
                {
                    BookBundleId = 200,
                    Quantity = 2
                }
            };

            Assert.Throws<PurchaseException>(() => _PurchaseService.PlaceOrder(1, ordersBooks));
        }

        [Fact]
        public void PlaceOrder_TooManyBooksRequested()
        {
            var ordersBooks = new List<BookOrderCreate>()
            {
                new BookOrderCreate()
                {
                    BookBundleId = 200,
                    Quantity = 2000
                }
            };

            Assert.Throws<PurchaseException>(() => _PurchaseService.PlaceOrder(1, ordersBooks));
        }

        [Fact]
        public void PlaceOrder_AllCorrect()
        {
            var ordersBooks = new List<BookOrderCreate>()
            {
                new BookOrderCreate()
                {
                    BookBundleId = 1,
                    Quantity = 1
                },
                new BookOrderCreate()
                {
                    BookBundleId = 2,
                    Quantity = 1
                },
            };

            _PurchaseService.PlaceOrder(1, ordersBooks);

            Assert.Single(_PurchaseService.GetOrdersOfUser(1));
        }
        #endregion

        #region PlaceRefund
        [Fact]
        public void PlaceRefund_WrongUserId()
        {
            Assert.Throws<PurchaseException>(() => _PurchaseService.PlaceRefund(999, 1));
        }

        [Fact]
        public void PlaceRefund_WrongEventId()
        {
            Assert.Throws<PurchaseException>(() => _PurchaseService.PlaceRefund(1, 999));
        }

        [Fact]
        public void PlaceRefund_AllCorrect()
        {
            var userId = 3;
            _PurchaseService.PlaceRefund(userId, 4);
            Assert.Single(_PurchaseService.GetRefundsOfUser(userId));
        }
        #endregion
    }
}
