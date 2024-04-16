using Microsoft.AspNetCore.Http;

namespace Application.Interfaces;
public interface IUploadImageService
{
    Task<string> UploadAsync(IFormFile file);
    Task<bool> DeleteAsync(string url);
    Task<IEnumerable<string>> UploadAsync(List<IFormFile> files);
    Task DeleteAsync(List<string> urls);
}