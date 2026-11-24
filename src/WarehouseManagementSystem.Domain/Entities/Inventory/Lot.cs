using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WarehouseManagementSystem.Domain.Common;
using WarehouseManagementSystem.Domain.Entities.MasterData;

namespace WarehouseManagementSystem.Domain.Entities.Inventory
{
    public class Lot :AuditableEntity
    {
        public Guid ProductId { get; set; }

        public string LotNumber { get; set; } = string.Empty;

        public DateOnly? ManufacturingDate { get; set; }
        public DateOnly? ExpiryDate { get; set; }

        public Guid? SupplierId { get; set; }

        public Product Product { get; set; } = null!;
        public Supplier? Supplier { get; set; }

        public ICollection<StockBalance> StockBalances { get; set; }= new List<StockBalance>();
    }
}
