
using System.ComponentModel.DataAnnotations;

namespace WarehouseManagementSystem.Application.DTOs.Warehouses;

public class UpdateWarehouseDto
{


    public Guid Id { get; set; }
    [Required]
    public Guid BranchId { get; set; }

    [Required]
    [MaxLength(200)]
    public string Name { get; set; } = string.Empty;

    [Required]
    [MaxLength(50)]
    public string Code { get; set; } = string.Empty;

    [MaxLength(500)]
    public string? Address { get; set; }

    public bool IsActive { get; set; }
}
