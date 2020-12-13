using DatabaseManager.Models;
using DatabaseManager.Repository.Contracts;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace DatabaseManager.Repository.Database
{
    class BookBundleRepositiory : RepositoryBase<BookBundle>, IBookBundleRepositiory
    {
        public BookBundleRepositiory(DbContextBase dbContext) : base(dbContext)
        {
        }

        public bool SoftDelete(BookBundle bookBundleToDelete)
        {
            bookBundleToDelete.IsAvailable = false;
            _DbContext.Set<BookBundle>().Update(bookBundleToDelete);
            return Save();
        }

        public bool UpdateBundles(List<BookBundle> bundles)
        {
            foreach (var bundle in bundles)
            {
                _DbContext.Set<BookBundle>().Update(bundle);
            }
            return Save();
        }
    }
}
