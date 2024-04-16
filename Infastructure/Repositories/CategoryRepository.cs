using Domain.Entities;
using Infastructure.Data;
using Infastructure.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Infastructure.Repositories
{
    public class CategoryRepository(AppDBContext dbContext) : ICategory
    {
        private readonly AppDBContext _dbContext = dbContext;

        public void Add(Category category)
        {
            _dbContext.Categories.Add(category);
        }

        public void Delete(int id)
        {
            var category = _dbContext.Categories.AsNoTracking().FirstOrDefault(c => c.Id == id);
            _dbContext.Categories.Remove(category!);
        }

        public async Task<IEnumerable<Category>> GetAllAsync()
        {
            return await _dbContext.Categories.AsNoTracking().ToListAsync();
        }

        public async Task<Category?> GetByIdAsync(int id)
        {
            return await _dbContext.Categories.AsNoTracking().FirstOrDefaultAsync(c => c.Id == id);
        }

        public void Update(Category category)
        {
            _dbContext.Categories.Update(category);
        }
    }
}
