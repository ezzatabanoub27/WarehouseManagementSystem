
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WarehouseManagementSystem.Domain.Entities.Sales;

namespace WarehouseManagementSystem.Infrastructure.Persistence.Configurations.Sales;

public class ShipmentConfiguration : IEntityTypeConfiguration<Shipment>
{
    public void Configure(EntityTypeBuilder<Shipment> builder)
    {
        builder.ToTable("Shipments");

        builder.HasKey(x => x.Id);

        builder.Property(x => x.ShipmentNumber)
            .IsRequired()
            .HasMaxLength(50);

        builder.Property(x => x.Status).IsRequired();
        builder.Property(x => x.Notes).HasMaxLength(1000);

        builder.HasIndex(x => x.ShipmentNumber).IsUnique();

        builder.HasOne(x => x.Warehouse)
            .WithMany()
            .HasForeignKey(x => x.WarehouseId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasMany(x => x.Items)
            .WithOne(x => x.Shipment)
            .HasForeignKey(x => x.ShipmentId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}
