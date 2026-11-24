using AutoMapper;
using Microsoft.EntityFrameworkCore.Query.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WarehouseManagementSystem.Application.Abstractions;
using WarehouseManagementSystem.Application.Abstractions.Repositories;
using WarehouseManagementSystem.Application.DTOs.Warehouses;
using WarehouseManagementSystem.Domain.Entities.Organizations;

namespace WarehouseManagementSystem.Application.Services.Warehouses
{
    public class WarehouseAppService
    {
        private readonly IWarehouseRepository _WarehouseRepo;
        private readonly IBranchRepository _branchRepo;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public WarehouseAppService(IWarehouseRepository repo, IMapper mapper, IBranchRepository branchRepo, IUnitOfWork unitOfWork)
        {
            _WarehouseRepo = repo;
            _mapper = mapper;
            _branchRepo = branchRepo;
            _unitOfWork = unitOfWork;
        }

        public async Task<List<WarehouseDto>> GetAllAsync()
        {
            var warehouse = await _WarehouseRepo.GetAllAsync();

            return _mapper.Map<List<WarehouseDto>>(warehouse);

        }


        public async Task<List<WarehouseDto>> GeyWarehouseByBranchIdAsync(Guid branchId)
        {
            var branch = await _branchRepo.GetByIdAsync(branchId);
            if (branch == null)
            {
                throw new KeyNotFoundException($"No Branches with this id : {branchId}");
            }

            var warhoses = await _WarehouseRepo.GetByBranchIdAsync(branchId);

            return _mapper.Map<List<WarehouseDto>>(warhoses);

        }

        public async Task<WarehouseDto> GetByIdAsync(Guid id)
        {
            var warehouse = await _WarehouseRepo.GetByIdAsync(id);

            if (warehouse == null)
            {
                throw new KeyNotFoundException($"No Warehouse with This Id : {id}");
            }

            return _mapper.Map<WarehouseDto>(warehouse);

        }


        public async Task<WarehouseDto> CreateAsync(CreateWarehouseDto dto)
        {
            ValidateWarehouseData(dto.Name, dto.Code, dto.BranchId);



            var branch = await _branchRepo.GetByIdAsync(dto.BranchId);
            if (branch == null)
            {
                throw new KeyNotFoundException($"Branch Not Found ");
            }
            if (!branch.IsActive)
            {
                throw new InvalidOperationException("Can not Create a Warehouse Under InActive Branch");
            }

            var name = dto.Name.Trim();
            var code = dto.Code.Trim();

            var exisitingCode = await _WarehouseRepo.GetByCodeAsync(dto.BranchId, code);

            if (exisitingCode != null)
            {
                throw new InvalidOperationException(
                "A warehouse with this code already exists in this branch.");
            }
            var warehouses = _mapper.Map<Warehouse>(dto);


            warehouses.Name = name;
            warehouses.Code = code;
            warehouses.IsActive = true;

            warehouses.SetCreationAudit(null);


            await _WarehouseRepo.AddAsync(warehouses);

            await _unitOfWork.SaveChangesAsync();

            var created = await _WarehouseRepo.GetByIdAsync(warehouses.Id);


            return _mapper.Map<WarehouseDto>(created);



        }


        public async Task <WarehouseDto> UpdateAsync(Guid id , UpdateWarehouseDto dto)
        {
            var warehouse = await _WarehouseRepo.GetByIdAsync(id);
             
            if (warehouse == null)
            {
                throw new KeyNotFoundException("No Warehouse Found ");
            }

            var existedBranch = await _branchRepo.GetByIdAsync(dto.BranchId);
            if(existedBranch == null)
            {
                throw new KeyNotFoundException("Branch Not Found");
            }
            if (!existedBranch.IsActive)
            {
                throw new InvalidOperationException("Branch Is Not Active ");
            }

            var name = dto.Name.Trim();
            var code = dto.Code.Trim();

            var existedWarehouse = await _WarehouseRepo.GetByCodeAsync(dto.BranchId, code);
            if(existedWarehouse!=null && existedWarehouse.Id != id)
            {
                throw new InvalidOperationException(
                "A warehouse with this code already exists in this branch.");
            }
            _mapper.Map(dto, warehouse);

            warehouse.Name = name;
            warehouse.Code = code;

            warehouse.SetModificationAudit(null);

            _WarehouseRepo.Update(warehouse);

            await _unitOfWork.SaveChangesAsync();

            var updatedWarehouse = await _WarehouseRepo
                .GetByIdAsync(id);

            return _mapper.Map<WarehouseDto>(updatedWarehouse);
        }



        public async Task DeleteAsync(Guid id)
        {
            var warehouse = await _WarehouseRepo.GetByIdAsync(id);

            if (warehouse == null)
                throw new KeyNotFoundException("Warehouse was not found.");

            _WarehouseRepo.Delete(warehouse);

            await _unitOfWork.SaveChangesAsync();
        }



        public async Task<bool> ExistsAsync(Guid id)
        {
            return await _WarehouseRepo.ExistsAsync(id);
        }



        private static void ValidateWarehouseData(
         string? name,
         string? code,
         Guid branchId)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentException("Warehouse name is required.");

            if (name.Trim().Length > 200)
                throw new ArgumentException(
                    "Warehouse name cannot exceed 200 characters.");


        }
    }
}