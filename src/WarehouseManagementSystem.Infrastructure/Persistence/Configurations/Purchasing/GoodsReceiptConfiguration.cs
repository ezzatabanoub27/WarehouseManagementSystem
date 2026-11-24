
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WarehouseManagementSystem.Domain.Entities.Purchasing;

namespace WarehouseManagementSystem.Infrastructure.Persistence.Configurations.Purchasing;

public class GoodsReceiptConfiguration
    : IEntityTypeConfiguration<GoodsReceipt>
{
    public void Configure(EntityTypeBuilder<GoodsReceipt> builder)
    {
        builder.ToTable("GoodsReceipts");

        builder.HasKey(x => x.Id);

        builder.Property(x => x.ReceiptNumber)
            .IsRequired()
            .HasMaxLength(50);

        builder.Property(x => x.Status).IsRequired();
        builder.Property(x => x.Notes).HasMaxLength(1000);

        builder.HasIndex(x => x.ReceiptNumber).IsUnique();

        builder.HasOne(x => x.Warehouse)
            .WithMany()
            .HasForeignKey(x => x.WarehouseId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasMany(x => x.Items)
            .WithOne(x => x.GoodsReceipt)
            .HasForeignKey(x => x.GoodsReceiptId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}
