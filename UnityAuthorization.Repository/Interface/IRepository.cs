using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace UnityAuthorization.Repository.Interface
{
    public interface IRepository<T> where T:class
    {
        void Add(T entity);
        Task AddAsync(T entity);
        int SaveAsync();
        Task<int> SaveChangeAsync();
        void Update(T entity);
    }
}
