
using WarehouseManagementSystem.Domain.Entities.MasterData;

namespace WarehouseManagementSystem.Application.Abstractions.Repositories
{
    public interface IProductRepository
    {
        Task<List<Product>> GetAllAsync(
            CancellationToken cancellationToken = default);

        Task<Product?> GetByIdAsync(
            Guid id,
            CancellationToken cancellationToken = default);

        Task<Product?> GetBySkuAsync(
            string sku,
            CancellationToken cancellationToken = default);

        Task<Product?> GetByBarcodeAsync(
            string barcode,
            CancellationToken cancellationToken = default);

        Task<bool> ExistsAsync(
            Guid id,
            CancellationToken cancellationToken = default);

        Task AddAsync(
            Product product,
            CancellationToken cancellationToken = default);

        void Update(Product product);

        void Delete(Product product);
    }
}
