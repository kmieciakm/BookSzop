using ShopService.Models.BookOrderModel;
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
            Assert.Equal(3, _PurchaseService.GetAllPurchases().ToList().Count);
        }

        [Fact]
        public void GetAllRefunds()
        {
            Assert.Single(_PurchaseService.GetAllRefunds());
        }
        #endregion

        #region GetUserPurchases
        [Fact]
        public void GetUserPurchases_UserWithPlacedOrders()
        {
            Assert.Equal(2, _PurchaseService.GetUserPurchases(2).ToList().Count);
        }

        [Fact]
        public void GetUserPurchases_UserWithNoOrders()
        {
            Assert.False(_PurchaseService.GetUserPurchases(1).Any());
        }

        [Fact]
        public void GetUserPurchases_WrongUserId()
        {
            Assert.False(_PurchaseService.GetUserPurchases(999).Any());
        }
        #endregion

        #region GetUserRefunds
        [Fact]
        public void GetUserRefunds_UserWithRefunds()
        {
            Assert.Single(_PurchaseService.GetUserRefunds(2));
        }

        [Fact]
        public void GetUserRefunds_UserWithNoRefunds()
        {
            Assert.False(_PurchaseService.GetUserRefunds(1).Any());
        }

        [Fact]
        public void GetUserRefunds_WrongUserId()
        {
            Assert.False(_PurchaseService.GetUserRefunds(999).Any());
        }
        #endregion

        #region MakePurchase
        [Fact]
        public void MakePurchase_WrongUserId()
        {
            var orderedBooks = new List<IBookOrderCreate>()
            {
                new BookOrderCreate()
                {
                    BookBundleId = 1,
                    Quantity = 2
                }
            };

            Assert.Throws<PurchaseException>(() => _PurchaseService.MakePurchase(999, orderedBooks));
        }

        [Fact]
        public void MakePurchase_WrongBookBundleId()
        {
            var orderedBooks = new List<IBookOrderCreate>()
            {
                new BookOrderCreate()
                {
                    BookBundleId = 200,
                    Quantity = 2
                }
            };

            Assert.Throws<PurchaseException>(() => _PurchaseService.MakePurchase(1, orderedBooks));
        }

        [Fact]
        public void MakePurchase_TooManyBooksRequested()
        {
            var orderedBooks = new List<IBookOrderCreate>()
            {
                new BookOrderCreate()
                {
                    BookBundleId = 200,
                    Quantity = 2000
                }
            };

            Assert.Throws<PurchaseException>(() => _PurchaseService.MakePurchase(1, orderedBooks));
        }

        [Fact]
        public void MakePurchase_AllCorrect()
        {
            var orderedBooks = new List<IBookOrderCreate>()
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

            _PurchaseService.MakePurchase(1, orderedBooks);

            Assert.Single(_PurchaseService.GetUserPurchases(1));
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
            Assert.Single(_PurchaseService.GetUserRefunds(userId));
        }
        #endregion
    }
}
