
using AutoMapper;
using WarehouseManagementSystem.Application.Abstractions;
using WarehouseManagementSystem.Application.Abstractions.Repositories;
using WarehouseManagementSystem.Application.DTOs.Categories;
using WarehouseManagementSystem.Domain.Entities.MasterData;

namespace WarehouseManagementSystem.Application.Services.Categories;

public class CategoryAppService
{
    private readonly ICategoryRepository _categoryRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public CategoryAppService(
        ICategoryRepository categoryRepository,
        IUnitOfWork unitOfWork,
        IMapper mapper)
    {
        _categoryRepository = categoryRepository;
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<List<CategoryDto>> GetAllAsync(
        CancellationToken cancellationToken = default)
    {
        var categories = await _categoryRepository.GetAllAsync(
            cancellationToken);

        return _mapper.Map<List<CategoryDto>>(categories);
    }

    public async Task<CategoryDto> GetByIdAsync(
        Guid id,
        CancellationToken cancellationToken = default)
    {
        var category = await _categoryRepository.GetByIdAsync(
            id,
            cancellationToken);

        if (category is null)
            throw new KeyNotFoundException("Category was not found.");

        return _mapper.Map<CategoryDto>(category);
    }

    public async Task<CategoryDto> CreateAsync(
        CreateCategoryDto dto,
        CancellationToken cancellationToken = default)
    {
        ValidateName(dto.Name);

        var name = dto.Name.Trim();

        await ValidateParentAsync(
            dto.ParentCategoryId,
            null,
            cancellationToken);

        await EnsureUniqueNameAsync(
            name,
            dto.ParentCategoryId,
            null,
            cancellationToken);

        var category = _mapper.Map<Category>(dto);

        category.Name = name;
        category.Description = dto.Description?.Trim();
        category.IsActive = true;
        category.SetCreationAudit(null);

        await _categoryRepository.AddAsync(
            category,
            cancellationToken);

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        var createdCategory = await _categoryRepository.GetByIdAsync(
            category.Id,
            cancellationToken);

        return _mapper.Map<CategoryDto>(createdCategory);
    }

    public async Task<CategoryDto> UpdateAsync(
        UpdateCategoryDto dto,
        CancellationToken cancellationToken = default)
    {
        ValidateName(dto.Name);

        var category = await _categoryRepository.GetByIdAsync(
            dto.Id,
            cancellationToken);

        if (category is null)
            throw new KeyNotFoundException("Category was not found.");

        await ValidateParentAsync(
            dto.ParentCategoryId,
            dto.Id,
            cancellationToken);

        var name = dto.Name.Trim();

        await EnsureUniqueNameAsync(
            name,
            dto.ParentCategoryId,
            dto.Id,
            cancellationToken);

        category.Name = name;
        category.Description = dto.Description?.Trim();
        category.ParentCategoryId = dto.ParentCategoryId;
        category.IsActive = dto.IsActive;

        category.SetModificationAudit(null);

        _categoryRepository.Update(category);

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        var updatedCategory = await _categoryRepository.GetByIdAsync(
            dto.Id,
            cancellationToken);

        return _mapper.Map<CategoryDto>(updatedCategory);
    }

    public async Task DeleteAsync(
        Guid id,
        CancellationToken cancellationToken = default)
    {
        var category = await _categoryRepository.GetByIdAsync(
            id,
            cancellationToken);

        if (category is null)
            throw new KeyNotFoundException("Category was not found.");

        if (await _categoryRepository.HasProductsAsync(
                id, cancellationToken))
        {
            throw new InvalidOperationException(
                "Cannot delete a category that contains products.");
        }

        if (await _categoryRepository.HasSubCategoriesAsync(
                id, cancellationToken))
        {
            throw new InvalidOperationException(
                "Cannot delete a category that contains subcategories.");
        }

        _categoryRepository.Delete(category);

        await _unitOfWork.SaveChangesAsync(cancellationToken);
    }

    public async Task<bool> ExistsAsync(
        Guid id,
        CancellationToken cancellationToken = default)
    {
        return await _categoryRepository.ExistsAsync(
            id,
            cancellationToken);
    }

    private static void ValidateName(string? name)
    {
        if (string.IsNullOrWhiteSpace(name))
        {
            throw new ArgumentException(
                "Category name is required.");
        }

        if (name.Trim().Length < 2 || name.Trim().Length > 150)
        {
            throw new ArgumentException(
                "Category name must be between 2 and 150 characters.");
        }
    }

    private async Task ValidateParentAsync(
        Guid? parentCategoryId,
        Guid? currentCategoryId,
        CancellationToken cancellationToken)
    {
        if (!parentCategoryId.HasValue)
            return;

        if (currentCategoryId.HasValue &&
            parentCategoryId.Value == currentCategoryId.Value)
        {
            throw new InvalidOperationException(
                "A category cannot be its own parent.");
        }

        var parent = await _categoryRepository.GetByIdAsync(
            parentCategoryId.Value,
            cancellationToken);

        if (parent is null)
        {
            throw new KeyNotFoundException(
                "Selected parent category was not found.");
        }

        if (!parent.IsActive)
        {
            throw new InvalidOperationException(
                "Cannot select an inactive parent category.");
        }

        if (currentCategoryId.HasValue)
        {
            var descendants =
                await _categoryRepository.GetDescendantsAsync(
                    currentCategoryId.Value,
                    cancellationToken);

            if (descendants.Any(x => x.Id == parentCategoryId.Value))
            {
                throw new InvalidOperationException(
                    "A category cannot be moved under one of its descendants.");
            }
        }
    }

    private async Task EnsureUniqueNameAsync(
        string name,
        Guid? parentCategoryId,
        Guid? excludedCategoryId,
        CancellationToken cancellationToken)
    {
        var existing = await _categoryRepository.GetByNameAsync(
            name,
            parentCategoryId,
            cancellationToken);

        if (existing is not null &&
            existing.Id != excludedCategoryId)
        {
            throw new InvalidOperationException(
                "A category with this name already exists under the selected parent.");
        }
    }



    public async Task<HashSet<Guid>> GetDescendantIdsAsync(
    Guid categoryId,
    CancellationToken cancellationToken = default)
    {
        var descendants = await _categoryRepository.GetDescendantsAsync(
            categoryId,
            cancellationToken);

        return descendants
            .Select(x => x.Id)
            .ToHashSet();
    }
}
