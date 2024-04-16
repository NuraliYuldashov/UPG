using Domain.Entities;
using Infastructure.Interface;
using UPG.Core.Filters;

namespace Infastructure.Interfaces;

public interface IAccessoriesInterface : IRepository<Accessories>
{
    Task<List<Accessories>> GetFilteredAccessoriesByCategoryIdAsync(int categoryId, AccessoriesFilter filter);
    Task<List<Accessories>> GetFilteredAccessoriesByCategoryNameAsync(string categoryName, AccessoriesFilter filter);   
    Task<List<Accessories>> GetAccessoriesByCategoryAsync(int categoryId);
    Task<List<Accessories>> GetAccessoriesByCategoryNameAsync(string categoryName);
}
