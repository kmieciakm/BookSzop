﻿using DatabaseManager.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace DatabaseManager.Repository.Contracts
{
    public interface IBookBundleRepositiory : IRepositoryBase<BookBundle>
    {
        public bool UpdateBundles(List<BookBundle> bundles);
    }
}
