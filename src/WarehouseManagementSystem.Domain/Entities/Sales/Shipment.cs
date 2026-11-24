using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WarehouseManagementSystem.Domain.Common;
using WarehouseManagementSystem.Domain.Entities.Inventory;
using WarehouseManagementSystem.Domain.Entities.Organizations;
using WarehouseManagementSystem.Domain.Enums;

namespace WarehouseManagementSystem.Domain.Entities.Sales
{
    public class Shipment :AuditableEntity
    {

        public string ShipmentNumber { get; set; } = string.Empty;

        public Guid SalesOrderId { get; set; }
        public Guid WarehouseId { get; set; }

        public DateTime ShipmentDate { get; set; }

        public DocumentStatus Status { get; set; } = DocumentStatus.Draft;

        public string? Notes { get; set; }

        public SalesOrder SalesOrder { get; set; } = null!;
        public Warehouse Warehouse { get; set; } = null!;
        public Lot? Lot { get; set; }
        public SerialNumber? SerialNumber { get; set; }
        public ICollection<ShipmentItem> Items { get; set; }= new List<ShipmentItem>();

    }
}
