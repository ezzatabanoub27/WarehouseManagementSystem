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
    public class ProductConfiguration : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {

            builder.ToTable("Products", table =>
            {
                table.HasCheckConstraint(
                    "CK_Products_CostPrice",
                    "[CostPrice] >= 0");

                table.HasCheckConstraint(
                    "CK_Products_SellingPrice",
                    "[SellingPrice] >= 0");
            });

            builder.HasKey(x => x.Id);

            builder.Property(x => x.SKU)
                .IsRequired()
                .HasMaxLength(50);

            builder.Property(x => x.Barcode)
                .HasMaxLength(100);

            builder.Property(x => x.Name)
                .IsRequired()
                .HasMaxLength(200);

            builder.Property(x => x.Description)
                .HasMaxLength(500);

            builder.Property(x => x.TrackingMode)
                .HasConversion<int>()
                .IsRequired();

            builder.Property(x => x.CostPrice)
                .HasPrecision(19, 4);

            builder.Property(x => x.SellingPrice)
                .HasPrecision(19, 4);

            // SKU must be unique
            builder.HasIndex(x => x.SKU)
                .IsUnique();
            builder.Property(x => x.ImagePath)
            .HasMaxLength(500)
            .IsRequired(false);

            // Barcode can be NULL, but non-null values must be unique
            builder.HasIndex(x => x.Barcode)
                .IsUnique()
                .HasFilter("[Barcode] IS NOT NULL");

            builder.HasOne(x => x.Category)
                .WithMany(x => x.Products)
                .HasForeignKey(x => x.CategoryId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(x => x.BaseUnit)
                  .WithMany()
                  .HasForeignKey(x => x.BaseUnitId)
                  .IsRequired(false)
                  .OnDelete(DeleteBehavior.Restrict);

            builder.HasMany(x => x.UnitConversions)
                .WithOne(x => x.Product)
                .HasForeignKey(x => x.ProductId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
