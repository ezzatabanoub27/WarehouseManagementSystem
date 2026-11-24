
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WarehouseManagementSystem.Domain.Entities.Sales;

namespace WarehouseManagementSystem.Infrastructure.Persistence.Configurations.Sales;

public class SalesOrderItemConfiguration : IEntityTypeConfiguration<SalesOrderItem>
{
    public void Configure(EntityTypeBuilder<SalesOrderItem> builder)
    {
        builder.ToTable("SalesOrderItems", table =>
        {
            table.HasCheckConstraint(
                "CK_SalesOrderItems_Quantity",
                "[Quantity] > 0 AND [ShippedQuantity] >= 0 AND [ShippedQuantity] <= [Quantity]");
        });

        builder.HasKey(x => x.Id);

        builder.Property(x => x.Quantity).HasPrecision(18, 3);
        builder.Property(x => x.ShippedQuantity).HasPrecision(18, 3);

        builder.Property(x => x.UnitPrice).HasPrecision(19, 4);
        builder.Property(x => x.Discount).HasPrecision(19, 4);
        builder.Property(x => x.Tax).HasPrecision(19, 4);
        builder.Property(x => x.Total).HasPrecision(19, 4);

        builder.HasOne(x => x.Product)
            .WithMany()
            .HasForeignKey(x => x.ProductId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(x => x.Unit)
            .WithMany()
            .HasForeignKey(x => x.UnitId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}
