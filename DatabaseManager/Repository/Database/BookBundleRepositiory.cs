using DatabaseManager.Models;
using DatabaseManager.Repository.Contracts;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace DatabaseManager.Repository.Database
{
    public class BookBundleRepositiory : RepositoryBase<BookBundle>, IBookBundleRepositiory
    {
        public BookBundleRepositiory(DbContext dbContext) : base(dbContext)
        {
        }
    }
}
