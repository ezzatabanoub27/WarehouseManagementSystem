
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WarehouseManagementSystem.Domain.Entities.Inventory;

namespace WarehouseManagementSystem.Infrastructure.Persistence.Configurations.Inventory.Adjustments;

public class InventoryAdjustmentConfiguration
    : IEntityTypeConfiguration<InventoryAdjustment>
{
    public void Configure(EntityTypeBuilder<InventoryAdjustment> builder)
    {
        builder.ToTable("InventoryAdjustments");

        builder.HasKey(x => x.Id);

        builder.Property(x => x.AdjustmentNumber)
            .IsRequired()
            .HasMaxLength(50);

        builder.Property(x => x.Status).IsRequired();
        builder.Property(x => x.Reason).IsRequired();
        builder.Property(x => x.Notes).HasMaxLength(1000);

        builder.HasIndex(x => x.AdjustmentNumber).IsUnique();

        builder.HasOne(x => x.Warehouse)
            .WithMany()
            .HasForeignKey(x => x.WarehouseId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasMany(x => x.Items)
            .WithOne(x => x.InventoryAdjustment)
            .HasForeignKey(x => x.InventoryAdjustmentId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}
