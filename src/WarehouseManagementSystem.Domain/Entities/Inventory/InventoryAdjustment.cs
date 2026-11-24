using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WarehouseManagementSystem.Domain.Common;
using WarehouseManagementSystem.Domain.Entities.Organizations;
using WarehouseManagementSystem.Domain.Enums;

namespace WarehouseManagementSystem.Domain.Entities.Inventory
{
    public class InventoryAdjustment:AuditableEntity
    {

        public string AdjustmentNumber { get; set; } = string.Empty;

        public Guid WarehouseId { get; set; }

        public DateTime AdjustmentDate { get; set; }

        public DocumentStatus Status { get; set; } = DocumentStatus.Draft;

        public StockAdjustmentReason Reason { get; set; }

        public string? Notes { get; set; }

        // Navigation Properties
        public Warehouse Warehouse { get; set; } = null!;

        public ICollection<InventoryAdjustmentItem> Items { get; set; }= new List<InventoryAdjustmentItem>();
    }
}
