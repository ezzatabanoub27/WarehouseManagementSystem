
using System.ComponentModel.DataAnnotations;
using WarehouseManagementSystem.Domain.Enums;

namespace WarehouseManagementSystem.Application.DTOs.Locations;

public class CreateLocationDto
{
    [Required]
    public Guid WarehouseId { get; set; }

    [Required]
    [MaxLength(50)]
    public string Code { get; set; } = string.Empty;

    [Required]
    [MaxLength(200)]
    public string Name { get; set; } = string.Empty;

    [Required]
    [Range(1, 6)]
    public LocationType LocationType { get; set; }
}
