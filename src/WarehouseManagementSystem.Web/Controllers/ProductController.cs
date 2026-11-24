
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using WarehouseManagementSystem.Application.DTOs.Products;
using WarehouseManagementSystem.Application.Services.Products;

namespace WarehouseManagementSystem.Web.Controllers;

public class ProductsController : Controller
{
    private readonly ProductAppService _productAppService;

    public ProductsController(ProductAppService productAppService)
    {
        _productAppService = productAppService;
    }

    public async Task<IActionResult> Index(
        CancellationToken cancellationToken)
    {
        var products = await _productAppService.GetAllAsync(
            cancellationToken);

        return View(products);
    }

    public async Task<IActionResult> Details(
        Guid id,
        CancellationToken cancellationToken)
    {
        try
        {
            var product = await _productAppService.GetByIdAsync(
                id,
                cancellationToken);

            return View(product);
        }
        catch (KeyNotFoundException)
        {
            return NotFound();
        }
    }

    [HttpGet]
    public async Task<IActionResult> Create(
     CancellationToken cancellationToken)
    {
        await LoadDropdownsAsync(
            cancellationToken: cancellationToken);

        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(
        CreateProductDto dto,
        CancellationToken cancellationToken)
    {
        if (!ModelState.IsValid)
        {
            await LoadDropdownsAsync(
                dto.CategoryId,
                dto.BaseUnitId,
                cancellationToken);

            return View(dto);
        }

        try
        {
            await _productAppService.CreateAsync(
                dto,
                cancellationToken);

            TempData["SuccessMessage"] = "Product created successfully.";

            return RedirectToAction(nameof(Index));
        }
        catch (Exception ex) when (
            ex is ArgumentException ||
            ex is InvalidOperationException ||
            ex is KeyNotFoundException)
        {
            ModelState.AddModelError(string.Empty, ex.Message);
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
            var product = await _productAppService.GetByIdAsync(
                id,
                cancellationToken);

            var dto = new UpdateProductDto
            {
                Id = product.Id,
                SKU = product.SKU,
                Barcode = product.Barcode,
                Name = product.Name,
                Description = product.Description,
                CategoryId = product.CategoryId,
                BaseUnitId = product.BaseUnitId,
                TrackingMode = product.TrackingMode,
                CostPrice = product.CostPrice,
                SellingPrice = product.SellingPrice,
                IsActive = product.IsActive

            };
            await LoadDropdownsAsync(
                        dto.CategoryId,
                        dto.BaseUnitId,
                        cancellationToken);

            return View(dto);
        }
        catch (KeyNotFoundException)
        {
            return NotFound();
        }
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(
        UpdateProductDto dto,
        CancellationToken cancellationToken)
    {
        if (!ModelState.IsValid)
            return View(dto);

        try
        {
            await _productAppService.UpdateAsync(
                dto,
                cancellationToken);

            TempData["SuccessMessage"] = "Product updated successfully.";

            return RedirectToAction(nameof(Index));
        }
        catch (Exception ex) when (
            ex is ArgumentException ||
            ex is InvalidOperationException ||
            ex is KeyNotFoundException)
        {
            ModelState.AddModelError(string.Empty, ex.Message);
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
            await _productAppService.DeleteAsync(
                id,
                cancellationToken);

            TempData["SuccessMessage"] = "Product deleted successfully.";
        }
        catch (Exception ex) when (
            ex is InvalidOperationException ||
            ex is KeyNotFoundException)
        {
            TempData["ErrorMessage"] = ex.Message;
        }

        return RedirectToAction(nameof(Index));
    }


    private async Task LoadDropdownsAsync(
        Guid? selectedCategoryId = null,
        Guid? selectedUnitId = null,
        CancellationToken cancellationToken = default)
    {
        var categories = await _productAppService.GetActiveCategoriesAsync(
            cancellationToken);

        var units = await _productAppService.GetActiveUnitsAsync(
            cancellationToken);

        ViewBag.Categories = new SelectList(
            categories,
            "Id",
            "Name",
            selectedCategoryId);

        ViewBag.Units = new SelectList(
            units,
            "Id",
            "Name",
            selectedUnitId);
    }

}
