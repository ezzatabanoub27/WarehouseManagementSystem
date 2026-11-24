using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WarehouseManagementSystem.Domain.Entities.Organizations;

namespace WarehouseManagementSystem.Infrastructure.Persistence.Configurations.Organizations
{
    public class LocationConfiguration : IEntityTypeConfiguration<Location>
    {
        public void Configure(EntityTypeBuilder<Location> builder)
        {
            builder.ToTable("Locations");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Code)
                .IsRequired()
                .HasMaxLength(50);

            builder.Property(x => x.Name)
                .IsRequired()
                .HasMaxLength(200);

            builder.Property(x => x.LocationType)
                .IsRequired()
                .HasConversion<int>();

            builder.HasIndex(x => new { x.WarehouseId, x.Code })
                .IsUnique();

            builder.HasOne(x => x.Warehouse)
                .WithMany(x => x.Locations)
                .HasForeignKey(x => x.WarehouseId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
