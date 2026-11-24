
using Microsoft.AspNetCore.Http;

namespace WarehouseManagementSystem.Application.Abstractions.Services;

public interface IFileStorageService
{
    Task<string> SaveProductImageAsync(
        IFormFile image,
        CancellationToken cancellationToken = default);

    Task DeleteFileAsync(
        string? filePath,
        CancellationToken cancellationToken = default);
}
