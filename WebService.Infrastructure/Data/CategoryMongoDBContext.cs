using Microsoft.Extensions.Options;
using MongoDB.Bson;
using MongoDB.Driver;
using System.Collections.Generic;
using System.Threading.Tasks;
using WebService.Core.Entities;
using WebService.Core.Interfaces.MongoDBInterfaces;

namespace WebService.Infrastructure.Data
{
    public class CategoryMongoDBContext : ICategoryMongoDBContext
    {
        private IMongoCollection<CategoryDTO> _mongoDatabase;
        public CategoryMongoDBContext(IMongoDatabase mongoDatabase, IOptions<Collections> settings)
        {
            _mongoDatabase = mongoDatabase.GetCollection<CategoryDTO>(settings.Value.CategoryCollectionName);
        }

        public async Task<CategoryDTO> CreateCategory(CategoryDTO Category)
        {
            await _mongoDatabase.InsertOneAsync(Category);
            return await _mongoDatabase.FindAsync("{}", new FindOptions<CategoryDTO>() { Sort = Builders<CategoryDTO>.Sort.Descending("_id") }).Result.FirstOrDefaultAsync();
        }

        public async Task<List<CategoryDTO>> GetAllCategories()
        {
            return await _mongoDatabase.FindAsync("{}", new FindOptions<CategoryDTO>() { Sort = Builders<CategoryDTO>.Sort.Descending("_id") }).Result.ToListAsync();
        }

        public async Task<CategoryDTO> GetCategoryById(string id)
        {
            return await _mongoDatabase.FindAsync(c => c.Id.Equals(ObjectId.Parse(id)),
                new FindOptions<CategoryDTO>() { Sort = Builders<CategoryDTO>.Sort.Descending("_id") }).Result.FirstOrDefaultAsync();
        }

        public async Task<CategoryDTO> UpdateCategoryById(CategoryDTO category)
        {
            await _mongoDatabase.ReplaceOneAsync(c => c.Id.Equals(category.Id), category);
            return category;
        }

        public async Task DeleteCategoryById(string id)
        {
            await _mongoDatabase.DeleteOneAsync(c => c.Id.Equals(ObjectId.Parse(id)));
        }
    }
}
