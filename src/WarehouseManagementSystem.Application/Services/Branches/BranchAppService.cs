using AutoMapper;
using WarehouseManagementSystem.Application.Abstractions;
using WarehouseManagementSystem.Application.Abstractions.Repositories;
using WarehouseManagementSystem.Application.DTOs.Branches;
using WarehouseManagementSystem.Domain.Entities.Organizations;

namespace WarehouseManagementSystem.Application.Services.Branches;

public class BranchAppService
{
    private readonly IBranchRepository _branchRepository;
    private readonly ICompanyRepository _companyRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public BranchAppService(
        IBranchRepository branchRepository,
        ICompanyRepository companyRepository,
        IUnitOfWork unitOfWork,
        IMapper mapper)
    {
        _branchRepository = branchRepository;
        _companyRepository = companyRepository;
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<List<BranchDto>> GetAllAsync(
        CancellationToken cancellationToken = default)
    {
        var branches = await _branchRepository
            .GetAllAsync(cancellationToken);

        return _mapper.Map<List<BranchDto>>(branches);
    }

    public async Task<List<BranchDto>> GetByCompanyIdAsync(Guid companyId,CancellationToken cancellationToken = default)
    {
        var company = await _companyRepository
            .GetByIdAsync(companyId, cancellationToken);

        if (company == null)
            throw new KeyNotFoundException("Company not found.");

        var branches = await _branchRepository
            .GetByCompanyIdAsync(companyId, cancellationToken);

        return _mapper.Map<List<BranchDto>>(branches);
    }

    public async Task<BranchDto?> GetByIdAsync(
        Guid id,
        CancellationToken cancellationToken = default)
    {
        var branch = await _branchRepository
            .GetByIdAsync(id, cancellationToken);

        return branch == null
            ? null
            : _mapper.Map<BranchDto>(branch);
    }

    // Create Branch
    public async Task<BranchDto> CreateAsync(
        CreateBranchDto dto,
        CancellationToken cancellationToken = default)
    {
        ValidateBranchData(dto.Name, dto.Code, dto.CompanyId);

        var company = await _companyRepository
            .GetByIdAsync(dto.CompanyId, cancellationToken);

        if (company == null)
            throw new KeyNotFoundException("Company not found.");

        if (!company.IsActive)
            throw new InvalidOperationException(
                "Cannot create a branch for an inactive company.");

        dto.Name = dto.Name.Trim();
        dto.Code = dto.Code.Trim();

        var existingBranch = await _branchRepository
            .GetByCodeAsync(
                dto.CompanyId,
                dto.Code,
                cancellationToken);

        if (existingBranch != null)
            throw new InvalidOperationException(
                $"Branch code '{dto.Code}' already exists in this company.");

        var branch = _mapper.Map<Branch>(dto);

        branch.IsActive = true;

        branch.SetCreationAudit(null);

        await _branchRepository.AddAsync(
            branch,
            cancellationToken);

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        var createdBranch = await _branchRepository
            .GetByIdAsync(branch.Id, cancellationToken);

        return _mapper.Map<BranchDto>(createdBranch);
    }

    public async Task<BranchDto> UpdateAsync(
        Guid id,
        UpdateBranchDto dto,
        CancellationToken cancellationToken = default)
    {
        ValidateBranchData(dto.Name, dto.Code, dto.CompanyId);

        var branch = await _branchRepository
            .GetByIdAsync(id, cancellationToken);

        if (branch == null)
            throw new KeyNotFoundException("Branch not found.");

        var company = await _companyRepository
            .GetByIdAsync(dto.CompanyId, cancellationToken);

        if (company == null)
            throw new KeyNotFoundException("Company not found.");

        if (!company.IsActive && branch.CompanyId != dto.CompanyId)
            throw new InvalidOperationException(
                "Cannot move a branch to an inactive company.");

        dto.Name = dto.Name.Trim();
        dto.Code = dto.Code.Trim();

        var existingBranch = await _branchRepository
            .GetByCodeAsync(
                dto.CompanyId,
                dto.Code,
                cancellationToken);

        if (existingBranch != null && existingBranch.Id != id)
            throw new InvalidOperationException(
                $"Branch code '{dto.Code}' already exists in this company.");

        _mapper.Map(dto, branch);

        branch.SetModificationAudit(null);

        _branchRepository.Update(branch);

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        var updatedBranch = await _branchRepository
            .GetByIdAsync(id, cancellationToken);

        return _mapper.Map<BranchDto>(updatedBranch);
    }

    public async Task DeleteAsync(
        Guid id,
        CancellationToken cancellationToken = default)
    {
        var branch = await _branchRepository
            .GetByIdAsync(id, cancellationToken);

        if (branch == null)
            throw new KeyNotFoundException("Branch not found.");

        _branchRepository.Delete(branch);

        await _unitOfWork.SaveChangesAsync(cancellationToken);
    }

    public async Task<bool> ExistsAsync(
        Guid id,
        CancellationToken cancellationToken = default)
    {
        return await _branchRepository
            .ExistsAsync(id, cancellationToken);
    }

    private static void ValidateBranchData(
        string? name,
        string? code,
        Guid companyId)
    {
        if (companyId == Guid.Empty)
            throw new ArgumentException("Company is required.");

        if (string.IsNullOrWhiteSpace(name))
            throw new ArgumentException("Branch name is required.");

        if (name.Trim().Length > 200)
            throw new ArgumentException(
                "Branch name cannot exceed 200 characters.");

        if (string.IsNullOrWhiteSpace(code))
            throw new ArgumentException("Branch code is required.");

        if (code.Trim().Length > 50)
            throw new ArgumentException(
                "Branch code cannot exceed 50 characters.");
    }
}