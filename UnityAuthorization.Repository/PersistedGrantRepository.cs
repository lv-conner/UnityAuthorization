using IdentityServer4.EntityFramework.DbContexts;
using System;
using System.Collections.Generic;
using System.Text;
using UnityAuthorization.Repository.Interface;

namespace UnityAuthorization.Repository
{
    public class PersistedGrantRepository<T> : RepositoryBase<T>, IPersistentRepository<T> where T : class
    {
        public PersistedGrantRepository(PersistedGrantDbContext context) : base(context)
        {

        }
    }
}
