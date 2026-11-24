
using WarehouseManagementSystem.Domain.Entities.Organizations;

namespace WarehouseManagementSystem.Application.Abstractions.Repositories;

public interface IWarehouseRepository
{
    Task<List<Warehouse>> GetAllAsync(
        CancellationToken cancellationToken = default);

    Task<List<Warehouse>> GetByBranchIdAsync(
        Guid branchId,
        CancellationToken cancellationToken = default);

    Task<Warehouse?> GetByIdAsync(
        Guid id,
        CancellationToken cancellationToken = default);

    Task<Warehouse?> GetByCodeAsync(
        Guid branchId,
        string code,
        CancellationToken cancellationToken = default);

    Task AddAsync(
        Warehouse warehouse,
        CancellationToken cancellationToken = default);

    void Update(Warehouse warehouse);

    void Delete(Warehouse warehouse);

    Task<bool> ExistsAsync(
        Guid id,
        CancellationToken cancellationToken = default);
}
