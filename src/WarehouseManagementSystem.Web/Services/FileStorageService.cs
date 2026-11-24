
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using WarehouseManagementSystem.Application.Abstractions.Services;

namespace WarehouseManagementSystem.Web.Services;

public class FileStorageService : IFileStorageService
{
    private readonly IWebHostEnvironment _environment;

    private const long MaxFileSize = 5 * 1024 * 1024;

    private static readonly HashSet<string> AllowedExtensions =
        new(StringComparer.OrdinalIgnoreCase)
        {
            ".jpg",
            ".jpeg",
            ".png",
            ".webp"
        };

    public FileStorageService(IWebHostEnvironment environment)
    {
        _environment = environment;
    }

    public async Task<string> SaveProductImageAsync(
        IFormFile image,
        CancellationToken cancellationToken = default)
    {
        if (image == null || image.Length == 0)
            throw new ArgumentException("Please select an image.");

        if (image.Length > MaxFileSize)
            throw new ArgumentException("Image size cannot exceed 5 MB.");

        var extension = Path.GetExtension(image.FileName);

        if (!AllowedExtensions.Contains(extension))
            throw new ArgumentException(
                "Only JPG, JPEG, PNG and WEBP images are allowed.");

        var uploadsFolder = Path.Combine(
            _environment.WebRootPath,
            "uploads",
            "products");

        Directory.CreateDirectory(uploadsFolder);

        var fileName = $"{Guid.NewGuid():N}{extension.ToLowerInvariant()}";

        var filePath = Path.Combine(uploadsFolder, fileName);

        await using (var stream = new FileStream(
            filePath,
            FileMode.CreateNew,
            FileAccess.Write,
            FileShare.None,
            81920,
            FileOptions.Asynchronous))
        {
            await image.CopyToAsync(stream, cancellationToken);
        }

        return $"/uploads/products/{fileName}";
    }

    public Task DeleteFileAsync(
        string? filePath,
        CancellationToken cancellationToken = default)
    {
        if (string.IsNullOrWhiteSpace(filePath))
            return Task.CompletedTask;

        var fileName = Path.GetFileName(filePath);

        if (string.IsNullOrWhiteSpace(fileName))
            return Task.CompletedTask;

        var uploadsFolder = Path.Combine(
            _environment.WebRootPath,
            "uploads",
            "products");

        var fullPath = Path.Combine(uploadsFolder, fileName);

        if (File.Exists(fullPath))
            File.Delete(fullPath);

        return Task.CompletedTask;
    }
}
