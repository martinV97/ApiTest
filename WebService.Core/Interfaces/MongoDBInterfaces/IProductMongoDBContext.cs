using System.Collections.Generic;
using System.Threading.Tasks;
using WebService.Core.Entities;

namespace WebService.Core.Interfaces.MongoDBInterfaces
{
    public interface IProductMongoDBContext
    {
        Task<ProductDTO> CreateProduct(ProductDTO product);
        Task<List<ProductDTO>> GetAllProductsByCategoryId(string id);
        Task<ProductDTO> GetProductById(string id);
        Task<ProductDTO> UpdateProductById(ProductDTO product);
        Task DeleteProductById(string id);
    }
}
