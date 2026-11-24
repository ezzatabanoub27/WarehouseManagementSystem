using Microsoft.AspNetCore.Mvc;
using WarehouseManagementSystem.Application.DTOs.Companies;
using WarehouseManagementSystem.Application.Services.Companies;

namespace WarehouseManagementSystem.Web.Controllers;

public class CompaniesController : Controller
{
    private readonly CompanyAppService _companyAppService;

    public CompaniesController(CompanyAppService companyAppService)
    {
        _companyAppService = companyAppService;
    }

    // GET: Companies
    public async Task<IActionResult> Index()
    {
        var companies = await _companyAppService.GetAllAsync();

        return View(companies);
    }

    // GET: Companies/Details/{id}
    public async Task<IActionResult> Details(Guid id)
    {
        var company = await _companyAppService.GetByIdAsync(id);

        if (company == null)
            return NotFound();

        return View(company);
    }

    // GET: Companies/Create
    [HttpGet]
    public IActionResult Create()
    {
        return View();
    }

    // POST: Companies/Create
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(CreateCompanyDto dto)
    {
        if (!ModelState.IsValid)
            return View(dto);

        try
        {
            await _companyAppService.CreateAsync(dto);

            TempData["SuccessMessage"] = "Company created successfully.";

            return RedirectToAction(nameof(Index));
        }
        catch (ArgumentException ex)
        {
            ModelState.AddModelError(string.Empty, ex.Message);
            return View(dto);
        }
        catch (InvalidOperationException ex)
        {
            ModelState.AddModelError(string.Empty, ex.Message);
            return View(dto);
        }
    }

    // GET: Companies/Edit/{id}
    [HttpGet]
    public async Task<IActionResult> Edit(Guid id)
    {
        var company = await _companyAppService.GetByIdAsync(id);

        if (company == null)
            return NotFound();

        var dto = new UpdateCompanyDto
        {
            Name = company.Name,
            Code = company.Code,
            TaxNumber = company.TaxNumber,
            Address = company.Address,
            IsActive = company.IsActive
        };

        return View(dto);
    }

    // POST: Companies/Edit/{id}
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(Guid id, UpdateCompanyDto dto)
    {
        if (!ModelState.IsValid)
            return View(dto);

        try
        {
            await _companyAppService.UpdateAsync(id, dto);

            TempData["SuccessMessage"] = "Company updated successfully.";

            return RedirectToAction(nameof(Index));
        }
        catch (ArgumentException ex)
        {
            ModelState.AddModelError(string.Empty, ex.Message);
            return View(dto);
        }
        catch (InvalidOperationException ex)
        {
            ModelState.AddModelError(string.Empty, ex.Message);
            return View(dto);
        }
        catch (KeyNotFoundException)
        {
            return NotFound();
        }
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Delete(Guid id)
    {
        try
        {
            await _companyAppService.DeleteAsync(id);

            TempData["SuccessMessage"] = "Company deleted successfully.";
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
}