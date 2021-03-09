using System.Collections.Generic;
using System.Threading.Tasks;
using WebService.Core.Entities;
using WebService.Core.Interfaces;
using WebService.Core.Interfaces.MongoDBInterfaces;

namespace WebService.Infrastructure.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly IProductMongoDBContext _productMongoDBContext;

        public ProductRepository(IProductMongoDBContext productMongoDBContext)
        {
            _productMongoDBContext = productMongoDBContext;
        }

        public async Task<ProductDTO> CreateProduct(ProductDTO product)
        {
            return await _productMongoDBContext.CreateProduct(product);
        }

        public async Task<List<ProductDTO>> GetAllProductsByCategoryId(string id)
        {
            return await _productMongoDBContext.GetAllProductsByCategoryId(id);
        }

        public async Task<ProductDTO> GetProductById(string id)
        {
            return await _productMongoDBContext.GetProductById(id);
        }

        public async Task<ProductDTO> UpdateProductById(ProductDTO product)
        {
            return await _productMongoDBContext.UpdateProductById(product);
        }

        public async Task<ProductDTO> DeleteProductById(string id)
        {
            var productDeleted = await GetProductById(id);
            await _productMongoDBContext.DeleteProductById(id);
            return productDeleted;
        }
    }
}
