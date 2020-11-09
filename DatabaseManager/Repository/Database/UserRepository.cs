using DatabaseManager.Models;
using DatabaseManager.Repository.Contracts;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace DatabaseManager.Repository.Database
{
    public class UserRepository : RepositoryBase<User>, IUserRepository
    {
        public UserRepository(DbContext dbContext) : base(dbContext)
        {
        }

        public User GetUserByLogin(string login)
        {
            var user = _DbContext.Set<User>()
                .FirstOrDefault(user => user.Login == login);
            return user;
        }

        public bool IsLoginFree(string login)
        {
            return GetUserByLogin(login) == null;
        }
    }
}
