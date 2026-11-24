
using AutoMapper;
using WarehouseManagementSystem.Application.Abstractions.Repositories;
using WarehouseManagementSystem.Application.Abstractions;
using WarehouseManagementSystem.Application.DTOs.Locations;
using WarehouseManagementSystem.Domain.Entities.Organizations;

namespace WarehouseManagementSystem.Application.Services.Locations;

public class LocationAppService
{
    private readonly ILocationRepository _locationRepository;
    private readonly IWarehouseRepository _warehouseRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public LocationAppService(
        ILocationRepository locationRepository,
        IWarehouseRepository warehouseRepository,
        IUnitOfWork unitOfWork,
        IMapper mapper)
    {
        _locationRepository = locationRepository;
        _warehouseRepository = warehouseRepository;
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<List<LocationDto>> GetAllAsync(
        CancellationToken cancellationToken = default)
    {
        var locations = await _locationRepository.GetAllAsync(cancellationToken);

        return _mapper.Map<List<LocationDto>>(locations);
    }

    public async Task<List<LocationDto>> GetByWarehouseIdAsync(
        Guid warehouseId,
        CancellationToken cancellationToken = default)
    {
        if (!await _warehouseRepository.ExistsAsync(warehouseId, cancellationToken))
            throw new KeyNotFoundException("Warehouse was not found.");

        var locations = await _locationRepository
            .GetByWarehouseIdAsync(warehouseId, cancellationToken);

        return _mapper.Map<List<LocationDto>>(locations);
    }

    public async Task<LocationDto> GetByIdAsync(
        Guid id,
        CancellationToken cancellationToken = default)
    {
        var location = await _locationRepository.GetByIdAsync(id, cancellationToken);

        if (location == null)
            throw new KeyNotFoundException("Location was not found.");

        return _mapper.Map<LocationDto>(location);
    }

    public async Task<LocationDto> CreateAsync(
        CreateLocationDto dto,
        CancellationToken cancellationToken = default)
    {
        Validate(dto.WarehouseId, dto.Code, dto.Name);

        var warehouse = await _warehouseRepository
            .GetByIdAsync(dto.WarehouseId, cancellationToken);

        if (warehouse == null)
            throw new KeyNotFoundException("Warehouse was not found.");

        if (!warehouse.IsActive)
            throw new InvalidOperationException("Cannot add a location to an inactive warehouse.");

        var code = dto.Code.Trim();
        var name = dto.Name.Trim();

        var existingLocation = await _locationRepository
            .GetByCodeAsync(dto.WarehouseId, code, cancellationToken);

        if (existingLocation != null)
            throw new InvalidOperationException(
                "A location with this code already exists in this warehouse.");

        var location = _mapper.Map<Location>(dto);

        location.Code = code;
        location.Name = name;
        location.IsActive = true;
        location.SetCreationAudit(null);

        await _locationRepository.AddAsync(location, cancellationToken);

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        var createdLocation = await _locationRepository
            .GetByIdAsync(location.Id, cancellationToken);

        return _mapper.Map<LocationDto>(createdLocation);
    }

    public async Task<LocationDto> UpdateAsync(
        UpdateLocationDto dto,
        CancellationToken cancellationToken = default)
    {
        Validate(dto.WarehouseId, dto.Code, dto.Name);

        var location = await _locationRepository
            .GetByIdAsync(dto.Id, cancellationToken);

        if (location == null)
            throw new KeyNotFoundException("Location was not found.");

        var warehouse = await _warehouseRepository
            .GetByIdAsync(dto.WarehouseId, cancellationToken);

        if (warehouse == null)
            throw new KeyNotFoundException("Warehouse was not found.");

        if (!warehouse.IsActive)
            throw new InvalidOperationException("Cannot assign a location to an inactive warehouse.");

        var code = dto.Code.Trim();
        var name = dto.Name.Trim();

        var existingLocation = await _locationRepository
            .GetByCodeAsync(dto.WarehouseId, code, cancellationToken);

        if (existingLocation != null && existingLocation.Id != dto.Id)
            throw new InvalidOperationException(
                "A location with this code already exists in this warehouse.");

        location.WarehouseId = dto.WarehouseId;
        location.Code = code;
        location.Name = name;
        location.LocationType = dto.LocationType;
        location.IsActive = dto.IsActive;

        location.SetModificationAudit(null);

        _locationRepository.Update(location);

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        var updatedLocation = await _locationRepository
            .GetByIdAsync(location.Id, cancellationToken);

        return _mapper.Map<LocationDto>(updatedLocation);
    }

    public async Task DeleteAsync(
        Guid id,
        CancellationToken cancellationToken = default)
    {
        var location = await _locationRepository.GetByIdAsync(id, cancellationToken);

        if (location == null)
            throw new KeyNotFoundException("Location was not found.");

        _locationRepository.Delete(location);

        await _unitOfWork.SaveChangesAsync(cancellationToken);
    }

    public async Task<bool> ExistsAsync(
        Guid id,
        CancellationToken cancellationToken = default)
    {
        return await _locationRepository.ExistsAsync(id, cancellationToken);
    }

    private static void Validate(
        Guid warehouseId,
        string? code,
        string? name)
    {
        if (warehouseId == Guid.Empty)
            throw new ArgumentException("Warehouse is required.");

        if (string.IsNullOrWhiteSpace(code))
            throw new ArgumentException("Location code is required.");

        if (code.Trim().Length > 50)
            throw new ArgumentException("Location code cannot exceed 50 characters.");

        if (string.IsNullOrWhiteSpace(name))
            throw new ArgumentException("Location name is required.");

        if (name.Trim().Length > 200)
            throw new ArgumentException("Location name cannot exceed 200 characters.");
    }
}
