using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WarehouseManagementSystem.Domain.Common;

namespace WarehouseManagementSystem.Domain.Entities.MasterData
{
    public class ProductUnitConversion :AuditableEntity
    {
        public Guid ProductId { get; set; }
        public Guid UnitId { get; set; }

        public decimal ConversionFactor { get; set; }

        public Product Product { get; set; } = null!;
        public Unit Unit { get; set; } = null!;
    }
}
