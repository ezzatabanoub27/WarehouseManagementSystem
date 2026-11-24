
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WarehouseManagementSystem.Domain.Entities.Inventory;

namespace WarehouseManagementSystem.Infrastructure.Persistence.Configurations.Inventory.Transfers;

public class StockTransferItemConfiguration
    : IEntityTypeConfiguration<StockTransferItem>
{
    public void Configure(EntityTypeBuilder<StockTransferItem> builder)
    {
        builder.ToTable("StockTransferItems", table =>
        {
            table.HasCheckConstraint(
                "CK_StockTransferItems_Quantity",
                "[Quantity] > 0");

            table.HasCheckConstraint(
                "CK_StockTransferItems_DifferentLocations",
                "[SourceLocationId] <> [DestinationLocationId]");
        });

        builder.HasKey(x => x.Id);

        builder.Property(x => x.Quantity).HasPrecision(18, 3);

        builder.HasOne(x => x.Product)
            .WithMany()
            .HasForeignKey(x => x.ProductId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(x => x.SourceLocation)
            .WithMany()
            .HasForeignKey(x => x.SourceLocationId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(x => x.DestinationLocation)
            .WithMany()
            .HasForeignKey(x => x.DestinationLocationId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(x => x.Lot)
            .WithMany()
            .HasForeignKey(x => x.LotId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(x => x.SerialNumber)
            .WithMany()
            .HasForeignKey(x => x.SerialNumberId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasIndex(x => x.StockTransferId);
        builder.HasIndex(x => x.ProductId);
    }
}
