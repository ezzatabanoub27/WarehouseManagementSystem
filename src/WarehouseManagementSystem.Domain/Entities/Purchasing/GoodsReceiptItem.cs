using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WarehouseManagementSystem.Domain.Common;
using WarehouseManagementSystem.Domain.Entities.Inventory;
using WarehouseManagementSystem.Domain.Entities.MasterData;
using WarehouseManagementSystem.Domain.Entities.Organizations;

namespace WarehouseManagementSystem.Domain.Entities.Purchasing
{
    public class GoodsReceiptItem:AuditableEntity
    {

        public Guid GoodsReceiptId { get; set; }
        public Guid PurchaseOrderItemId { get; set; }

        public Guid ProductId { get; set; }
        public Guid LocationId { get; set; }

        public Guid? LotId { get; set; }

        public decimal Quantity { get; set; }
        public decimal UnitCost { get; set; }

        public GoodsReceipt GoodsReceipt { get; set; } = null!;
        public PurchaseOrderItem PurchaseOrderItem { get; set; } = null!;

        public Product Product { get; set; } = null!;
        public Location Location { get; set; } = null!;
        public Lot? Lot { get; set; }
    }
}
