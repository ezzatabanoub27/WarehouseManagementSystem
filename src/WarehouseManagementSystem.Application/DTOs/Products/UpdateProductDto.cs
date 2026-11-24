
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;
using WarehouseManagementSystem.Domain.Enums;

namespace WarehouseManagementSystem.Application.DTOs.Products
{
    public class UpdateProductDto
    {
        [Required]
        public Guid Id { get; set; }

        [Required]
        [StringLength(50)]
        public string SKU { get; set; } = string.Empty;

        [StringLength(100)]
        public string? Barcode { get; set; }

        [Required]
        [StringLength(200, MinimumLength = 2)]
        public string Name { get; set; } = string.Empty;

        [StringLength(500)]
        public string? Description { get; set; }

        [Required]
        public Guid CategoryId { get; set; }

        public Guid? BaseUnitId { get; set; }

        public ProductTrackingMode TrackingMode { get; set; }
            = ProductTrackingMode.None;

        [Range(typeof(decimal), "0", "999999999999999")]
        public decimal CostPrice { get; set; }

        [Range(typeof(decimal), "0", "999999999999999")]
        public decimal SellingPrice { get; set; }

        public bool IsActive { get; set; } = true;

        public IFormFile? Image { get; set; }
    }
}
