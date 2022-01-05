using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Skinet.Core.Entities;

namespace Skinet.Infrastructure.Data.Config
{
   public class UserConfiguration:IEntityTypeConfiguration<UserEntity>
    {
        public void Configure(EntityTypeBuilder<UserEntity> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.UserEmail).IsRequired();
            builder.Property(x => x.UserName).IsRequired();
            builder.Property(x => x.UserPassword).IsRequired();
        }
    }
}
