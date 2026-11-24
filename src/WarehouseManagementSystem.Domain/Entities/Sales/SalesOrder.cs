using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WarehouseManagementSystem.Domain.Common;
using WarehouseManagementSystem.Domain.Entities.MasterData;
using WarehouseManagementSystem.Domain.Entities.Organizations;
using WarehouseManagementSystem.Domain.Enums;

namespace WarehouseManagementSystem.Domain.Entities.Sales
{
    public class SalesOrder:AuditableEntity
    {

        public string OrderNumber { get; set; } = string.Empty;

        public Guid CustomerId { get; set; }
        public Guid WarehouseId { get; set; }

        public DateTime OrderDate { get; set; }

        public DocumentStatus Status { get; set; } = DocumentStatus.Draft;

        public decimal SubTotal { get; set; }
        public decimal Discount { get; set; }
        public decimal Tax { get; set; }
        public decimal Total { get; set; }

        public string? Notes { get; set; }

        public Customer Customer { get; set; } = null!;
        public Warehouse Warehouse { get; set; } = null!;

        public ICollection<SalesOrderItem> Items { get; set; }
            = new List<SalesOrderItem>();

        public ICollection<Shipment> Shipments { get; set; }
            = new List<Shipment>();
    }
}
