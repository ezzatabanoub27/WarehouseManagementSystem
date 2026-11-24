
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WarehouseManagementSystem.Domain.Entities.Sales;

namespace WarehouseManagementSystem.Infrastructure.Persistence.Configurations.Sales;

public class SalesOrderConfiguration : IEntityTypeConfiguration<SalesOrder>
{
    public void Configure(EntityTypeBuilder<SalesOrder> builder)
    {
        builder.ToTable("SalesOrders");

        builder.HasKey(x => x.Id);

        builder.Property(x => x.OrderNumber)
            .IsRequired()
            .HasMaxLength(50);

        builder.Property(x => x.Status).IsRequired();

        builder.Property(x => x.SubTotal).HasPrecision(19, 4);
        builder.Property(x => x.Discount).HasPrecision(19, 4);
        builder.Property(x => x.Tax).HasPrecision(19, 4);
        builder.Property(x => x.Total).HasPrecision(19, 4);

        builder.Property(x => x.Notes).HasMaxLength(1000);

        builder.HasIndex(x => x.OrderNumber).IsUnique();

        builder.HasOne(x => x.Customer)
            .WithMany()
            .HasForeignKey(x => x.CustomerId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(x => x.Warehouse)
            .WithMany()
            .HasForeignKey(x => x.WarehouseId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasMany(x => x.Items)
            .WithOne(x => x.SalesOrder)
            .HasForeignKey(x => x.SalesOrderId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasMany(x => x.Shipments)
            .WithOne(x => x.SalesOrder)
            .HasForeignKey(x => x.SalesOrderId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}
