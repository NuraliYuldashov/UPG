using Domain.Entities;

namespace Infastructure.Interfaces
{
    public  interface ICategory
    {
        Task<IEnumerable<Category>> GetAllAsync();
        Task<Category?> GetByIdAsync(int id);
        void Add(Category category);
        void Update(Category category);
        void Delete(int id);
    }
}
