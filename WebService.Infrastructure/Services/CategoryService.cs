using Mapster;
using System.Collections.Generic;
using System.Threading.Tasks;
using WebService.Core.Entities;
using WebService.Core.Interfaces;

namespace WebService.Infrastructure.Repositories
{

    public class CategoryService : ICategoryService
    {
        private readonly ICategoryRepository _categoryRepository;

        public CategoryService(ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }

        public async Task<CategoryDTO> CreateCategory(Category category)
        {
            return await _categoryRepository.CreateCategory(category.Adapt<CategoryDTO>());
        }

        public async Task<List<CategoryDTO>> GetAllCategories()
        {
            return await _categoryRepository.GetAllCategories();
        }

        public async Task<CategoryDTO> GetCategoryById(string id)
        {
            return await _categoryRepository.GetCategoryById(id);
        }

        public async Task<CategoryDTO> UpdateCategoryById(CategoryDTO category)
        {
            return await _categoryRepository.UpdateCategoryById(category);
        }

        public async Task<CategoryDTO> DeleteCategoryById(string id)
        {
            return await _categoryRepository.DeleteCategoryById(id);
        }
    }
}
