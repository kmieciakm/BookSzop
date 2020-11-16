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
            Assert.Equal(2, _PurchaseService.GetAllOrders().Count);
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
    }
}
