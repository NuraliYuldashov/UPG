using Domain.Entities;
using System.Threading.Tasks;

namespace Application.Interfaces;

public interface ICategoryInterface
{
    Task<IEnumerable<Category>> GetAllAsync();
    Task<Category?> GetByIdAsync(int id);
    void Add (Category category);
    void Delete (int id);
    void Update (Category category);
}
