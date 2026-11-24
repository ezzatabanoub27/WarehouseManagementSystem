
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WarehouseManagementSystem.Domain.Entities.Sales;

namespace WarehouseManagementSystem.Infrastructure.Persistence.Configurations.Sales;

public class ShipmentItemConfiguration : IEntityTypeConfiguration<ShipmentItem>
{
    public void Configure(EntityTypeBuilder<ShipmentItem> builder)
    {
        builder.ToTable("ShipmentItems", table =>
        {
            table.HasCheckConstraint(
                "CK_ShipmentItems_Quantity",
                "[Quantity] > 0");
        });

        builder.HasKey(x => x.Id);

        builder.Property(x => x.Quantity).HasPrecision(18, 3);

        builder.HasOne(x => x.SalesOrderItem)
            .WithMany()
            .HasForeignKey(x => x.SalesOrderItemId)
            .OnDelete(DeleteBehavior.Restrict);

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
