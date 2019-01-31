﻿using Doctrina.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Doctrina.Persistence.Configurations
{
    public class AccountConfiguration : IEntityTypeConfiguration<AccountEntity>
    {
        public void Configure(EntityTypeBuilder<AccountEntity> builder)
        {
            builder.Property(e => e.HomePage)
                .HasMaxLength(Constants.MAX_URL_LENGTH);

            builder.Property(e => e.Name)
               .HasMaxLength(40);

            builder
                .HasIndex(account => new { account.HomePage, account.Name })
                .HasFilter("[HomePage] IS NOT NULL AND [Name] IS NOT NULL")
                .IsUnique();
        }
    }
}