using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WarehouseManagementSystem.Domain.Common;
using WarehouseManagementSystem.Domain.Entities.MasterData;

namespace WarehouseManagementSystem.Domain.Entities.Purchasing
{
    public class PurchaseOrderItem :AuditableEntity
    {
        public Guid PurchaseOrderId { get; set; }
        public Guid ProductId { get; set; }
        public Guid UnitId { get; set; }

        public decimal Quantity { get; set; }
        public decimal ReceivedQuantity { get; set; }

        public decimal UnitPrice { get; set; }
        public decimal Discount { get; set; }
        public decimal Tax { get; set; }
        public decimal Total { get; set; }

        public PurchaseOrder PurchaseOrder { get; set; } = null!;
        public Product Product { get; set; } = null!;
        public Unit Unit { get; set; } = null!;
    }
}
