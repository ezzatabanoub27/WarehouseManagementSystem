
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WarehouseManagementSystem.Domain.Entities.Inventory;

namespace WarehouseManagementSystem.Infrastructure.Persistence.Configurations.Inventory;

public class LotConfiguration : IEntityTypeConfiguration<Lot>
{
    public void Configure(EntityTypeBuilder<Lot> builder)
    {
        builder.ToTable("Lots", table =>
        {
            table.HasCheckConstraint(
                "CK_Lots_ExpiryDate",
                "[ExpiryDate] IS NULL OR [ManufacturingDate] IS NULL OR [ExpiryDate] >= [ManufacturingDate]");
        });

        builder.HasKey(x => x.Id);

        builder.Property(x => x.LotNumber)
            .IsRequired()
            .HasMaxLength(100);

        builder.HasIndex(x => new { x.ProductId, x.LotNumber })
            .IsUnique();

        builder.HasOne(x => x.Product)
            .WithMany(x => x.Lots)
            .HasForeignKey(x => x.ProductId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(x => x.Supplier)
            .WithMany()
            .HasForeignKey(x => x.SupplierId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}
