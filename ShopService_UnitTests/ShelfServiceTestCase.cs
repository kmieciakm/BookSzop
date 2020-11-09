using ShopService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xunit;

namespace ShopService_UnitTests
{
    public class ShelfServiceTestCase
    {
        public IShelfService ShelfService { get; }

        public ShelfServiceTestCase(IShelfService shelfService)
        {
            ShelfService = shelfService;
        }

        [Fact]
        public void GetUserBooks_BookCount()
        {
            Assert.Equal(2, ShelfService.GetUserBooks(2).Count);
        }

        [Fact]
        public void GetUserBooks_RightBook()
        {
            Assert.Equal("Master and Margarita", ShelfService.GetUserBooks(2).ElementAt(1).Title);
        }
    }
}
