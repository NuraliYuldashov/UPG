using Microsoft.AspNetCore.Http;

namespace Application.Interfaces;
public interface IUploadImageService
{
    Task<string> UploadAsync(IFormFile file, string folderName, string domain);
    //Task DeleteAsync(RemoveImageDto dto, string folderName);
    Task<bool> DeleteAsync(string url, string folderName);
}