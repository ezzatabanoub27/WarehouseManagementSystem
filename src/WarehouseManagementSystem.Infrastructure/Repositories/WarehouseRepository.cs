using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using WarehouseManagementSystem.Application.Abstractions.Repositories;
using WarehouseManagementSystem.Domain.Entities.Organizations;
using WarehouseManagementSystem.Infrastructure.Persistence;

namespace WarehouseManagementSystem.Infrastructure.Repositories
{
    public class WarehouseRepository : IWarehouseRepository
    {

        private readonly WarehouseDbContext _db;
        public WarehouseRepository(WarehouseDbContext db)
        {
            _db = db;
        }
        public async Task AddAsync(Warehouse warehouse, CancellationToken cancellationToken = default)
        {
            await _db.Warehouses.AddAsync(warehouse,cancellationToken);
        }

        public void Delete(Warehouse warehouse)
        {
            _db.Warehouses.Remove(warehouse);
        }

        public async Task<bool> ExistsAsync(Guid id, CancellationToken cancellationToken = default)
        {
            return await _db.Warehouses.AnyAsync(x => x.Id == id, cancellationToken);
        }

        public async Task<List<Warehouse>> GetAllAsync(
     CancellationToken cancellationToken = default)
        {
            return await _db.Warehouses
                .AsNoTracking()
                .Include(x => x.Branch)
                .OrderBy(x => x.Branch.Name)
                .ThenBy(x => x.Name)
                .ToListAsync(cancellationToken);
        }

        public async Task<List<Warehouse>> GetByBranchIdAsync(Guid branchId, CancellationToken cancellationToken = default)
        {

            return await _db.Warehouses
            .AsNoTracking()
            .Include(x => x.Branch)
            .Where(x => x.BranchId == branchId)
            .OrderBy(x => x.Name)
            .ToListAsync(cancellationToken);
        }

        public async Task<Warehouse?> GetByCodeAsync(Guid branchId, string code, CancellationToken cancellationToken = default)
        {

            return await _db.Warehouses.AsNoTracking()
                .FirstOrDefaultAsync(x => x.BranchId == branchId &&
                 x.Code == code, cancellationToken);
        }

        public async Task<Warehouse?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
        {

            return await _db.Warehouses.Include(x => x.Branch)
            .FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
        }

        public void  Update(Warehouse warehouse)
        {
             _db.Warehouses.Update(warehouse);
        }
    }
}
