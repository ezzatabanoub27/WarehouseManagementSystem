
using WarehouseManagementSystem.Application.Abstractions;

namespace WarehouseManagementSystem.Infrastructure.Persistence;

public class UnitOfWork : IUnitOfWork
{
    private readonly WarehouseDbContext _context;

    public UnitOfWork(WarehouseDbContext context)
    {
        _context = context;
    }

    public Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        return _context.SaveChangesAsync(cancellationToken);
    }
}
