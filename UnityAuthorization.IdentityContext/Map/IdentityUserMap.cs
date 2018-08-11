using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;
using UnityAuthorization.IdentityContext.Entity;
using System.ComponentModel.DataAnnotations.Schema;

namespace UnityAuthorization.IdentityContext.Map
{
    public class IdentityUserMap : IEntityTypeConfiguration<IdentityUser>
    {
        public void Configure(EntityTypeBuilder<IdentityUser> builder)
        {
            builder.ToTable("IdentityUser");
        }
    }
}
