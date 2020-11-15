using DatabaseManager.Models;
using DatabaseManager.Repository.Contracts;
using DatabaseManager.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xunit;

namespace DatabaseManager_UnitTests
{
    public class ModelsUtilsTestCase
    {
        private IBookBundleRepositiory _BookBundleRepository { get; }

        public ModelsUtilsTestCase(IBookBundleRepositiory bookBundleRepositiory)
        {
            _BookBundleRepository = bookBundleRepositiory;
        }

        [Fact]
        public void DeepClone()
        {
            var bundle = new BookBundle()
            {
                Id = 1,
                BookId = 1,
                Book = new Book()
                {
                    Id = 44,
                    Title = "Dziady",
                    Author = "Adaś",
                    BookBundles = new List<BookBundle>()
                },
                Price = 42,
                Quantity = 120
            };
            var bundleClone = ModelsUtils.DeepClone(bundle);
            Assert.NotSame(bundle, bundleClone);
            Assert.Equal(bundle, bundleClone);
        }
    }
}
