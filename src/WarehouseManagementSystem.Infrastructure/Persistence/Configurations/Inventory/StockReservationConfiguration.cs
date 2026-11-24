
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WarehouseManagementSystem.Domain.Entities.Inventory;

namespace WarehouseManagementSystem.Infrastructure.Persistence.Configurations.Inventory;

public class StockReservationConfiguration
    : IEntityTypeConfiguration<StockReservation>
{
    public void Configure(EntityTypeBuilder<StockReservation> builder)
    {
        builder.ToTable("StockReservations", table =>
        {
            table.HasCheckConstraint(
                "CK_StockReservations_Quantity",
                "[Quantity] > 0");
        });

        builder.HasKey(x => x.Id);

        builder.Property(x => x.Quantity).HasPrecision(18, 3);
        builder.Property(x => x.Status).IsRequired();

        builder.HasIndex(x => x.SalesOrderItemId);
        builder.HasIndex(x => new { x.ProductId, x.LocationId, x.Status });

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
    }
}
