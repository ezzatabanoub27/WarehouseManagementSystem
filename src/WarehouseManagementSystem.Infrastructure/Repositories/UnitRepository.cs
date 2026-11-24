
using Microsoft.EntityFrameworkCore;
using WarehouseManagementSystem.Application.Abstractions.Repositories;
using WarehouseManagementSystem.Domain.Entities.MasterData;
using WarehouseManagementSystem.Infrastructure.Persistence;

namespace WarehouseManagementSystem.Infrastructure.Persistence.Repositories;

public class UnitRepository : IUnitRepository
{
    private readonly WarehouseDbContext _context;

    public UnitRepository(WarehouseDbContext context)
    {
        _context = context;
    }

    public async Task<List<Unit>> GetAllAsync(
        CancellationToken cancellationToken = default)
    {
        return await _context.Units
            .AsNoTracking()
            .Where(x => x.IsActive)
            .OrderBy(x => x.Name)
            .ToListAsync(cancellationToken);
    }

    public async Task<Unit?> GetByIdAsync(
        Guid id,
        CancellationToken cancellationToken = default)
    {
        return await _context.Units
            .FirstOrDefaultAsync(
                x => x.Id == id,
                cancellationToken);
    }

    public async Task<bool> ExistsAsync(
        Guid id,
        CancellationToken cancellationToken = default)
    {
        return await _context.Units
            .AnyAsync(x => x.Id == id, cancellationToken);
    }
}
