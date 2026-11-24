
using Microsoft.EntityFrameworkCore;
using WarehouseManagementSystem.Application.Abstractions.Repositories;
using WarehouseManagementSystem.Domain.Entities.Organizations;
using WarehouseManagementSystem.Infrastructure.Persistence;

namespace WarehouseManagementSystem.Infrastructure.Persistence.Repositories;

public class LocationRepository : ILocationRepository
{
    private readonly WarehouseDbContext _context;

    public LocationRepository(WarehouseDbContext context)
    {
        _context = context;
    }

    public async Task<List<Location>> GetAllAsync(
        CancellationToken cancellationToken = default)
    {
        return await _context.Locations
            .AsNoTracking()
            .Include(x => x.Warehouse)
            .OrderBy(x => x.Warehouse.Name)
            .ThenBy(x => x.Code)
            .ToListAsync(cancellationToken);
    }

    public async Task<List<Location>> GetByWarehouseIdAsync(
        Guid warehouseId,
        CancellationToken cancellationToken = default)
    {
        return await _context.Locations
            .AsNoTracking()
            .Include(x => x.Warehouse)
            .Where(x => x.WarehouseId == warehouseId)
            .OrderBy(x => x.Code)
            .ToListAsync(cancellationToken);
    }

    public async Task<Location?> GetByIdAsync(
        Guid id,
        CancellationToken cancellationToken = default)
    {
        return await _context.Locations
            .Include(x => x.Warehouse)
            .FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
    }

    public async Task<Location?> GetByCodeAsync(
        Guid warehouseId,
        string code,
        CancellationToken cancellationToken = default)
    {
        return await _context.Locations
            .AsNoTracking()
            .FirstOrDefaultAsync(
                x => x.WarehouseId == warehouseId && x.Code == code,
                cancellationToken);
    }

    public async Task AddAsync(
        Location location,
        CancellationToken cancellationToken = default)
    {
        await _context.Locations.AddAsync(location, cancellationToken);
    }

    public void Update(Location location)
    {
        _context.Locations.Update(location);
    }

    public void Delete(Location location)
    {
        _context.Locations.Remove(location);
    }

    public async Task<bool> ExistsAsync(
        Guid id,
        CancellationToken cancellationToken = default)
    {
        return await _context.Locations
            .AnyAsync(x => x.Id == id, cancellationToken);
    }
}
