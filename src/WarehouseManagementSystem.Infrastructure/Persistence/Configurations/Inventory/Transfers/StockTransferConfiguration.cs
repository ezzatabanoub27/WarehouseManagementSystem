
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WarehouseManagementSystem.Domain.Entities.Inventory;

namespace WarehouseManagementSystem.Infrastructure.Persistence.Configurations.Inventory.Transfers;

public class StockTransferConfiguration
    : IEntityTypeConfiguration<StockTransfer>
{
    public void Configure(EntityTypeBuilder<StockTransfer> builder)
    {
        builder.ToTable("StockTransfers", table =>
        {
            table.HasCheckConstraint(
                "CK_StockTransfers_DifferentWarehouses",
                "[FromWarehouseId] <> [ToWarehouseId]");
        });

        builder.HasKey(x => x.Id);

        builder.Property(x => x.TransferNumber)
            .IsRequired()
            .HasMaxLength(50);

        builder.Property(x => x.Status).IsRequired();
        builder.Property(x => x.Notes).HasMaxLength(1000);

        builder.HasIndex(x => x.TransferNumber).IsUnique();

        builder.HasOne(x => x.FromWarehouse)
            .WithMany()
            .HasForeignKey(x => x.FromWarehouseId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(x => x.ToWarehouse)
            .WithMany()
            .HasForeignKey(x => x.ToWarehouseId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasMany(x => x.Items)
            .WithOne(x => x.StockTransfer)
            .HasForeignKey(x => x.StockTransferId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}
