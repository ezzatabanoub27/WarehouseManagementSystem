
using WarehouseManagementSystem.Domain.Entities.Organizations;

namespace WarehouseManagementSystem.Application.Abstractions.Repositories;

public interface ILocationRepository
{
    Task<List<Location>> GetAllAsync(
        CancellationToken cancellationToken = default);

    Task<List<Location>> GetByWarehouseIdAsync(
        Guid warehouseId,
        CancellationToken cancellationToken = default);

    Task<Location?> GetByIdAsync(
        Guid id,
        CancellationToken cancellationToken = default);

    Task<Location?> GetByCodeAsync(
        Guid warehouseId,
        string code,
        CancellationToken cancellationToken = default);

    Task AddAsync(
        Location location,
        CancellationToken cancellationToken = default);

    void Update(Location location);

    void Delete(Location location);

    Task<bool> ExistsAsync(
        Guid id,
        CancellationToken cancellationToken = default);
}
