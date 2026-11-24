using System.ComponentModel.DataAnnotations;

namespace WarehouseManagementSystem.Application.DTOs.Branches;

public class UpdateBranchDto
{
    [Required(ErrorMessage = "Company is required.")]
    public Guid CompanyId { get; set; }

    [Required(ErrorMessage = "Branch name is required.")]
    [StringLength(200)]
    public string Name { get; set; } = string.Empty;

    [Required(ErrorMessage = "Branch code is required.")]
    [StringLength(50)]
    public string Code { get; set; } = string.Empty;

    [StringLength(500)]
    public string? Address { get; set; }

    public bool IsActive { get; set; }
}