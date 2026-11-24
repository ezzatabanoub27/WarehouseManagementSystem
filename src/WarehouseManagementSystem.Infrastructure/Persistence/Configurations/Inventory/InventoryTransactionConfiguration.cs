using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WarehouseManagementSystem.Domain.Entities.Inventory;

namespace WarehouseManagementSystem.Infrastructure.Persistence.Configurations.Inventory
{
    public class InventoryTransactionConfiguration : IEntityTypeConfiguration<InventoryTransaction>
    {
        public void Configure(EntityTypeBuilder<InventoryTransaction> builder)
        {
            builder.ToTable("InventoryTransactions");
            builder.HasKey(x => x.Id);

            builder.Property(x => x.TransactionNumber).IsRequired().HasMaxLength(50);
            builder.Property(x => x.TransactionType).IsRequired();
            builder.Property(x => x.ReferenceType).HasMaxLength(50);
            builder.Property(x => x.ReferenceNumber).HasMaxLength(50);
            builder.Property(x => x.Status).IsRequired();
            builder.Property(x => x.PostedBy).HasMaxLength(200);

            builder.HasIndex(x => x.TransactionNumber).IsUnique();
            builder.HasIndex(x => new { x.ReferenceType, x.ReferenceId });

            builder.HasMany(x => x.StockMovements)
                .WithOne(x => x.InventoryTransaction)
                .HasForeignKey(x => x.InventoryTransactionId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
