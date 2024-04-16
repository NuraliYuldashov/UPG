using Application.Interfaces;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;

namespace Application.Services;

public class UploadImageService(IWebHostEnvironment hostEnvironment,
                                IConfiguration configuration) : IUploadImageService
{
    private readonly IWebHostEnvironment _hostEnvironment = hostEnvironment;
    private readonly IConfiguration _configuration = configuration;
    public async Task<string> UploadAsync(IFormFile file)
    {
        var folderName = Path.Combine(_hostEnvironment.WebRootPath, "images");
        var domain = _configuration["Domain"]!;
        if (file == null) return null!;
        var fileName = $"{Guid.NewGuid()}{Path.GetExtension(file.FileName)}";
        var path = Path.Combine(folderName, fileName);
        using (var stream = new FileStream(path, FileMode.Create))
        {
            await file.CopyToAsync(stream);
        }
        return $"{domain}images/{fileName}";
    }

    public Task<bool> DeleteAsync(string url)
    {
        var folderName = Path.Combine(_hostEnvironment.WebRootPath, "images");
        string[] splitters = { "/", "%2F" };
        var fileName = url.Split(splitters, StringSplitOptions.RemoveEmptyEntries).Last();
        var path = Path.Combine(folderName, fileName);
        if (File.Exists(path))
        {
            File.Delete(path);
        }
        else
        {
            return Task.FromResult(false);
        }
        return Task.FromResult(true);
    }

    public async Task DeleteAsync(List<string> urls)
    {
        foreach (var url in urls)
        {
            await DeleteAsync(url);
        }
    }

    public async Task<IEnumerable<string>> UploadAsync(List<IFormFile> files)
    {
        //string folder = Path.Combine(_hostEnvironment.WebRootPath, "images");
        //string domain = _configuration["Domain"] ?? "";
        List<string> result = new();
        foreach (var file in files)
        {
            result.Add(await UploadAsync(file));
        }
        return result;
    }
}
