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
    class UserRepository : RepositoryBase<User>, IUserRepository
    {
        public UserRepository(DbContextBase dbContext) : base(dbContext)
        {
        }

        public override User FindById(int id)
        {
            return _DbContext.Users
                .Include(u => u.Events)
                .ThenInclude(e => e.OrderedBooks)
                .ThenInclude(o => o.BookBundle)
                .ThenInclude(b => b.Book)
                .FirstOrDefault(user => user.Id == id);
        }

        public override ICollection<User> FindAll()
        {
            return _DbContext.Users
                .Include(u => u.Events)
                .ThenInclude(e => e.OrderedBooks)
                .ThenInclude(o => o.BookBundle)
                .ThenInclude(b => b.Book)
                .ToList();
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
