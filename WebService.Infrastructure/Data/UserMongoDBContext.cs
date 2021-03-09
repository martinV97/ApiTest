using Mapster;
using Microsoft.Extensions.Options;
using MongoDB.Bson;
using MongoDB.Driver;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using WebService.Core.Entities;
using WebService.Core.Interfaces.MongoDBInterfaces;

namespace WebService.Infrastructure.Data
{
    public class UserMongoDBContext : IUserMongoDBContext
    {
        private IMongoCollection<UserDTO> _mongoDatabase;
        public UserMongoDBContext(IMongoDatabase mongoDatabase, IOptions<Collections> settings)
        {
            _mongoDatabase = mongoDatabase.GetCollection<UserDTO>(settings.Value.UsersCollectionName);
        }

        public async Task<UserDTO> CreateUser(UserDTO user)
        {
            try
            {
                await _mongoDatabase.InsertOneAsync(user);
                return await _mongoDatabase.FindAsync("{}", new FindOptions<UserDTO>() { Sort = Builders<UserDTO>.Sort.Descending("_id") }).Result.FirstOrDefaultAsync();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<List<UserDTO>> GetUsersByFilters(JToken filters)
        {
            List<UserDTO> result;
            var sort = Builders<UserDTO>.Sort.Descending("_id");
            var options = new FindOptions<UserDTO>()
            {
                Sort = sort,
            };
            result = await _mongoDatabase.FindAsync(AddNoSQLFilters(filters), options).Result.ToListAsync();
            return result;
        }

        private FilterDefinition<UserDTO> AddNoSQLFilters(JToken filters)
        {
            FilterDefinition<UserDTO> filterDefinition = Builders<UserDTO>.Filter.And
                (LoopFilters(filters));
            return filterDefinition;
        }

        private FilterDefinition<UserDTO>[] LoopFilters(JToken filters)
        {
            List<FilterDefinition<UserDTO>> filterList = new List<FilterDefinition<UserDTO>>();
            foreach (JProperty filter in filters)
            {
                if (!(filter.Name is null)) filterList.Add(AddNoSQLFilter(filter));
            }
            return filterList.ToArray();
        }

        private FilterDefinition<UserDTO> AddNoSQLFilter(JProperty filter)
        {
            switch (filter.Name)
            {
                case "All":
                    return Builders<UserDTO>.Filter.Where(u => true);
                case "Id":
                    return Builders<UserDTO>.Filter.Where(u => u.Id.Equals(ObjectId.Parse(filter.Value.ToString())));
                case "PlaceId":
                    return Builders<UserDTO>.Filter.Where(u => u.PlaceId.Equals(ObjectId.Parse(filter.Value.ToString())));
                case "Identification":
                    return Builders<UserDTO>.Filter.Where(u => u.Identification.Equals(filter.Value.ToString()));
                case "Email":
                    return Builders<UserDTO>.Filter.Where(u => u.Email.Equals(filter.Value.ToString()));
                default:
                    return null;
            }
        }

        public async Task<UserDTO> UpdateUser(UserDTO user)
        {
            try
            {
                await _mongoDatabase.ReplaceOneAsync(AddNoSQLFilters(new JProperty("Id", user.Id)), user);
                return user;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task DeleteUserById(string id)
        {
            try
            {
                await _mongoDatabase.DeleteOneAsync(AddNoSQLFilters(new JObject { new JProperty("Id", id) }));
            }
            catch (Exception)
            {
                throw;
            }
        }

    }
}
