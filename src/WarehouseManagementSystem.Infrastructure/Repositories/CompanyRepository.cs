
using Microsoft.EntityFrameworkCore;

using WarehouseManagementSystem.Application.Abstractions.Repositories;
using WarehouseManagementSystem.Domain.Entities.Organizations;
using WarehouseManagementSystem.Infrastructure.Persistence;

namespace WarehouseManagementSystem.Infrastructure.Repositories;

public class CompanyRepository : ICompanyRepository
{
    private readonly WarehouseDbContext _context;

    public CompanyRepository(WarehouseDbContext context)
    {
        _context = context;
    }

    public async Task<List<Company>> GetAllAsync(
        CancellationToken cancellationToken = default)
    {
        return await _context.Companies
            .AsNoTracking()
            .OrderBy(x => x.Name)
            .ToListAsync(cancellationToken);
    }

    public async Task<Company?> GetByIdAsync(
        Guid id,
        CancellationToken cancellationToken = default)
    {
        return await _context.Companies
            .FirstOrDefaultAsync(
                x => x.Id == id,
                cancellationToken);
    }

    public async Task<Company?> GetByCodeAsync(
        string code,
        CancellationToken cancellationToken = default)
    {
        return await _context.Companies
            .FirstOrDefaultAsync(
                x => x.Code == code,
                cancellationToken);
    }

    public async Task AddAsync(
        Company company,
        CancellationToken cancellationToken = default)
    {
        await _context.Companies.AddAsync(
            company,
            cancellationToken);
    }

    public void Update(Company company)
    {
        _context.Companies.Update(company);
    }

    public void Delete(Company company)
    {
        _context.Companies.Remove(company);
    }

    public async Task<bool> ExistsAsync(
        Guid id,
        CancellationToken cancellationToken = default)
    {
        return await _context.Companies
            .AnyAsync(x => x.Id == id, cancellationToken);
    }
}
