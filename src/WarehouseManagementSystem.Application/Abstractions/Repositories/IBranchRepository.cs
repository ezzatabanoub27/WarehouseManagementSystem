using WarehouseManagementSystem.Domain.Entities.Organizations;

namespace WarehouseManagementSystem.Application.Abstractions.Repositories;

public interface IBranchRepository
{
    Task<List<Branch>> GetAllAsync(
        CancellationToken cancellationToken = default);

    Task<List<Branch>> GetByCompanyIdAsync(
        Guid companyId,
        CancellationToken cancellationToken = default);

    Task<Branch?> GetByIdAsync(
        Guid id,
        CancellationToken cancellationToken = default);

    Task<Branch?> GetByCodeAsync(
        Guid companyId,
        string code,
        CancellationToken cancellationToken = default);

    Task AddAsync(
        Branch branch,
        CancellationToken cancellationToken = default);

    void Update(Branch branch);

    void Delete(Branch branch);

    Task<bool> ExistsAsync(
        Guid id,
        CancellationToken cancellationToken = default);
}