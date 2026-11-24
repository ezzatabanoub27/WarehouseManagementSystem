using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WarehouseManagementSystem.Domain.Common;
using WarehouseManagementSystem.Domain.Entities.Inventory;
using WarehouseManagementSystem.Domain.Entities.MasterData;
using WarehouseManagementSystem.Domain.Entities.Organizations;

namespace WarehouseManagementSystem.Domain.Entities.Sales
{
    public class ShipmentItem :AuditableEntity
    {

        public Guid ShipmentId { get; set; }
        public Guid SalesOrderItemId { get; set; }

        public Guid ProductId { get; set; }
        public Guid LocationId { get; set; }

        public Guid? LotId { get; set; }
        public Guid? SerialNumberId { get; set; }

        public decimal Quantity { get; set; }

        public Shipment Shipment { get; set; } = null!;
        public SalesOrderItem SalesOrderItem { get; set; } = null!;

        public Product Product { get; set; } = null!;
        public Location Location { get; set; } = null!;
        public SerialNumber? SerialNumber { get; set; }

        public Lot? Lot { get; set; }
    }
}
