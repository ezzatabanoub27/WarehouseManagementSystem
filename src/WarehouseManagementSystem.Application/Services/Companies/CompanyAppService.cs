using AutoMapper;
using WarehouseManagementSystem.Application.Abstractions;
using WarehouseManagementSystem.Application.Abstractions.Repositories;
using WarehouseManagementSystem.Application.DTOs.Companies;
using WarehouseManagementSystem.Domain.Entities.Organizations;

namespace WarehouseManagementSystem.Application.Services.Companies;

public class CompanyAppService
{
    private readonly ICompanyRepository _companyRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public CompanyAppService(
        ICompanyRepository companyRepository,
        IUnitOfWork unitOfWork,
        IMapper mapper)
    {
        _companyRepository = companyRepository;
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    // Get All Companies
    public async Task<List<CompanyDto>> GetAllAsync(
        CancellationToken cancellationToken = default)
    {
        var companies = await _companyRepository
            .GetAllAsync(cancellationToken);

        return _mapper.Map<List<CompanyDto>>(companies);
    }

    // Get Company By Id
    public async Task<CompanyDto?> GetByIdAsync(
        Guid id,
        CancellationToken cancellationToken = default)
    {
        var company = await _companyRepository
            .GetByIdAsync(id, cancellationToken);

        if (company == null)
            return null;

        return _mapper.Map<CompanyDto>(company);
    }

    // Create Company
    public async Task<CompanyDto> CreateAsync(
        CreateCompanyDto dto,
        CancellationToken cancellationToken = default)
    {
        // Validation
        ValidateCompanyData(dto.Name, dto.Code);

        // Check Duplicate Code
        var existingCompany = await _companyRepository
            .GetByCodeAsync(dto.Code, cancellationToken);

        if (existingCompany != null)
            throw new InvalidOperationException(
                $"Company with code '{dto.Code}' already exists.");

        // DTO -> Entity
        var company = _mapper.Map<Company>(dto);

        // Set default values
        company.IsActive = true;

        // Set Audit Information
        company.SetCreationAudit(null);

        // Save
        await _companyRepository.AddAsync(
            company,
            cancellationToken);

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        // Entity -> DTO
        return _mapper.Map<CompanyDto>(company);
    }

    // Update Company
    public async Task<CompanyDto> UpdateAsync(
        Guid id,
        UpdateCompanyDto dto,
        CancellationToken cancellationToken = default)
    {
        // Get Existing Company
        var company = await _companyRepository
            .GetByIdAsync(id, cancellationToken);

        if (company == null)
            throw new KeyNotFoundException(
                $"Company with ID '{id}' was not found.");

        // Validation
        ValidateCompanyData(dto.Name, dto.Code);

        // Check Duplicate Code
        var existingCompany = await _companyRepository
            .GetByCodeAsync(dto.Code, cancellationToken);

        if (existingCompany != null && existingCompany.Id != id)
            throw new InvalidOperationException(
                $"Company with code '{dto.Code}' already exists.");

        // Map DTO -> Existing Entity
        _mapper.Map(dto, company);

        // Set Audit Information
        company.SetModificationAudit(null);

        // Update
        _companyRepository.Update(company);

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        // Entity -> DTO
        return _mapper.Map<CompanyDto>(company);
    }

    // Delete Company
    public async Task DeleteAsync(
        Guid id,
        CancellationToken cancellationToken = default)
    {
        var company = await _companyRepository
            .GetByIdAsync(id, cancellationToken);

        if (company == null)
            throw new KeyNotFoundException(
                $"Company with ID '{id}' was not found.");

        _companyRepository.Delete(company);

        await _unitOfWork.SaveChangesAsync(cancellationToken);
    }

    // Check If Company Exists
    public async Task<bool> ExistsAsync(
        Guid id,
        CancellationToken cancellationToken = default)
    {
        return await _companyRepository
            .ExistsAsync(id, cancellationToken);
    }

    // Private Validation Method
    private static void ValidateCompanyData(
        string? name,
        string? code)
    {
        if (string.IsNullOrWhiteSpace(name))
            throw new ArgumentException(
                "Company name is required.");

        if (name.Trim().Length > 200)
            throw new ArgumentException(
                "Company name cannot exceed 200 characters.");

        if (string.IsNullOrWhiteSpace(code))
            throw new ArgumentException(
                "Company code is required.");

        if (code.Trim().Length > 50)
            throw new ArgumentException(
                "Company code cannot exceed 50 characters.");
    }
}