﻿using DatabaseManager.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace DatabaseManager.Repository.Contracts
{
    public interface IBookRepository : IRepositoryBase<Book>
    {
        bool SoftDelete(Book bookToDelete);
    }
}
