using Microsoft.EntityFrameworkCore;
using System;
using UnityAuthorization.IdentityContext.Entity;

namespace UnityAuthorization.IdentityContext
{
    public class IdentityUserContext:DbContext
    {
        public IdentityUserContext(DbContextOptions options):base(options)
        {

        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}
