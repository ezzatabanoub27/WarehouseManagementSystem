using Microsoft.EntityFrameworkCore;
using WarehouseManagementSystem.Application.Abstractions.Repositories;
using WarehouseManagementSystem.Domain.Entities.Organizations;
using WarehouseManagementSystem.Infrastructure.Persistence;

namespace WarehouseManagementSystem.Infrastructure.Repositories;

public class BranchRepository : IBranchRepository
{
    private readonly WarehouseDbContext _context;

    public BranchRepository(WarehouseDbContext context)
    {
        _context = context;
    }

    public async Task<List<Branch>> GetAllAsync(
        CancellationToken cancellationToken = default)
    {
        return await _context.Branches
            .AsNoTracking()
            .Include(x => x.Company)
            .OrderBy(x => x.Company.Name)
            .ThenBy(x => x.Name)
            .ToListAsync(cancellationToken);
    }

    public async Task<List<Branch>> GetByCompanyIdAsync(
        Guid companyId,
        CancellationToken cancellationToken = default)
    {
        return await _context.Branches
            .AsNoTracking()
            .Include(x=>x.Company)
            .Where(x => x.CompanyId == companyId)
            .OrderBy(x => x.Name)
            .ToListAsync(cancellationToken);
    }

    public async Task<Branch?> GetByIdAsync(
        Guid id,
        CancellationToken cancellationToken = default)
    {
        return await _context.Branches
            .Include(x => x.Company)
            .FirstOrDefaultAsync(
                x => x.Id == id,
                cancellationToken);
    }

    public async Task<Branch?> GetByCodeAsync(
        Guid companyId,
        string code,
        CancellationToken cancellationToken = default)
    {
        return await _context.Branches
            .FirstOrDefaultAsync(
                x => x.CompanyId == companyId &&
                     x.Code == code,
                cancellationToken);
    }

    public async Task AddAsync(
        Branch branch,
        CancellationToken cancellationToken = default)
    {
        await _context.Branches.AddAsync(
            branch,
            cancellationToken);
    }

    public void Update(Branch branch)
    {
        _context.Branches.Update(branch);
    }

    public void Delete(Branch branch)
    {
        _context.Branches.Remove(branch);
    }

    public async Task<bool> ExistsAsync(
        Guid id,
        CancellationToken cancellationToken = default)
    {
        return await _context.Branches
            .AnyAsync(x => x.Id == id, cancellationToken);
    }
}