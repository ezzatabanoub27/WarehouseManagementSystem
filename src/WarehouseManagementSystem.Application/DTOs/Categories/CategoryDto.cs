
namespace WarehouseManagementSystem.Application.DTOs.Categories;

public class CategoryDto
{
    public Guid Id { get; set; }

    public string Name { get; set; } = string.Empty;

    public string? Description { get; set; }

    public Guid? ParentCategoryId { get; set; }

    public string? ParentCategoryName { get; set; }

    public bool IsActive { get; set; }

    public DateTime CreatedAtUtc { get; set; }

    public int ProductsCount { get; set; }

    public int SubCategoriesCount { get; set; }
}
