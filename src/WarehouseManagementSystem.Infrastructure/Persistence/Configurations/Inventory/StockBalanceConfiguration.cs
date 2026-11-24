
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WarehouseManagementSystem.Domain.Entities.Inventory;

namespace WarehouseManagementSystem.Infrastructure.Persistence.Configurations.Inventory;

public class StockBalanceConfiguration
    : IEntityTypeConfiguration<StockBalance>
{
    public void Configure(EntityTypeBuilder<StockBalance> builder)
    {
        builder.ToTable("StockBalances", table =>
        {
            table.HasCheckConstraint(
                "CK_StockBalances_OnHand",
                "[OnHandQuantity] >= 0");

            table.HasCheckConstraint(
                "CK_StockBalances_Reserved",
                "[ReservedQuantity] >= 0 AND [ReservedQuantity] <= [OnHandQuantity]");
        });

        builder.HasKey(x => x.Id);

        builder.Property(x => x.OnHandQuantity).HasPrecision(18, 3);
        builder.Property(x => x.ReservedQuantity).HasPrecision(18, 3);

        builder.Property(x => x.RowVersion)
            .IsRowVersion();

        // One balance per Product + Location when LotId is NULL
        builder.HasIndex(x => new { x.ProductId, x.LocationId })
            .IsUnique()
            .HasFilter("[LotId] IS NULL");

        // One balance per Product + Location + Lot when LotId has a value
        builder.HasIndex(x => new { x.ProductId, x.LocationId, x.LotId })
            .IsUnique()
            .HasFilter("[LotId] IS NOT NULL");

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
