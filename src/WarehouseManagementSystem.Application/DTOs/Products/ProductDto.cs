
using WarehouseManagementSystem.Domain.Enums;

namespace WarehouseManagementSystem.Application.DTOs.Products
{
    public class ProductDto
    {
        public Guid Id { get; set; }

        public string SKU { get; set; } = string.Empty;
        public string? Barcode { get; set; }

        public string Name { get; set; } = string.Empty;
        public string? Description { get; set; }

        public string? ImagePath { get; set; }

        public Guid CategoryId { get; set; }
        public string CategoryName { get; set; } = string.Empty;

        public Guid? BaseUnitId { get; set; }
        public string? UnitName { get; set; } 
        public string? UnitSymbol { get; set; }

        public ProductTrackingMode TrackingMode { get; set; }

        public decimal CostPrice { get; set; }
        public decimal SellingPrice { get; set; }

        public bool IsActive { get; set; }

        public DateTime CreatedAtUtc { get; set; }
    }
}
