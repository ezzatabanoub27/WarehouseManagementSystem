using System.ComponentModel.DataAnnotations;

namespace WarehouseManagementSystem.Application.DTOs.Companies;

public class UpdateCompanyDto
{
    [Required(ErrorMessage = "Company name is required.")]
    [StringLength(200)]
    public string Name { get; set; } = string.Empty;

    [Required(ErrorMessage = "Company code is required.")]
    [StringLength(50)]
    public string Code { get; set; } = string.Empty;

    [StringLength(50)]
    public string? TaxNumber { get; set; }

    [StringLength(500)]
    public string? Address { get; set; }

    public bool IsActive { get; set; }
}