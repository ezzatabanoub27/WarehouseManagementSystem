
using Microsoft.AspNetCore.Mvc;
using WarehouseManagementSystem.Application.DTOs.Branches;
using WarehouseManagementSystem.Application.Services.Branches;
using WarehouseManagementSystem.Application.Services.Companies;

namespace WarehouseManagementSystem.Web.Controllers;

public class BranchesController : Controller
{
    private readonly BranchAppService _branchAppService;
    private readonly CompanyAppService _companyAppService;

    public BranchesController(
        BranchAppService branchAppService,
        CompanyAppService companyAppService)
    {
        _branchAppService = branchAppService;
        _companyAppService = companyAppService;
    }

    // GET: Branches
    public async Task<IActionResult> Index()
    {
        var branches = await _branchAppService.GetAllAsync();
        return View(branches);
    }

    // GET: Branches/Details/{id}
    public async Task<IActionResult> Details(Guid id)
    {
        var branch = await _branchAppService.GetByIdAsync(id);

        if (branch == null)
            return NotFound();

        return View(branch);
    }

    // GET: Branches/Create
    [HttpGet]
    public async Task<IActionResult> Create()
    {
        await LoadCompaniesAsync();
        return View();
    }

    // POST: Branches/Create
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(CreateBranchDto dto)
    {
        if (!ModelState.IsValid)
        {
            await LoadCompaniesAsync();
            return View(dto);
        }

        try
        {
            await _branchAppService.CreateAsync(dto);

            TempData["SuccessMessage"] =
                "Branch created successfully.";

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

        await LoadCompaniesAsync();
        return View(dto);
    }

    // GET: Branches/Edit/{id}
    [HttpGet]
    public async Task<IActionResult> Edit(Guid id)
    {
        var branch = await _branchAppService.GetByIdAsync(id);

        if (branch == null)
            return NotFound();

        var dto = new UpdateBranchDto
        {
            CompanyId = branch.CompanyId,
            Name = branch.Name,
            Code = branch.Code,
            Address = branch.Address,
            IsActive = branch.IsActive
        };

        await LoadCompaniesAsync();

        return View(dto);
    }

    // POST: Branches/Edit/{id}
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(
        Guid id,
        UpdateBranchDto dto)
    {
        if (!ModelState.IsValid)
        {
            await LoadCompaniesAsync();
            return View(dto);
        }

        try
        {
            await _branchAppService.UpdateAsync(id, dto);

            TempData["SuccessMessage"] =
                "Branch updated successfully.";

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
            return NotFound();
        }

        await LoadCompaniesAsync();
        return View(dto);
    }

    // POST: Branches/Delete/{id}
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Delete(Guid id)
    {
        try
        {
            await _branchAppService.DeleteAsync(id);

            TempData["SuccessMessage"] =
                "Branch deleted successfully.";
        }
        catch (KeyNotFoundException)
        {
            return NotFound();
        }
        catch (InvalidOperationException ex)
        {
            TempData["ErrorMessage"] = ex.Message;
        }

        return RedirectToAction(nameof(Index));
    }

    private async Task LoadCompaniesAsync()
    {
        var companies = await _companyAppService.GetAllAsync();

        ViewBag.Companies = companies
            .Where(x => x.IsActive)
            .OrderBy(x => x.Name)
            .ToList();
    }
}
