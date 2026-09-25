using Deskflow.API.Models;
using Deskflow.API.Repositories.Interfaces;
using Deskflow.API.Services.Interfaces;

namespace Deskflow.API.Services

{
    public class CategoryService : ICategoryService
    {
        private readonly ICategoryRepository _categoryRepository;

        public CategoryService(ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }

        public async Task CreateAsync(Category category)
        {
            if (category is null)
            {
                throw new ArgumentException("A categoria não pode ser nula.");
            }

            ValidateName(category.Name);
            category.Name = category.Name.Trim();

            await _categoryRepository.CreateAsync(category);
        }

        public async Task DeleteAsync(Guid id)
        {
            if (id == Guid.Empty)
            {
                throw new ArgumentException("O ID da categoria não pode ser vazio.");
            }
            
            var category = await _categoryRepository.GetByIdAsync(id);
            
            if (category == null)
            {
                throw new KeyNotFoundException($"Categoria com ID {id} não encontrada.");
            }
            
            await _categoryRepository.DeleteAsync(category);
        }
        public async Task UpdateAsync(Guid id, Category category)
        {
            if (id == Guid.Empty)
            {
                throw new ArgumentException("O ID da categoria não pode ser vazio.");
            }

            if (category is null)
            {
                throw new ArgumentException("A categoria não pode ser nula.");
            }

            var existingCategory = await _categoryRepository.GetByIdAsync(id);

            if (existingCategory is null)
            {
                throw new KeyNotFoundException($"Categoria com ID {id} não encontrada.");
            }

            ValidateName(category.Name);

            existingCategory.Name = category.Name.Trim();

            await _categoryRepository.UpdateAsync(existingCategory);
        }

        public async Task<List<Category>> GetAllAsync()
        {
            return await _categoryRepository.GetAllAsync();
        }

        public async Task<Category> GetByIdAsync(Guid id)
        {
            var category = await _categoryRepository.GetByIdAsync(id);
            if (category is null)
            {
                throw new KeyNotFoundException($"Categoria com ID {id} não encontrada.");
            }
            return category;
        }

        private void ValidateName(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                throw new ArgumentException("O nome da categoria não pode ser nulo ou vazio.");
            }
            if(name.Trim().Length > 50)
            {
                throw new ArgumentException("O nome da categoria não pode ter mais de 50 caracteres.");
            }
        }

    }
}