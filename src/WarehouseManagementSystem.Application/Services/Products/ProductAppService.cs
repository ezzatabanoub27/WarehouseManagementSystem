
using AutoMapper;
using WarehouseManagementSystem.Application.Abstractions;
using WarehouseManagementSystem.Application.Abstractions.Repositories;
using WarehouseManagementSystem.Application.Abstractions.Services;
using WarehouseManagementSystem.Application.DTOs.Products;
using WarehouseManagementSystem.Domain.Entities.MasterData;

namespace WarehouseManagementSystem.Application.Services.Products;

public class ProductAppService
{
    private readonly IProductRepository _productRepository;
    private readonly ICategoryRepository _categoryRepository;
    private readonly IUnitRepository _unitRepository;
    private readonly IFileStorageService _fileStorageService;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public ProductAppService(
        IProductRepository productRepository,
        ICategoryRepository categoryRepository,
        IUnitRepository unitRepository,
        IUnitOfWork unitOfWork,
        IMapper mapper,
        IFileStorageService fileStorageService)
    {
        _productRepository = productRepository;
        _categoryRepository = categoryRepository;
        _unitRepository = unitRepository;
        _unitOfWork = unitOfWork;
        _mapper = mapper;
        _fileStorageService = fileStorageService;
    }

    public async Task<List<ProductDto>> GetAllAsync(
        CancellationToken cancellationToken = default)
    {
        var products = await _productRepository.GetAllAsync(
            cancellationToken);

        return _mapper.Map<List<ProductDto>>(products);
    }

    public async Task<ProductDto> GetByIdAsync(
        Guid id,
        CancellationToken cancellationToken = default)
    {
        var product = await _productRepository.GetByIdAsync(
            id,
            cancellationToken);

        if (product is null)
            throw new KeyNotFoundException("Product was not found.");

        return _mapper.Map<ProductDto>(product);
    }

    public async Task<ProductDto> CreateAsync(
        CreateProductDto dto,
        CancellationToken cancellationToken = default)
    {
        ValidateProduct(dto.SKU, dto.Name, dto.CostPrice, dto.SellingPrice);

        var sku = dto.SKU.Trim();
        var barcode = NormalizeBarcode(dto.Barcode);

        await EnsureUniqueSkuAsync(sku, null, cancellationToken);
        await EnsureUniqueBarcodeAsync(barcode, null, cancellationToken);

        await ValidateCategoryAsync(
            dto.CategoryId,
            cancellationToken);

        if (dto.BaseUnitId.HasValue)
        {
            await ValidateUnitAsync(dto.BaseUnitId.Value, cancellationToken);
        }

        var product = _mapper.Map<Product>(dto);

        product.SKU = sku;
        product.Barcode = barcode;
        product.Name = dto.Name.Trim();
        product.Description = dto.Description?.Trim();
        if (dto.Image != null)
        {
            product.ImagePath = await _fileStorageService
                .SaveProductImageAsync(dto.Image, cancellationToken);
        }
        product.IsActive = true;

        product.SetCreationAudit(null);

        await _productRepository.AddAsync(
            product,
            cancellationToken);

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        var createdProduct = await _productRepository.GetByIdAsync(
            product.Id,
            cancellationToken);

        return _mapper.Map<ProductDto>(createdProduct);
    }

    public async Task<ProductDto> UpdateAsync(
        UpdateProductDto dto,
        CancellationToken cancellationToken = default)
    {
        ValidateProduct(dto.SKU, dto.Name, dto.CostPrice, dto.SellingPrice);

        var product = await _productRepository.GetByIdAsync(
            dto.Id,
            cancellationToken);

        if (product is null)
            throw new KeyNotFoundException("Product was not found.");

        var sku = dto.SKU.Trim();
        var barcode = NormalizeBarcode(dto.Barcode);
        var oldImagePath = product.ImagePath;

        await EnsureUniqueSkuAsync(
            sku,
            dto.Id,
            cancellationToken);

        await EnsureUniqueBarcodeAsync(
            barcode,
            dto.Id,
            cancellationToken);

        await ValidateCategoryAsync(
            dto.CategoryId,
            cancellationToken);

        if (dto.BaseUnitId.HasValue)
        {
            await ValidateUnitAsync(dto.BaseUnitId.Value, cancellationToken);
        }

        _mapper.Map(dto, product);
        product.ImagePath = oldImagePath;

        if (dto.Image != null)
        {
            product.ImagePath = await _fileStorageService
                .SaveProductImageAsync(dto.Image, cancellationToken);
        }
        product.SKU = sku;
        product.Barcode = barcode;
        product.Name = dto.Name.Trim();
        product.Description = dto.Description?.Trim();

        // Keep the existing image until image upload is implemented.
        product.SetModificationAudit(null);

        _productRepository.Update(product);

        await _unitOfWork.SaveChangesAsync(cancellationToken);
        if (dto.Image != null &&
    !string.IsNullOrWhiteSpace(oldImagePath))
        {
            await _fileStorageService.DeleteFileAsync(
                oldImagePath,
                cancellationToken);
        }

        var updatedProduct = await _productRepository.GetByIdAsync(
            dto.Id,
            cancellationToken);

        return _mapper.Map<ProductDto>(updatedProduct);
    }

