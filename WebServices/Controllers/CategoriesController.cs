using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using WebService.Core.Entities;
using WebService.Core.Interfaces;
namespace WebServices.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        private readonly ICategoryService _categoryService;

        public CategoriesController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        /// <summary>
        /// Create a category
        /// </summary>
        /// <param name="category">Category object</param>
        /// <returns>Category object with id</returns>
        [HttpPost]
        [Route("createCategory")]
        public async Task<IActionResult> CreateCategory(Category category)
        {
            var result = await _categoryService.CreateCategory(category);
            return Ok(result);
        }

        /// <summary>
        /// Get all categories
        /// </summary>
        /// <returns>List of categories</returns>
        [HttpGet]
        [Route("getCategories")]
        public async Task<IActionResult> GetCategories()
        {
            var result = await _categoryService.GetAllCategories();
            return Ok(result);
        }

        /// <summary>
        /// Get a category by id
        /// </summary>
        /// <param name="id">Category id</param>
        /// <returns>Category object</returns>
        [HttpGet]
        [Route("getCategoryById")]
        public async Task<IActionResult> GetCategoryById(string id)
        {
            var result = await _categoryService.GetCategoryById(id);
            return Ok(result);
        }

        /// <summary>
        /// Update a category
        /// </summary>
        /// <param name="category">Category object</param>
        /// <returns>Update Category object</returns>
        [HttpPut]
        [Route("updateCategory")]
        public async Task<IActionResult> UpdateCategory(CategoryDTO category)
        {
            var result = await _categoryService.UpdateCategoryById(category);
            return Ok(result);
        }

        /// <summary>
        /// Delete a category
        /// </summary>
        /// <param name="id">Category id</param>
        /// <returns>Deleted Categry Object</returns>
        [HttpDelete]
        [Route("deleteCategoryById")]
        public async Task<IActionResult> DeleteCategoryById(string id)
        {
            var result = await _categoryService.DeleteCategoryById(id);
            return Ok(result);
        }
    }
}
