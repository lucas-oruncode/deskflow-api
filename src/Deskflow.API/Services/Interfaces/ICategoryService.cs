using Deskflow.API.Models;

namespace Deskflow.API.Services.Interfaces

{
    public interface ICategoryService
    {
        Task<List<Category>> GetAllAsync();
        Task<Category> GetByIdAsync(Guid id);
        Task CreateAsync(Category category);
        Task UpdateAsync(Guid id, Category category);
        Task DeleteAsync(Guid id);
    }
}