    public async Task DeleteAsync(
        Guid id,
        CancellationToken cancellationToken = default)
    {
        var product = await _productRepository.GetByIdAsync(
            id,
            cancellationToken);

        if (product is null)
            throw new KeyNotFoundException("Product was not found.");

        _productRepository.Delete(product);

        await _unitOfWork.SaveChangesAsync(cancellationToken);
    }

    public async Task<bool> ExistsAsync(
        Guid id,
        CancellationToken cancellationToken = default)
    {
        return await _productRepository.ExistsAsync(
            id,
            cancellationToken);
    }

    private async Task EnsureUniqueSkuAsync(
        string sku,
        Guid? excludedProductId,
        CancellationToken cancellationToken)
    {
        var existing = await _productRepository.GetBySkuAsync(
            sku,
            cancellationToken);

        if (existing is not null &&
            existing.Id != excludedProductId)
        {
            throw new InvalidOperationException(
                "A product with this SKU already exists.");
        }
    }

    private async Task EnsureUniqueBarcodeAsync(
        string? barcode,
        Guid? excludedProductId,
        CancellationToken cancellationToken)
    {
        if (string.IsNullOrWhiteSpace(barcode))
            return;

        var existing = await _productRepository.GetByBarcodeAsync(
            barcode,
            cancellationToken);

        if (existing is not null &&
            existing.Id != excludedProductId)
        {
            throw new InvalidOperationException(
                "A product with this barcode already exists.");
        }
    }

    private async Task ValidateCategoryAsync(
        Guid categoryId,
        CancellationToken cancellationToken)
    {
        var category = await _categoryRepository.GetByIdAsync(
            categoryId,
            cancellationToken);

        if (category is null)
            throw new KeyNotFoundException("Selected category was not found.");

        if (!category.IsActive)
        {
            throw new InvalidOperationException(
                "Cannot assign a product to an inactive category.");
        }
    }

    private async Task ValidateUnitAsync(
     Guid unitId,
     CancellationToken cancellationToken)
    {
        var unit = await _unitRepository.GetByIdAsync(
            unitId, cancellationToken);

        if (unit == null)
            throw new KeyNotFoundException("Selected unit was not found.");

        if (!unit.IsActive)
            throw new InvalidOperationException("Selected unit is inactive.");
    }

    private static void ValidateProduct(
        string? sku,
        string? name,
        decimal costPrice,
        decimal sellingPrice)
    {
        if (string.IsNullOrWhiteSpace(sku))
            throw new ArgumentException("Product SKU is required.");

        if (sku.Trim().Length > 50)
            throw new ArgumentException("Product SKU cannot exceed 50 characters.");

        if (string.IsNullOrWhiteSpace(name))
            throw new ArgumentException("Product name is required.");

        if (name.Trim().Length < 2 || name.Trim().Length > 200)
            throw new ArgumentException(
                "Product name must be between 2 and 200 characters.");

        if (costPrice < 0 || sellingPrice < 0)
            throw new ArgumentException("Product prices cannot be negative.");
    }




    public async Task<List<Category>> GetActiveCategoriesAsync(
        CancellationToken cancellationToken = default)
    {
        var categories = await _categoryRepository.GetAllAsync(
            cancellationToken);

        return categories
            .Where(x => x.IsActive)
            .OrderBy(x => x.Name)
            .ToList();
    }

    public async Task<List<Unit>> GetActiveUnitsAsync(
        CancellationToken cancellationToken = default)
    {
        return await _unitRepository.GetAllAsync(
            cancellationToken);
    }

    private static string? NormalizeBarcode(string? barcode)
    {
        return string.IsNullOrWhiteSpace(barcode)
            ? null
            : barcode.Trim();
    }
}
