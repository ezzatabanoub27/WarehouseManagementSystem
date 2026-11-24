
using System;
using WarehouseManagementSystem.Domain.Enums;

namespace WarehouseManagementSystem.Application.DTOs.Locations;

public class LocationDto
{
    public Guid Id { get; set; }

    public Guid WarehouseId { get; set; }

    public string WarehouseName { get; set; } = string.Empty;
    public string WarehouseCode { get; set; } = string.Empty;


    public string BranchName { get; set; } = string.Empty;

    public string CompanyName { get; set; } = string.Empty;

    public string Code { get; set; } = string.Empty;

    public string Name { get; set; } = string.Empty;

    public LocationType LocationType { get; set; }

    public bool IsActive { get; set; }

    public DateTime CreatedAtUtc { get; set; }
}
