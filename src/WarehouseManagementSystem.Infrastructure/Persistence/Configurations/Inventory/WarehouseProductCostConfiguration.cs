
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WarehouseManagementSystem.Domain.Entities.Inventory;

namespace WarehouseManagementSystem.Infrastructure.Persistence.Configurations.Inventory;

public class WarehouseProductCostConfiguration
    : IEntityTypeConfiguration<WarehouseProductCost>
{
    public void Configure(EntityTypeBuilder<WarehouseProductCost> builder)
    {
        builder.ToTable("WarehouseProductCosts", table =>
        {
            table.HasCheckConstraint(
                "CK_WarehouseProductCosts_Quantity",
                "[QuantityOnHand] >= 0");

            table.HasCheckConstraint(
                "CK_WarehouseProductCosts_Cost",
                "[AverageUnitCost] >= 0 AND [InventoryValue] >= 0");
        });

        builder.HasKey(x => x.Id);

        builder.Property(x => x.QuantityOnHand).HasPrecision(18, 3);
        builder.Property(x => x.AverageUnitCost).HasPrecision(19, 4);
        builder.Property(x => x.InventoryValue).HasPrecision(19, 4);

        builder.Property(x => x.RowVersion).IsRowVersion();

        builder.HasIndex(x => new { x.ProductId, x.WarehouseId })
            .IsUnique();

        builder.HasOne(x => x.Product)
            .WithMany()
            .HasForeignKey(x => x.ProductId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(x => x.Warehouse)
            .WithMany()
            .HasForeignKey(x => x.WarehouseId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}
