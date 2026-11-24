
namespace WarehouseManagementSystem.Application.DTOs.Warehouses;

public class WarehouseDto
{
    public Guid Id { get; set; }

    public Guid BranchId { get; set; }

    public string BranchName { get; set; } = string.Empty;

    public string Name { get; set; } = string.Empty;

    public string Code { get; set; } = string.Empty;

    public string? Address { get; set; }

    public bool IsActive { get; set; }

    public DateTime CreatedAtUtc { get; set; }
}
