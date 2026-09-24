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
    }
}