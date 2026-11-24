
using Microsoft.AspNetCore.Mvc;
using WarehouseManagementSystem.Application.DTOs.Categories;
using WarehouseManagementSystem.Application.Services.Categories;

namespace WarehouseManagementSystem.Web.Controllers;

public class CategoriesController : Controller
{
    private readonly CategoryAppService _categoryAppService;

    public CategoriesController(CategoryAppService categoryAppService)
    {
        _categoryAppService = categoryAppService;
    }

    public async Task<IActionResult> Index(
        CancellationToken cancellationToken)
    {
        var categories = await _categoryAppService.GetAllAsync(
            cancellationToken);

        return View(categories);
    }

    public async Task<IActionResult> Details(
        Guid id,
        CancellationToken cancellationToken)
    {
        try
        {
            var category = await _categoryAppService.GetByIdAsync(
                id,
                cancellationToken);

            return View(category);
        }
        catch (KeyNotFoundException)
        {
            TempData["Error"] = "Category was not found.";
            return RedirectToAction(nameof(Index));
        }
    }

    [HttpGet]
    public async Task<IActionResult> Create(
        CancellationToken cancellationToken)
    {
        await LoadParentCategoriesAsync(cancellationToken);

        return View(new CreateCategoryDto());
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(
        CreateCategoryDto dto,
        CancellationToken cancellationToken)
    {
        if (!ModelState.IsValid)
        {
            await LoadParentCategoriesAsync(cancellationToken);
            return View(dto);
        }

        try
        {
            await _categoryAppService.CreateAsync(
                dto,
                cancellationToken);

            TempData["Success"] = "Category created successfully.";

            return RedirectToAction(nameof(Index));
        }
        catch (Exception ex) when (
            ex is ArgumentException ||
            ex is InvalidOperationException ||
            ex is KeyNotFoundException)
        {
            ModelState.AddModelError(string.Empty, ex.Message);

            await LoadParentCategoriesAsync(cancellationToken);

            return View(dto);
        }
    }

    [HttpGet]
    public async Task<IActionResult> Edit(
        Guid id,
        CancellationToken cancellationToken)
    {
        try
        {
            var category = await _categoryAppService.GetByIdAsync(
                id,
                cancellationToken);

            var dto = new UpdateCategoryDto
            {
                Id = category.Id,
                Name = category.Name,
                Description = category.Description,
                ParentCategoryId = category.ParentCategoryId,
                IsActive = category.IsActive
            };

            await LoadParentCategoriesAsync(
                cancellationToken,
                category.Id);

            return View(dto);
        }
        catch (KeyNotFoundException)
        {
            TempData["Error"] = "Category was not found.";
            return RedirectToAction(nameof(Index));
        }
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(
        UpdateCategoryDto dto,
        CancellationToken cancellationToken)
    {
        if (!ModelState.IsValid)
        {
            await LoadParentCategoriesAsync(
                cancellationToken,
                dto.Id);

            return View(dto);
        }

        try
        {
            await _categoryAppService.UpdateAsync(
                dto,
                cancellationToken);

            TempData["Success"] = "Category updated successfully.";

            return RedirectToAction(nameof(Index));
        }
        catch (Exception ex) when (
            ex is ArgumentException ||
            ex is InvalidOperationException ||
            ex is KeyNotFoundException)
        {
            ModelState.AddModelError(string.Empty, ex.Message);

            await LoadParentCategoriesAsync(
                cancellationToken,
                dto.Id);

            return View(dto);
        }
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Delete(
        Guid id,
        CancellationToken cancellationToken)
    {
        try
        {
            await _categoryAppService.DeleteAsync(
                id,
                cancellationToken);

            TempData["Success"] = "Category deleted successfully.";
        }
        catch (Exception ex) when (
            ex is InvalidOperationException ||
            ex is KeyNotFoundException)
        {
            TempData["Error"] = ex.Message;
        }

        return RedirectToAction(nameof(Index));
    }

    private async Task LoadParentCategoriesAsync(
        CancellationToken cancellationToken,
        Guid? excludedCategoryId = null)
    {
        var categories = await _categoryAppService.GetAllAsync(
            cancellationToken);

        ViewBag.ParentCategories = categories
            .Where(x =>
                x.IsActive &&
                (!excludedCategoryId.HasValue ||
                 x.Id != excludedCategoryId.Value))
            .OrderBy(x => x.Name)
            .ToList();
    }
}
