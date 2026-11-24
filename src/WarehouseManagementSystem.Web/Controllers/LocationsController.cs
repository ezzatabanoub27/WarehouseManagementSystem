
using Microsoft.AspNetCore.Mvc;
using WarehouseManagementSystem.Application.DTOs.Locations;
using WarehouseManagementSystem.Application.Services.Locations;
using WarehouseManagementSystem.Application.Services.Warehouses;

namespace WarehouseManagementSystem.Web.Controllers;

public class LocationsController : Controller
{
    private readonly LocationAppService _locationAppService;
    private readonly WarehouseAppService _warehouseAppService;

    public LocationsController(
        LocationAppService locationAppService,
        WarehouseAppService warehouseAppService)
    {
        _locationAppService = locationAppService;
        _warehouseAppService = warehouseAppService;
    }

    public async Task<IActionResult> Index()
    {
        var locations = await _locationAppService.GetAllAsync();
        return View(locations);
    }

    public async Task<IActionResult> Details(Guid id)
    {
        try
        {
            var location = await _locationAppService.GetByIdAsync(id);
            return View(location);
        }
        catch (KeyNotFoundException)
        {
            return NotFound();
        }
    }

    [HttpGet]
    public async Task<IActionResult> Create(Guid? warehouseId)
    {
        await LoadWarehousesAsync();

        var model = new CreateLocationDto();

        if (warehouseId.HasValue)
            model.WarehouseId = warehouseId.Value;

        return View(model);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(CreateLocationDto model)
    {
        if (!ModelState.IsValid)
        {
            await LoadWarehousesAsync();
            return View(model);
        }

        try
        {
            await _locationAppService.CreateAsync(model);

            TempData["SuccessMessage"] = "Location created successfully.";

            return RedirectToAction(nameof(Index));
        }
        catch (Exception ex) when (
            ex is ArgumentException ||
            ex is InvalidOperationException ||
            ex is KeyNotFoundException)
        {
            ModelState.AddModelError(string.Empty, ex.Message);

            await LoadWarehousesAsync();

            return View(model);
        }
    }

    [HttpGet]
    public async Task<IActionResult> Edit(Guid id)
    {
        try
        {
            var location = await _locationAppService.GetByIdAsync(id);

            var model = new UpdateLocationDto
            {
                Id = location.Id,
                WarehouseId = location.WarehouseId,
                Code = location.Code,
                Name = location.Name,
                LocationType = location.LocationType,
                IsActive = location.IsActive
            };

            await LoadWarehousesAsync();

            return View(model);
        }
        catch (KeyNotFoundException)
        {
            return NotFound();
        }
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(UpdateLocationDto model)
    {
        if (!ModelState.IsValid)
        {
            await LoadWarehousesAsync();
            return View(model);
        }

        try
        {
            await _locationAppService.UpdateAsync(model);

            TempData["SuccessMessage"] = "Location updated successfully.";

            return RedirectToAction(nameof(Index));
        }
        catch (Exception ex) when (
            ex is ArgumentException ||
            ex is InvalidOperationException ||
            ex is KeyNotFoundException)
        {
            ModelState.AddModelError(string.Empty, ex.Message);

            await LoadWarehousesAsync();

            return View(model);
        }
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Delete(Guid id)
    {
        try
        {
            await _locationAppService.DeleteAsync(id);

            TempData["SuccessMessage"] = "Location deleted successfully.";
        }
        catch (Exception ex) when (
            ex is InvalidOperationException ||
            ex is Microsoft.EntityFrameworkCore.DbUpdateException)
        {
            TempData["ErrorMessage"] =
                "This location cannot be deleted because it may be linked to inventory records.";
        }

        return RedirectToAction(nameof(Index));
    }

    private async Task LoadWarehousesAsync()
    {
        var warehouses = await _warehouseAppService.GetAllAsync();

        ViewBag.Warehouses = warehouses
            .Where(x => x.IsActive)
            .OrderBy(x => x.BranchName)
            .ThenBy(x => x.Name)
            .ToList();
    }
}
