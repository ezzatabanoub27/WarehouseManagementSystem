
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WarehouseManagementSystem.Domain.Entities.Inventory;

namespace WarehouseManagementSystem.Infrastructure.Persistence.Configurations.Inventory;

public class StockMovementConfiguration
    : IEntityTypeConfiguration<StockMovement>
{
    public void Configure(EntityTypeBuilder<StockMovement> builder)
    {
        builder.ToTable("StockMovements", table =>
        {
            table.HasCheckConstraint(
                "CK_StockMovements_QuantityDelta",
                "[QuantityDelta] <> 0");

            table.HasCheckConstraint(
                "CK_StockMovements_UnitCost",
                "[UnitCost] >= 0");
        });

        builder.HasKey(x => x.Id);

        builder.Property(x => x.QuantityDelta).HasPrecision(18, 3);
        builder.Property(x => x.UnitCost).HasPrecision(19, 4);

        builder.HasIndex(x => x.ProductId);
        builder.HasIndex(x => x.LocationId);
        builder.HasIndex(x => x.InventoryTransactionId);

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

        builder.HasOne(x => x.SerialNumber)
            .WithMany()
            .HasForeignKey(x => x.SerialNumberId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}
