
using WarehouseManagementSystem.Domain.Entities.MasterData;

namespace WarehouseManagementSystem.Application.Abstractions.Repositories;

public interface IUnitRepository
{
    Task<List<Unit>> GetAllAsync(
        CancellationToken cancellationToken = default);

    Task<Unit?> GetByIdAsync(
        Guid id,
        CancellationToken cancellationToken = default);

    Task<bool> ExistsAsync(
        Guid id,
        CancellationToken cancellationToken = default);
}
