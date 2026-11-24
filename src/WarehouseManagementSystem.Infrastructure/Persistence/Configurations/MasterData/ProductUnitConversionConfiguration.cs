using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WarehouseManagementSystem.Domain.Entities.MasterData;

namespace WarehouseManagementSystem.Infrastructure.Persistence.Configurations.MasterData
{
    public class ProductUnitConversionConfiguration : IEntityTypeConfiguration<ProductUnitConversion>
    {
        public void Configure(EntityTypeBuilder<ProductUnitConversion> builder)
        {
            builder.ToTable("ProductUnitConversions", table =>
            {
                table.HasCheckConstraint(
                    "CK_ProductUnitConversions_Factor",
                    "[ConversionFactor] > 0");
            });

            builder.HasKey(x => x.Id);

            builder.Property(x => x.ConversionFactor)
                .HasPrecision(18, 6);

            builder.HasIndex(x => new { x.ProductId, x.UnitId })
                .IsUnique();

            builder.HasOne(x => x.Product)
                .WithMany(x => x.UnitConversions)
                .HasForeignKey(x => x.ProductId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(x => x.Unit)
                .WithMany()
                .HasForeignKey(x => x.UnitId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
