using Application.Interfaces;
using Domain.Entities;
using Infastructure.Interfaces;

namespace Application.Services;

public class CategoryService(ICategory category) : ICategoryInterface
{
    private readonly ICategory _category = category;

    public  void Add(Category category)
    {
        if(category == null) throw new ArgumentNullException("category was null");
        _category.Add(category);
    }
    public void Delete(int id)
    {
        if (id > 0)
        {
            _category.Delete(id);
        }
        else
        {
            throw new ArgumentNullException("category was null");
        }
    }
    public async Task<IEnumerable<Category>> GetAllAsync()
    => await _category.GetAllAsync();

    public async Task<Category?> GetByIdAsync(int id)
    {
        if (id <= 0) 
        {
            throw new ArgumentOutOfRangeException(nameof(id), "Id must be a positive integer.");
        }

        return await _category.GetByIdAsync(id);
    }


    public void Update(Category category)
    {
        _category.Update(category);
    }
}
