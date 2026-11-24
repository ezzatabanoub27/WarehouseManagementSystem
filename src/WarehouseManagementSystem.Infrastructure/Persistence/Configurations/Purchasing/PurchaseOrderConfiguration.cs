using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WarehouseManagementSystem.Domain.Entities.Purchasing;

namespace WarehouseManagementSystem.Infrastructure.Persistence.Configurations.Purchasing
{
    public class PurchaseOrderConfiguration : IEntityTypeConfiguration<PurchaseOrder>
    {
        public void Configure(EntityTypeBuilder<PurchaseOrder> builder)
        {
            builder.ToTable("PurchaseOrders");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.OrderNumber)
                .IsRequired()
                .HasMaxLength(50);

            builder.Property(x => x.Status)
                .IsRequired();

            builder.Property(x => x.SubTotal).HasPrecision(19, 4);
            builder.Property(x => x.Discount).HasPrecision(19, 4);
            builder.Property(x => x.Tax).HasPrecision(19, 4);
            builder.Property(x => x.Total).HasPrecision(19, 4);

            builder.Property(x => x.Notes).HasMaxLength(1000);

            builder.HasIndex(x => x.OrderNumber).IsUnique();

            builder.HasOne(x => x.Supplier)
                .WithMany()
                .HasForeignKey(x => x.SupplierId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(x => x.Warehouse)
                .WithMany()
                .HasForeignKey(x => x.WarehouseId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasMany(x => x.Items)
                .WithOne(x => x.PurchaseOrder)
                .HasForeignKey(x => x.PurchaseOrderId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasMany(x => x.GoodsReceipts)
                .WithOne(x => x.PurchaseOrder)
                .HasForeignKey(x => x.PurchaseOrderId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
