using Domain.Entities;
using Infastructure.Data;
using Infastructure.Interfaces;
using Microsoft.EntityFrameworkCore;
using UPG.Core.Filters;

namespace Infastructure.Repositories;

public class AccessoriesRepository(AppDBContext dbContext) : Repository<Accessories>(dbContext), IAccessoriesInterface
{
    private readonly AppDBContext _dbContext = dbContext;

    public async Task<List<Accessories>> GetFilteredAccessoriesByCategoryIdAsync(int categoryId, AccessoriesFilter filter)
    {
        var query = _dbContext.Accessories
            .Include(a => a.Category) 
            .Where(a => a.CategoryId == categoryId) 
            .AsQueryable();

        if (!string.IsNullOrEmpty(filter.brand))
            query = query.Where(h => h.BrandName == filter.brand);

        if (filter.minPrice.HasValue)
            query = query.Where(h => h.Price >= filter.minPrice);

        if (filter.maxPrice.HasValue)
            query = query.Where(h => h.Price <= filter.maxPrice);


        query = query.Skip((filter.pageNumber - 1) * filter.pageSize)
                    .Take(filter.pageSize);

        return await query.ToListAsync();
    }

    public async Task<List<Accessories>> GetFilteredAccessoriesByCategoryNameAsync(string categoryName, AccessoriesFilter filter)
    {
        var query = _dbContext.Accessories
            .Include(a => a.Category) 
            .Where(a => a.Category.Name == categoryName) 
            .AsQueryable();

        if (!string.IsNullOrEmpty(filter.brand))
            query = query.Where(h => h.BrandName == filter.brand);

        if (filter.minPrice.HasValue)
            query = query.Where(h => h.Price >= filter.minPrice);

        if (filter.maxPrice.HasValue)
            query = query.Where(h => h.Price <= filter.maxPrice);

        query = query.Skip((filter.pageNumber - 1) * filter.pageSize)
                    .Take(filter.pageSize);

        return await query.ToListAsync();
    }

    public async Task<List<Accessories>> GetAccessoriesByCategoryAsync(int categoryId)
    {
        return await _dbContext.Accessories
            .Include(a => a.Category)
            .Where(a => a.CategoryId == categoryId)
            .ToListAsync();
    }

    public async Task<List<Accessories>> GetAccessoriesByCategoryNameAsync(string categoryName)
    {
        return await _dbContext.Accessories
            .Include(a => a.Category)
            .Where(a => a.Category.Name == categoryName)
            .ToListAsync();
    }

}
