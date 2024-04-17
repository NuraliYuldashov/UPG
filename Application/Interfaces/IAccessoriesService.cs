using Application.Helpers;
using DTOS.AccessoriesDtos;
using UPG.Core.Filters;

namespace Application.Interfaces
{ 
    public interface IAccessoriesService
    {
        Task<IEnumerable<AccessoriesDto>> GetAccessoriesAsync();
        Task<IEnumerable<AccessoriesDto>> GetAllAccessoriesByCategoryIdAsync(int categoryId);
        Task<IEnumerable<AccessoriesDto>> GetAllAccessoriesByCategoryNameAsync(string categoryName);
        Task<AccessoriesDto> GetAccessoriesByIdAsync(int id);
        Task AddAccessoriesAsync(AddAccessoriesDto addAccessoriesDto);
        Task UpdateAccessoriesAsync(UpdateAccessoriesDto updateAccessoriesDto);
        Task<List<AccessoriesDto>> FilterByCategoryIdAsync(int id, AccessoriesFilter accessoriesFilter);
        Task<List<AccessoriesDto>> FilterByCategoryNameAsync(string categoryName, AccessoriesFilter accessoriesFilter);
        Task DeleteAccessoriesAsync(int id);
    }
}
