using IdentityServer4.EntityFramework.DbContexts;
using IdentityServer4.EntityFramework.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using UnityAuthorization.Repository.Interface;

namespace UnityAuthorization.Repository
{
    public class ConfigurationDbContextRepository<T> : RepositoryBase<T>,IConfigurationDbContextRepository<T> where T : class
    {
        public ConfigurationDbContextRepository(ConfigurationDbContext context) : base(context)
        {

        }
    }
}
