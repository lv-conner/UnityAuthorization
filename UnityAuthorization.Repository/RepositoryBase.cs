using System;
using System.Collections.Generic;
using System.Text;
using UnityAuthorization.Repository.Interface;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace UnityAuthorization.Repository
{
    public abstract class RepositoryBase<T> : IRepository<T> where T:class
    {
        protected readonly DbContext _dbContext;

        public RepositoryBase(DbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public virtual void Add(T entity)
        {
            _dbContext.Set<T>().Add(entity);
        }

        public virtual Task AddAsync(T entity)
        {
            return _dbContext.Set<T>().AddAsync(entity);
        }

        public virtual int SaveAsync()
        {
            return _dbContext.SaveChanges();
        }

        public virtual Task<int> SaveChangeAsync()
        {
            return _dbContext.SaveChangesAsync();
        }
    }
}
