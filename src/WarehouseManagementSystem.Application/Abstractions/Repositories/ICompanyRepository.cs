
using WarehouseManagementSystem.Domain.Entities.Organizations;

namespace WarehouseManagementSystem.Application.Abstractions.Repositories;

public interface ICompanyRepository
{
    Task<List<Company>> GetAllAsync(
        CancellationToken cancellationToken = default);

    Task<Company?> GetByIdAsync(
        Guid id,
        CancellationToken cancellationToken = default);

    Task<Company?> GetByCodeAsync(
        string code,
        CancellationToken cancellationToken = default);

    Task AddAsync(
        Company company,
        CancellationToken cancellationToken = default);

    void Update(Company company);

    void Delete(Company company);

    Task<bool> ExistsAsync(
        Guid id,
        CancellationToken cancellationToken = default);
}
