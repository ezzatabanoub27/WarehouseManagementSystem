
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WarehouseManagementSystem.Domain.Entities.Inventory;

namespace WarehouseManagementSystem.Infrastructure.Persistence.Configurations.Inventory;

public class SerialNumberConfiguration
    : IEntityTypeConfiguration<SerialNumber>
{
    public void Configure(EntityTypeBuilder<SerialNumber> builder)
    {
        builder.ToTable("SerialNumbers");

        builder.HasKey(x => x.Id);

        builder.Property(x => x.SerialNumberValue)
            .IsRequired()
            .HasMaxLength(150);

        builder.Property(x => x.Status).IsRequired();

        builder.HasIndex(x => new { x.ProductId, x.SerialNumberValue })
            .IsUnique();

        builder.HasOne(x => x.Product)
            .WithMany(x => x.SerialNumbers)
            .HasForeignKey(x => x.ProductId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(x => x.Lot)
            .WithMany()
            .HasForeignKey(x => x.LotId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(x => x.CurrentLocation)
            .WithMany()
            .HasForeignKey(x => x.CurrentLocationId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}
