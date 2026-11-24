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
    public class UnitConfiguration : IEntityTypeConfiguration<Unit>
    {
        public void Configure(EntityTypeBuilder<Unit> builder)
        {
            builder.ToTable("Units");
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Name)
                .IsRequired()
                .HasMaxLength(200);
            builder.Property(x => x.Symbol)
          .IsRequired()
          .HasMaxLength(20);

            builder.HasIndex(x => x.Name)
                .IsUnique();

            builder.HasIndex(x => x.Symbol)
                .IsUnique();
        }
    }
}
