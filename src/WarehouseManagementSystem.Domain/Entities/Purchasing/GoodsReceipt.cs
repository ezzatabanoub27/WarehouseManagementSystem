using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WarehouseManagementSystem.Domain.Common;
using WarehouseManagementSystem.Domain.Entities.Organizations;
using WarehouseManagementSystem.Domain.Enums;

namespace WarehouseManagementSystem.Domain.Entities.Purchasing
{
    public class GoodsReceipt :AuditableEntity
    {

        public string ReceiptNumber { get; set; } = string.Empty;

        public Guid PurchaseOrderId { get; set; }
        public Guid WarehouseId { get; set; }

        public DateTime ReceiptDate { get; set; }

        public DocumentStatus Status { get; set; } = DocumentStatus.Draft;

        public string? Notes { get; set; }

        public PurchaseOrder PurchaseOrder { get; set; } = null!;
        public Warehouse Warehouse { get; set; } = null!;

        public ICollection<GoodsReceiptItem> Items { get; set; }
            = new List<GoodsReceiptItem>();
    }
}
