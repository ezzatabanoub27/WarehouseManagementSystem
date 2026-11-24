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
    public class InventoryAdjustmentItem:AuditableEntity
    {
        public Guid InventoryAdjustmentId { get; set; }

        public Guid ProductId { get; set; }

        public Guid LocationId { get; set; }

        public Guid? LotId { get; set; }

        public decimal SystemQuantity { get; set; }

        public decimal CountedQuantity { get; set; }

        public decimal Difference => CountedQuantity - SystemQuantity;

        // Navigation Properties
        public InventoryAdjustment InventoryAdjustment { get; set; } = null!;

        public Product Product { get; set; } = null!;

        public Location Location { get; set; } = null!;

        public Lot? Lot { get; set; }

    }
}
