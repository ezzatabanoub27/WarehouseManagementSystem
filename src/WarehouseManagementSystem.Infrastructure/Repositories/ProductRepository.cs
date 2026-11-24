
using Microsoft.EntityFrameworkCore;
using WarehouseManagementSystem.Application.Abstractions.Repositories;
using WarehouseManagementSystem.Domain.Entities.MasterData;
using WarehouseManagementSystem.Infrastructure.Persistence;

namespace WarehouseManagementSystem.Infrastructure.Persistence.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly WarehouseDbContext _context;

        public ProductRepository(WarehouseDbContext context)
        {
            _context = context;
        }

        public async Task<List<Product>> GetAllAsync(
            CancellationToken cancellationToken = default)
        {
            return await _context.Products
                .AsNoTracking()
                .Include(x => x.Category)
                .Include(x => x.BaseUnit)
                .OrderBy(x => x.Name)
                .ToListAsync(cancellationToken);
        }

        public async Task<Product?> GetByIdAsync(
            Guid id,
            CancellationToken cancellationToken = default)
        {
            return await _context.Products
                .Include(x => x.Category)
                .Include(x => x.BaseUnit)
                .FirstOrDefaultAsync(
                    x => x.Id == id,
                    cancellationToken);
        }

        public async Task<Product?> GetBySkuAsync(
     string sku,
     CancellationToken cancellationToken = default)
        {
            var normalizedSku = sku.Trim().ToUpper();

            return await _context.Products
                .FirstOrDefaultAsync(
                    x => x.SKU.ToUpper() == normalizedSku,
                    cancellationToken);
        }

        public async Task<Product?> GetByBarcodeAsync(
            string barcode,
            CancellationToken cancellationToken = default)
        {
            var normalizedBarcode = barcode.Trim().ToUpper();

            return await _context.Products
                .FirstOrDefaultAsync(
                    x => x.Barcode != null &&
                         x.Barcode.ToUpper() == normalizedBarcode,
                    cancellationToken);
        }

        public async Task<bool> ExistsAsync(
            Guid id,
            CancellationToken cancellationToken = default)
        {
            return await _context.Products
                .AnyAsync(x => x.Id == id, cancellationToken);
        }

        public async Task AddAsync(
            Product product,
            CancellationToken cancellationToken = default)
        {
            await _context.Products.AddAsync(product, cancellationToken);
        }

        public void Update(Product product)
        {
            _context.Products.Update(product);
        }

        public void Delete(Product product)
        {
            _context.Products.Remove(product);
        }
    }
}
