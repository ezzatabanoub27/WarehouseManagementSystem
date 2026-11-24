
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using WarehouseManagementSystem.Application.DTOs.Warehouses;
using WarehouseManagementSystem.Application.Services.Branches;
using WarehouseManagementSystem.Application.Services.Warehouses;

namespace WarehouseManagementSystem.Web.Controllers;

public class WarehousesController : Controller
{
    private readonly WarehouseAppService _warehouseAppService;
    private readonly BranchAppService _branchAppService;

    public WarehousesController(
        WarehouseAppService warehouseAppService,
        BranchAppService branchAppService)
    {
        _warehouseAppService = warehouseAppService;
        _branchAppService = branchAppService;
    }

    public async Task<IActionResult> Index()
    {
        var warehouses = await _warehouseAppService.GetAllAsync();

        return View(warehouses);
    }

    public async Task<IActionResult> Details(Guid id)
    {
        var warehouse = await _warehouseAppService.GetByIdAsync(id);
        return View(warehouse);
    }

    public async Task<IActionResult> Create()
    {
        await LoadBranchesAsync();

        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(CreateWarehouseDto dto)
    {
        if (!ModelState.IsValid)
        {
            await LoadBranchesAsync();
            return View(dto);
        }

        try
        {
            await _warehouseAppService.CreateAsync(dto);

            TempData["SuccessMessage"] = "Warehouse created successfully.";

            return RedirectToAction(nameof(Index));
        }
        catch (ArgumentException ex)
        {
            ModelState.AddModelError(string.Empty, ex.Message);
        }
        catch (InvalidOperationException ex)
        {
            ModelState.AddModelError(string.Empty, ex.Message);
        }
        catch (KeyNotFoundException ex)
        {
            ModelState.AddModelError(string.Empty, ex.Message);
        }

        await LoadBranchesAsync();

        return View(dto);
    }

    public async Task<IActionResult> Edit(Guid id)
    {
        var warehouse = await _warehouseAppService.GetByIdAsync(id);

        var model = new UpdateWarehouseDto
        {
            Id = warehouse.Id,
            BranchId = warehouse.BranchId,
            Name = warehouse.Name,
            Code = warehouse.Code,
            Address = warehouse.Address,
            IsActive = warehouse.IsActive
        };

        await LoadBranchesAsync();

        return View(model);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(Guid id, UpdateWarehouseDto dto)
    {
        if (!ModelState.IsValid)
        {
            await LoadBranchesAsync();
            return View(dto);
        }

        try
        {
            await _warehouseAppService.UpdateAsync(id, dto);

            TempData["SuccessMessage"] = "Warehouse updated successfully.";

            return RedirectToAction(nameof(Index));
        }
        catch (ArgumentException ex)
        {
            ModelState.AddModelError(string.Empty, ex.Message);
        }
        catch (InvalidOperationException ex)
        {
            ModelState.AddModelError(string.Empty, ex.Message);
        }
        catch (KeyNotFoundException ex)
        {
            ModelState.AddModelError(string.Empty, ex.Message);
        }

        await LoadBranchesAsync();

        return View(dto);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Delete(Guid id)
    {
        try
        {
            await _warehouseAppService.DeleteAsync(id);

            TempData["SuccessMessage"] = "Warehouse deleted successfully.";
        }
        catch (KeyNotFoundException)
        {
            TempData["ErrorMessage"] = "Warehouse was not found.";
        }
        catch (InvalidOperationException ex)
        {
            TempData["ErrorMessage"] = ex.Message;
        }

        return RedirectToAction(nameof(Index));
    }

    private async Task LoadBranchesAsync()
    {
        var branches = await _branchAppService.GetAllAsync();

        ViewBag.Branches = branches
            .Where(x => x.IsActive)
            .OrderBy(x => x.CompanyName)
            .ThenBy(x => x.Name)
            .ToList();
    }
}
