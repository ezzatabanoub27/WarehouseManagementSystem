
using WarehouseManagementSystem.Domain.Entities.MasterData;

namespace WarehouseManagementSystem.Application.Abstractions.Repositories;

public interface ICategoryRepository
{
    Task<List<Category>> GetAllAsync(
        CancellationToken cancellationToken = default);

    Task<Category?> GetByIdAsync(
        Guid id,
        CancellationToken cancellationToken = default);

    Task<Category?> GetByNameAsync(
        string name,
        Guid? parentCategoryId,
        CancellationToken cancellationToken = default);

    Task<bool> HasProductsAsync(
        Guid categoryId,
        CancellationToken cancellationToken = default);

    Task<bool> HasSubCategoriesAsync(
        Guid categoryId,
        CancellationToken cancellationToken = default);

    Task<bool> ExistsAsync(
        Guid id,
        CancellationToken cancellationToken = default);

    Task AddAsync(
        Category category,
        CancellationToken cancellationToken = default);

    void Update(Category category);

    void Delete(Category category);

    Task<List<Category>> GetDescendantsAsync(
    Guid categoryId,
    CancellationToken cancellationToken = default);
}
