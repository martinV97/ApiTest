using Microsoft.Extensions.Options;
using MongoDB.Bson;
using MongoDB.Driver;
using System.Collections.Generic;
using System.Threading.Tasks;
using WebService.Core.Entities;
using WebService.Core.Interfaces.MongoDBInterfaces;

namespace WebService.Infrastructure.Data
{
    public class ProductMongoDBContext : IProductMongoDBContext
    {
        private IMongoCollection<ProductDTO> _mongoDatabase;

        public ProductMongoDBContext(IMongoDatabase mongoDatabase, IOptions<Collections> settings)
        {
            _mongoDatabase = mongoDatabase.GetCollection<ProductDTO>(settings.Value.ProductsCollectionName);
        }

        public async Task<ProductDTO> CreateProduct(ProductDTO product)
        {
            await _mongoDatabase.InsertOneAsync(product);
            return await _mongoDatabase.FindAsync("{}", new FindOptions<ProductDTO>() { Sort = Builders<ProductDTO>.Sort.Descending("_id") }).Result.FirstOrDefaultAsync();
        }

        public async Task<List<ProductDTO>> GetAllProductsByCategoryId(string id)
        {
            return await _mongoDatabase.FindAsync(p => p.CategoryId.Equals(ObjectId.Parse(id)),
                new FindOptions<ProductDTO>() { Sort = Builders<ProductDTO>.Sort.Descending("_id") }).Result.ToListAsync();
        }

        public async Task<ProductDTO> GetProductById(string id)
        {
            return await _mongoDatabase.FindAsync(p => p.Id.Equals(ObjectId.Parse(id)),
                new FindOptions<ProductDTO>() { Sort = Builders<ProductDTO>.Sort.Descending("_id") }).Result.FirstOrDefaultAsync();
        }

        public async Task<ProductDTO> UpdateProductById(ProductDTO product)
        {
            await _mongoDatabase.ReplaceOneAsync(p => p.Id.Equals(product.Id), product);
            return product;
        }

        public async Task DeleteProductById(string id)
        {
            await _mongoDatabase.DeleteOneAsync(p => p.Id.Equals(ObjectId.Parse(id)));
        }
    }
}
