using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using WarehouseManagementSystem.Application.Abstractions.Repositories;
using WarehouseManagementSystem.Domain.Entities.MasterData;
using WarehouseManagementSystem.Infrastructure.Persistence;
namespace WarehouseManagementSystem.Infrastructure.Persistence.Repositories;

public class CategoryRepository : ICategoryRepository
{
    private readonly WarehouseDbContext _context;

    public CategoryRepository(WarehouseDbContext context)
    {
        _context = context;
    }

    public async Task<List<Category>> GetAllAsync(
        CancellationToken cancellationToken = default)
    {
        return await _context.Categories
            .AsNoTracking()
            .Include(x => x.ParentCategory)
            .OrderBy(x => x.ParentCategory == null ? 0 : 1)
            .ThenBy(x => x.ParentCategory!.Name)
            .ThenBy(x => x.Name)
            .ToListAsync(cancellationToken);
    }

    public async Task<Category?> GetByIdAsync(
        Guid id,
        CancellationToken cancellationToken = default)
    {
        return await _context.Categories
            .Include(x => x.ParentCategory)
            .FirstOrDefaultAsync(
                x => x.Id == id,
                cancellationToken);
    }

    public async Task<Category?> GetByNameAsync(
        string name,
        Guid? parentCategoryId,
        CancellationToken cancellationToken = default)
    {
        return await _context.Categories
            .AsNoTracking()
            .FirstOrDefaultAsync(
                x => x.Name == name &&
                     x.ParentCategoryId == parentCategoryId,
                cancellationToken);
    }

    public async Task<bool> HasProductsAsync(
        Guid categoryId,
        CancellationToken cancellationToken = default)
    {
        return await _context.Products
            .AnyAsync(
                x => x.CategoryId == categoryId,
                cancellationToken);
    }

    public async Task<bool> HasSubCategoriesAsync(
        Guid categoryId,
        CancellationToken cancellationToken = default)
    {
        return await _context.Categories
            .AnyAsync(
                x => x.ParentCategoryId == categoryId,
                cancellationToken);
    }

    public async Task<bool> ExistsAsync(
        Guid id,
        CancellationToken cancellationToken = default)
    {
        return await _context.Categories
            .AnyAsync(x => x.Id == id, cancellationToken);
    }

    public async Task AddAsync(
        Category category,
        CancellationToken cancellationToken = default)
    {
        await _context.Categories.AddAsync(
            category,
            cancellationToken);
    }

    public void Update(Category category)
    {
        _context.Categories.Update(category);
    }

    public void Delete(Category category)
    {
        _context.Categories.Remove(category);
    }

    public async Task<List<Category>> GetDescendantsAsync(
    Guid categoryId,
    CancellationToken cancellationToken = default)
    {
        var allCategories = await _context.Categories
            .AsNoTracking()
            .ToListAsync(cancellationToken);

        var descendants = new List<Category>();
        var parentIds = new HashSet<Guid> { categoryId };

        bool foundNew;

        do
        {
            var children = allCategories
                .Where(x =>
                    x.ParentCategoryId.HasValue &&
                    parentIds.Contains(x.ParentCategoryId.Value) &&
                    !descendants.Any(d => d.Id == x.Id))
                .ToList();

            foundNew = children.Count > 0;

            foreach (var child in children)
            {
                descendants.Add(child);
                parentIds.Add(child.Id);
            }

        } while (foundNew);

        return descendants;
    }

}

