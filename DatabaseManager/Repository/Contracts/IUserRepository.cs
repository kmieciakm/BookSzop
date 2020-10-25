﻿using DatabaseManager.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace DatabaseManager.Repository.Contracts
{
    public interface IUserRepository : IRepositoryBase<User>
    {
        bool CheckUserCredentials(string login, string password);
    }
}
