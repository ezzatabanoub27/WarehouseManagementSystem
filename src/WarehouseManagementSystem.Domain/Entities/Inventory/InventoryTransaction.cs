using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WarehouseManagementSystem.Domain.Common;
using WarehouseManagementSystem.Domain.Enums;

namespace WarehouseManagementSystem.Domain.Entities.Inventory
{
    public class InventoryTransaction :AuditableEntity
    {

        public string TransactionNumber { get; set; } = string.Empty;

        public InventoryTransactionType TransactionType { get; set; }

        public string ReferenceType { get; set; } = string.Empty;

        public Guid ReferenceId { get; set; }

        public string ReferenceNumber { get; set; } = string.Empty;

        public DateTime TransactionDate { get; set; }

        public InventoryTransactionStatus Status { get; set; }
            = InventoryTransactionStatus.Draft;

        public DateTime? PostedAtUtc { get; set; }

        public Guid? PostedBy { get; set; }

        public ICollection<StockMovement> StockMovements { get; set; }= new List<StockMovement>();

    }
}
