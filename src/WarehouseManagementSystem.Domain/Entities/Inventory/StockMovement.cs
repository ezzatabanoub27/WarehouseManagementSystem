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
    public class StockMovement :AuditableEntity
    {
        public Guid InventoryTransactionId { get; set; }

        public Guid ProductId { get; set; }
        public Guid LocationId { get; set; }

        public Guid? LotId { get; set; }
        public Guid? SerialNumberId { get; set; }

        // Positive = Stock In
        // Negative = Stock Out
        public decimal QuantityDelta { get; set; }

        public decimal UnitCost { get; set; }

        public InventoryTransaction InventoryTransaction { get; set; }= null!;

        public Location Location { get; set; } = null!;
        public Lot? Lot { get; set; }
        public SerialNumber? SerialNumber { get; set; }
        public Product Product { get; set; } = null!;
    }
}
