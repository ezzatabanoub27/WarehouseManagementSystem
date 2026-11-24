using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WarehouseManagementSystem.Domain.Entities.MasterData;

namespace WarehouseManagementSystem.Infrastructure.Persistence.Configurations.MasterData
{
    public class CustomerConfiguration : IEntityTypeConfiguration<Customer>
    {
        public void Configure(EntityTypeBuilder<Customer> builder)
        {
            builder.ToTable("Customers");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Code)
                .IsRequired()
                .HasMaxLength(50);

            builder.Property(x => x.Name)
                .IsRequired()
                .HasMaxLength(200);

            builder.Property(x => x.Phone)
                .HasMaxLength(30);

            builder.Property(x => x.Email)
                .HasMaxLength(200);

            builder.Property(x => x.Address)
                .HasMaxLength(500);

            builder.HasIndex(x => x.Code)
                .IsUnique();

            builder.HasIndex(x => x.Name);
        }
    }
}
