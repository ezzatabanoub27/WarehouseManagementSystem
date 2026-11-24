using AutoMapper;
using WarehouseManagementSystem.Application.DTOs.Branches;
using WarehouseManagementSystem.Application.DTOs.Categories;
using WarehouseManagementSystem.Application.DTOs.Companies;
using WarehouseManagementSystem.Application.DTOs.Locations;
using WarehouseManagementSystem.Application.DTOs.Products;
using WarehouseManagementSystem.Application.DTOs.Warehouses;
using WarehouseManagementSystem.Domain.Entities.MasterData;
using WarehouseManagementSystem.Domain.Entities.Organizations;

namespace WarehouseManagementSystem.Application.Mapping;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        ///Company Mapping 
        CreateMap<Company, CompanyDto>();

        CreateMap<CreateCompanyDto, Company>()
            .ForMember(dest => dest.Id, opt => opt.Ignore())
            .ForMember(dest => dest.CreatedAtUtc, opt => opt.Ignore())
            .ForMember(dest => dest.CreatedBy, opt => opt.Ignore())
            .ForMember(dest => dest.LastModifiedAtUtc, opt => opt.Ignore())
            .ForMember(dest => dest.LastModifiedBy, opt => opt.Ignore())
            .ForMember(dest => dest.Branches, opt => opt.Ignore());

        CreateMap<UpdateCompanyDto, Company>()
            .ForMember(dest => dest.Id, opt => opt.Ignore())
            .ForMember(dest => dest.CreatedAtUtc, opt => opt.Ignore())
            .ForMember(dest => dest.CreatedBy, opt => opt.Ignore())
            .ForMember(dest => dest.LastModifiedAtUtc, opt => opt.Ignore())
            .ForMember(dest => dest.LastModifiedBy, opt => opt.Ignore())
            .ForMember(dest => dest.Branches, opt => opt.Ignore());



        /////////////////////////////////
        // Branch Mappings

        CreateMap<Branch, BranchDto>()
            .ForMember(
                dest => dest.CompanyName,
                opt => opt.MapFrom(src => src.Company.Name));

        CreateMap<CreateBranchDto, Branch>()
            .ForMember(dest => dest.Id, opt => opt.Ignore())
            .ForMember(dest => dest.CreatedAtUtc, opt => opt.Ignore())
            .ForMember(dest => dest.CreatedBy, opt => opt.Ignore())
            .ForMember(dest => dest.LastModifiedAtUtc, opt => opt.Ignore())
            .ForMember(dest => dest.LastModifiedBy, opt => opt.Ignore())
            .ForMember(dest => dest.Company, opt => opt.Ignore())
            .ForMember(dest => dest.Warehouses, opt => opt.Ignore());

        CreateMap<UpdateBranchDto, Branch>()
            .ForMember(dest => dest.Id, opt => opt.Ignore())
            .ForMember(dest => dest.CreatedAtUtc, opt => opt.Ignore())
            .ForMember(dest => dest.CreatedBy, opt => opt.Ignore())
            .ForMember(dest => dest.LastModifiedAtUtc, opt => opt.Ignore())
            .ForMember(dest => dest.LastModifiedBy, opt => opt.Ignore())
            .ForMember(dest => dest.Company, opt => opt.Ignore())
            .ForMember(dest => dest.Warehouses, opt => opt.Ignore());





        //////////////////////////

        // Warehouse Mapping

        CreateMap<Warehouse, WarehouseDto>()
            .ForMember(
                dest => dest.BranchName,
                opt => opt.MapFrom(src => src.Branch.Name));

        CreateMap<CreateWarehouseDto, Warehouse>()
            .ForMember(dest => dest.Id, opt => opt.Ignore())
            .ForMember(dest => dest.IsActive, opt => opt.Ignore())
            .ForMember(dest => dest.CreatedAtUtc, opt => opt.Ignore())
            .ForMember(dest => dest.CreatedBy, opt => opt.Ignore())
            .ForMember(dest => dest.LastModifiedAtUtc, opt => opt.Ignore())
            .ForMember(dest => dest.LastModifiedBy, opt => opt.Ignore())
            .ForMember(dest => dest.Branch, opt => opt.Ignore())
            .ForMember(dest => dest.Locations, opt => opt.Ignore());

        CreateMap<UpdateWarehouseDto, Warehouse>()
            .ForMember(dest => dest.Id, opt => opt.Ignore())
            .ForMember(dest => dest.CreatedAtUtc, opt => opt.Ignore())
            .ForMember(dest => dest.CreatedBy, opt => opt.Ignore())
            .ForMember(dest => dest.LastModifiedAtUtc, opt => opt.Ignore())
            .ForMember(dest => dest.LastModifiedBy, opt => opt.Ignore())
            .ForMember(dest => dest.Branch, opt => opt.Ignore())
            .ForMember(dest => dest.Locations, opt => opt.Ignore());


        //////////////////////////////////
        ////Location Mapping 
        ///

        CreateMap<CreateLocationDto, Location>();
        CreateMap<UpdateLocationDto, Location>();
        CreateMap<Location, LocationDto>()
            .ForMember(dest => dest.WarehouseName,
                opt => opt.MapFrom(src => src.Warehouse.Name))
            .ForMember(dest => dest.WarehouseCode,
                opt => opt.MapFrom(src => src.Warehouse.Code));






        ////////////////////////////////////////////
        /////////Categories Mapping


           CreateMap<Category, CategoryDto>()
            .ForMember(
                dest => dest.ParentCategoryName,
                opt => opt.MapFrom(src =>
                    src.ParentCategory != null
                        ? src.ParentCategory.Name  
                        : null));

        CreateMap<CreateCategoryDto, Category>();

        CreateMap<UpdateCategoryDto, Category>();


















        // Product Mappings
        CreateMap<Product, ProductDto>()
            .ForMember(
                dest => dest.CategoryName,
                opt => opt.MapFrom(src => src.Category.Name))
           .ForMember(dest => dest.UnitName,
                opt => opt.MapFrom(src => src.BaseUnit != null
                    ? src.BaseUnit.Name
                    : null))
            .ForMember(dest => dest.UnitSymbol,
                opt => opt.MapFrom(src => src.BaseUnit != null
                    ? src.BaseUnit.Symbol
                    : null));

        CreateMap<CreateProductDto, Product>()
            .ForMember(
                dest => dest.ImagePath,
                opt => opt.Ignore());

        CreateMap<UpdateProductDto, Product>()
            .ForMember(
                dest => dest.ImagePath,
                opt => opt.Ignore())
            .ForMember(
                dest => dest.Category,
                opt => opt.Ignore())
            .ForMember(
                dest => dest.BaseUnit,
                opt => opt.Ignore())
            .ForMember(
                dest => dest.Lots,
                opt => opt.Ignore())
            .ForMember(
                dest => dest.SerialNumbers,
                opt => opt.Ignore())
            .ForMember(
                dest => dest.UnitConversions,
                opt => opt.Ignore());


    }
}