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
    public class PurchaseOrderItemConfiguration : IEntityTypeConfiguration<PurchaseOrderItem>
    {
        public void Configure(EntityTypeBuilder<PurchaseOrderItem> builder)
        {
            builder.ToTable("PurchaseOrderItems", table =>
            {
                table.HasCheckConstraint(
                    "CK_PurchaseOrderItems_Quantity",
                    "[Quantity] > 0 AND [ReceivedQuantity] >= 0 AND [ReceivedQuantity] <= [Quantity]");
            });

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Quantity).HasPrecision(18, 3);
            builder.Property(x => x.ReceivedQuantity).HasPrecision(18, 3);
            builder.Property(x => x.UnitPrice).HasPrecision(19, 4);
            builder.Property(x => x.Discount).HasPrecision(19, 4);
            builder.Property(x => x.Tax).HasPrecision(19, 4);
            builder.Property(x => x.Total).HasPrecision(19, 4);

            builder.HasOne(x => x.Product)
                .WithMany()
                .HasForeignKey(x => x.ProductId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(x => x.Unit)
                .WithMany()
                .HasForeignKey(x => x.UnitId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
