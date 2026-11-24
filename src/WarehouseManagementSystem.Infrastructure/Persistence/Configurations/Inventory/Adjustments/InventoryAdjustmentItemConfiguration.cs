
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WarehouseManagementSystem.Domain.Entities.Inventory;

namespace WarehouseManagementSystem.Infrastructure.Persistence.Configurations.Inventory.Adjustments;

public class InventoryAdjustmentItemConfiguration
    : IEntityTypeConfiguration<InventoryAdjustmentItem>
{
    public void Configure(EntityTypeBuilder<InventoryAdjustmentItem> builder)
    {
        builder.ToTable("InventoryAdjustmentItems", table =>
        {
            table.HasCheckConstraint(
                "CK_InventoryAdjustmentItems_Quantities",
                "[SystemQuantity] >= 0 AND [CountedQuantity] >= 0");
        });

        builder.HasKey(x => x.Id);

        builder.Property(x => x.SystemQuantity).HasPrecision(18, 3);
        builder.Property(x => x.CountedQuantity).HasPrecision(18, 3);

        // Difference is calculated in C# and is not stored as a database column.
        builder.Ignore(x => x.Difference);

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

        builder.HasIndex(x => x.InventoryAdjustmentId);
        builder.HasIndex(x => new { x.ProductId, x.LocationId });
    }
}
