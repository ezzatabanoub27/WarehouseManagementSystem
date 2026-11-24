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
    public class StockBalance :AuditableEntity
    {
        public Guid ProductId { get; set; }
        public Guid LocationId { get; set; }

        public Guid? LotId { get; set; }

        public decimal OnHandQuantity { get; set; }

        public decimal ReservedQuantity { get; set; }

        public byte[] RowVersion { get; set; } = Array.Empty<byte>();

        public decimal AvailableQuantity =>OnHandQuantity - ReservedQuantity;

        public Location Location { get; set; } = null!;
        public Lot? Lot { get; set; }
        public Product Product { get; set; } = null!;
    }
}
