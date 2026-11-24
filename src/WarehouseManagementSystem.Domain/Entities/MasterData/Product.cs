using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WarehouseManagementSystem.Domain.Common;
using WarehouseManagementSystem.Domain.Entities.Inventory;
using WarehouseManagementSystem.Domain.Enums;

namespace WarehouseManagementSystem.Domain.Entities.MasterData
{
    public class Product :AuditableEntity
    {
        public string SKU { get; set; } = string.Empty;
        public string? Barcode { get; set; }

        public string Name { get; set; } = string.Empty;
        public string? Description { get; set; }

        public Guid CategoryId { get; set; }
        public Guid? BaseUnitId { get; set; }

        public ProductTrackingMode TrackingMode { get; set; } = ProductTrackingMode.None;

        public decimal CostPrice { get; set; }
        public decimal SellingPrice { get; set; }

        public bool IsActive { get; set; } = true;

        public Category Category { get; set; } = null!;
        public Unit? BaseUnit { get; set; } = null!;
        public ICollection<Lot> Lots { get; set; } = new List<Lot>();

        public string? ImagePath { get; set; }

        public ICollection<SerialNumber> SerialNumbers { get; set; }
            = new List<SerialNumber>();
        public ICollection<ProductUnitConversion> UnitConversions { get; set; } = new List<ProductUnitConversion>();
    }
}
