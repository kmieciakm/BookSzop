using DatabaseManager.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace DatabaseManager.Repository.Contracts
{
    public interface IUserRepository : IRepositoryBase<User>
    {
        User GetUserByLogin(string login);
    }
}
