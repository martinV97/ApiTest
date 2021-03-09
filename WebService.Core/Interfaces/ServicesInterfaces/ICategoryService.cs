using System.Collections.Generic;
using System.Threading.Tasks;
using WebService.Core.Entities;

namespace WebService.Core.Interfaces
{
    public interface ICategoryService
    {
        Task<CategoryDTO> CreateCategory(Category category);
        Task<List<CategoryDTO>> GetAllCategories();
        Task<CategoryDTO> GetCategoryById(string id);
        Task<CategoryDTO> UpdateCategoryById(CategoryDTO Category);
        Task<CategoryDTO> DeleteCategoryById(string id);
    }
}
