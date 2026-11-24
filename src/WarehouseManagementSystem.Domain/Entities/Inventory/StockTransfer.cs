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
    public class StockTransfer :AuditableEntity
    {

        public string TransferNumber { get; set; } = string.Empty;

        public Guid FromWarehouseId { get; set; }
        public Guid ToWarehouseId { get; set; }

        public DateTime TransferDate { get; set; }

        public DocumentStatus Status { get; set; } = DocumentStatus.Draft;

        public string? Notes { get; set; }

        // Navigation Properties
        public Warehouse FromWarehouse { get; set; } = null!;
        public Warehouse ToWarehouse { get; set; } = null!;

        public ICollection<StockTransferItem> Items { get; set; }= new List<StockTransferItem>();
    }
}
