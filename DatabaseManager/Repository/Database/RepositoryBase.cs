using DatabaseManager.Models;
using DatabaseManager.Repository.Contracts;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DatabaseManager.Repository.Database
{
    public abstract class RepositoryBase<T> : IRepositoryBase<T> where T : EntityBase
    {
        protected DbContext _DbContext { get; }

        public RepositoryBase(DbContext dbContext)
        {
            _DbContext = dbContext;
        }

        public virtual ICollection<T> FindAll()
        {
            return _DbContext.Set<T>().ToList();
        }

        public virtual T FindById(int id)
        {
            return _DbContext.Set<T>().FirstOrDefault(element => element.Id == id);
        }

        public bool Create(T entity)
        {
            _DbContext.Set<T>().Add(entity);
            return Save();
        }

        public bool Update(T entity)
        {
            _DbContext.Set<T>().Update(entity);
            return Save();
        }

        public bool Delete(T entity)
        {
            _DbContext.Set<T>().Remove(entity);
            return Save();
        }

        public bool Exists(int id)
        {
            return _DbContext.Set<T>().Any(element => element.Id == id);
        }

        protected bool Save()
        {
            return _DbContext.SaveChanges() > 0;
        }
    }
}
