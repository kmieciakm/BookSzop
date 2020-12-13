using DatabaseManager.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace DatabaseManager.Repository.Contracts
{
    public interface IBookBundleRepositiory : IRepositoryBase<BookBundle>
    {
        bool UpdateBundles(List<BookBundle> bundles);
        bool SoftDelete(BookBundle bookBundleToDelete);
    }
}
