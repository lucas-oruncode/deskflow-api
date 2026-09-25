using Deskflow.API.DTOs.Categories;
using Deskflow.API.Models;
using Deskflow.API.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Deskflow.API.Controllers

{
    [ApiController]
    [Route("api/category")]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryService _categoryService;

        public CategoryController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        [HttpPost]
        public async Task<IActionResult> CreateCategory([FromBody] CategoryDto categoryDto)
        {
            var category = new Category { Name = categoryDto.Name };
            await _categoryService.CreateAsync(category);
            return Created($"/api/category/{category.Id}", new CategoryResponseDto { Id = category.Id, Name = category.Name });
        }

        [HttpGet]
        public async Task<IActionResult> GetAllCategories()
        {
            var categories = await _categoryService.GetAllAsync();
            var categoryDtos = categories.Select(c => new CategoryResponseDto { Id = c.Id, Name = c.Name }).ToList();
            return Ok(categoryDtos);
        }

        [HttpGet("{id:guid}")]
        public async Task<IActionResult> GetCategory(Guid id)
        {
            try
            {
                var category = await _categoryService.GetByIdAsync(id);
                return Ok(new CategoryResponseDto { Id = category.Id, Name = category.Name });
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpPut("{id:guid}")]
        public async Task<IActionResult> UpdateCategory(Guid id, [FromBody] CategoryDto categoryDto)
        {
            var category = new Category { Name = categoryDto.Name };
            try
            {
                await _categoryService.UpdateAsync(id, category);
                return NoContent();
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> DeleteCategory(Guid id)
        {
            try
            {
                await _categoryService.DeleteAsync(id);
                return NoContent();
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}