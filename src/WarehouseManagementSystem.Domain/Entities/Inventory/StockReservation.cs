using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WarehouseManagementSystem.Domain.Common;
using WarehouseManagementSystem.Domain.Entities.MasterData;
using WarehouseManagementSystem.Domain.Entities.Organizations;
using WarehouseManagementSystem.Domain.Entities.Sales;
using WarehouseManagementSystem.Domain.Enums;

namespace WarehouseManagementSystem.Domain.Entities.Inventory
{
    public class StockReservation:AuditableEntity
    {
        public Guid SalesOrderItemId { get; set; }

        public Guid ProductId { get; set; }
        public Guid LocationId { get; set; }

        public Guid? LotId { get; set; }

        public decimal Quantity { get; set; }

        public StockReservationStatus Status { get; set; }= StockReservationStatus.Active;

        public SalesOrderItem SalesOrderItem { get; set; } = null!;

        public Location Location { get; set; } = null!;
        public Lot? Lot { get; set; }
        public Product Product { get; set; } = null!;
    }
}
