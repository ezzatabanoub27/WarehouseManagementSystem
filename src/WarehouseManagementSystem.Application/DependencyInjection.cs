using Microsoft.Extensions.DependencyInjection;
using WarehouseManagementSystem.Application.Mapping;
using WarehouseManagementSystem.Application.Services.Branches;
using WarehouseManagementSystem.Application.Services.Categories;
using WarehouseManagementSystem.Application.Services.Companies;
using WarehouseManagementSystem.Application.Services.Locations;
using WarehouseManagementSystem.Application.Services.Products;
using WarehouseManagementSystem.Application.Services.Warehouses;

namespace WarehouseManagementSystem.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddAutoMapper(cfg =>
        {
            cfg.AddProfile<MappingProfile>();
        });
        services.AddScoped<CompanyAppService>();
        services.AddScoped<BranchAppService>();
        services.AddScoped<WarehouseAppService>();
        services.AddScoped<LocationAppService>();
        services.AddScoped<CategoryAppService>();
        services.AddScoped<ProductAppService>();
        return services;
    }
}