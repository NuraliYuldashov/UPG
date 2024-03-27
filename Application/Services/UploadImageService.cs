using Application.Interfaces;
using Microsoft.AspNetCore.Http;

namespace Application.Services;

public class UploadImageService : IUploadImageService
{
    public async Task<string> UploadAsync(IFormFile file, 
                                          string folderName,
                                          string domain)
    {
        if (file == null) return null!;
        var fileName = $"{Guid.NewGuid()}{Path.GetExtension(file.FileName)}";
        var path = Path.Combine(folderName, fileName);
        using (var stream = new FileStream(path, FileMode.Create))
        {
            await file.CopyToAsync(stream);
        }
        return $"{domain}images/{fileName}";
    }

    //public Task DeleteAsync(RemoveImageDto dto, string folderName)
    //{
    //    string[] splitters = { "/", "%2F" };
    //    var fileName = dto.url.Split(splitters, StringSplitOptions.RemoveEmptyEntries).Last();
    //    var path = Path.Combine(folderName, fileName);
    //    if (File.Exists(path))
    //    {
    //        File.Delete(path);
    //    }
    //    return Task.CompletedTask;
    //}

    public Task<bool> DeleteAsync(string url, string folderName)
    {
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
}
