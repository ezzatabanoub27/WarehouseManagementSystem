using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WarehouseManagementSystem.Domain.Common;
using WarehouseManagementSystem.Domain.Entities.MasterData;
using WarehouseManagementSystem.Domain.Entities.Organizations;

namespace WarehouseManagementSystem.Domain.Entities.Inventory
{
    public class WarehouseProductCost:AuditableEntity
    {
        public Guid ProductId { get; set; }

        public Guid WarehouseId { get; set; }

        public decimal QuantityOnHand { get; set; }

        public decimal AverageUnitCost { get; set; }

        public decimal InventoryValue { get; set; }

        public byte[] RowVersion { get; set; } = Array.Empty<byte>();

        public DateTime UpdatedAtUtc { get; set; } = DateTime.UtcNow;

        public Product Product { get; set; } = null!;

        public Warehouse Warehouse { get; set; } = null!;
    }
}
