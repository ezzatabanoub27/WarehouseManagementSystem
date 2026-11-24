
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WarehouseManagementSystem.Domain.Entities.Purchasing;

namespace WarehouseManagementSystem.Infrastructure.Persistence.Configurations.Purchasing;

public class GoodsReceiptItemConfiguration
    : IEntityTypeConfiguration<GoodsReceiptItem>
{
    public void Configure(EntityTypeBuilder<GoodsReceiptItem> builder)
    {
        builder.ToTable("GoodsReceiptItems", table =>
        {
            table.HasCheckConstraint(
                "CK_GoodsReceiptItems_Quantity",
                "[Quantity] > 0");
        });

        builder.HasKey(x => x.Id);

        builder.Property(x => x.Quantity).HasPrecision(18, 3);
        builder.Property(x => x.UnitCost).HasPrecision(19, 4);

        builder.HasOne(x => x.PurchaseOrderItem)
            .WithMany()
            .HasForeignKey(x => x.PurchaseOrderItemId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(x => x.Product)
            .WithMany()
            .HasForeignKey(x => x.ProductId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(x => x.Location)
            .WithMany()
            .HasForeignKey(x => x.LocationId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(x => x.Lot)
            .WithMany()
            .HasForeignKey(x => x.LotId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}
