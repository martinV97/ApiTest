using System.Collections.Generic;
using System.Threading.Tasks;
using WebService.Core.Entities;

namespace WebService.Core.Interfaces.MongoDBInterfaces
{
    public interface ICategoryMongoDBContext
    {
        Task<CategoryDTO> CreateCategory(CategoryDTO category);
        Task<List<CategoryDTO>> GetAllCategories();
        Task<CategoryDTO> GetCategoryById(string id);
        Task<CategoryDTO> UpdateCategoryById(CategoryDTO category);
        Task DeleteCategoryById(string id);
    }
}
