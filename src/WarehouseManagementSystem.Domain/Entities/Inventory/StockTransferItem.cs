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
    public class StockTransferItem:AuditableEntity
    {

        public Guid StockTransferId { get; set; }

        public Guid ProductId { get; set; }

        public Guid SourceLocationId { get; set; }

        public Guid DestinationLocationId { get; set; }

        public Guid? LotId { get; set; }

        public Guid? SerialNumberId { get; set; }

        public decimal Quantity { get; set; }

        // Navigation Properties
        public StockTransfer StockTransfer { get; set; } = null!;

        public Product Product { get; set; } = null!;

        public Location SourceLocation { get; set; } = null!;

        public Location DestinationLocation { get; set; } = null!;

        public Lot? Lot { get; set; }

        public SerialNumber? SerialNumber { get; set; }
    }
}
