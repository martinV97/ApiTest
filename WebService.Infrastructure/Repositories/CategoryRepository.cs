using System.Collections.Generic;
using System.Threading.Tasks;
using WebService.Core.Entities;
using WebService.Core.Interfaces;
using WebService.Core.Interfaces.MongoDBInterfaces;

namespace WebService.Infrastructure.Repositories
{

    public class CategoryRepository : ICategoryRepository
    {
        private readonly ICategoryMongoDBContext _categoryMongoDbContext;
        public CategoryRepository(ICategoryMongoDBContext paymenMongoDbContext)
        {
            _categoryMongoDbContext = paymenMongoDbContext;
        }

        public async Task<CategoryDTO> CreateCategory(CategoryDTO Category)
        {
            return await _categoryMongoDbContext.CreateCategory(Category);
        }

        public async Task<List<CategoryDTO>> GetAllCategories()
        {
            return await _categoryMongoDbContext.GetAllCategories();
        }

        public async Task<CategoryDTO> GetCategoryById(string id)
        {
            return await _categoryMongoDbContext.GetCategoryById(id);
        }

        public async Task<CategoryDTO> UpdateCategoryById(CategoryDTO category)
        {
            return await _categoryMongoDbContext.UpdateCategoryById(category);
        }

        public async Task<CategoryDTO> DeleteCategoryById(string id)
        {
            var categoryDeleted = await GetCategoryById(id);
            await _categoryMongoDbContext.DeleteCategoryById(id);
            return categoryDeleted;
        }
    }
}
