using Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Infra.Map
{
    public class AccountMap : IEntityTypeConfiguration<Account>
    {
        public void Configure(EntityTypeBuilder<Account> builder)
        {

            builder.ToTable("Account");

            builder.HasKey(c => c.Id);

            builder.Property(c => c.Name)
               .HasMaxLength(100)
               .HasField("Name")
               .IsRequired();

            builder.Property(c => c.Description)
                .HasMaxLength(100)
                .HasField("Description")
                .IsRequired();

            builder.Property(c => c.Balance)
                .HasField("Balance")
                .IsRequired();
        }
    }
}
