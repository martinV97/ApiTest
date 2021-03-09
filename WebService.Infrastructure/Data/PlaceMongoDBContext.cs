using Microsoft.Extensions.Options;
using MongoDB.Bson;
using MongoDB.Driver;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using WebService.Core.DTOs;
using WebService.Core.Entities;
using WebService.Core.Enumerations;
using WebService.Core.Interfaces.MongoDBInterfaces;

namespace WebService.Infrastructure.Data
{
    public class PlaceMongoDBContext : IPlaceMongoDBContext
    {
        private IMongoCollection<PlaceDTO> _mongoDatabase;
        public PlaceMongoDBContext(IMongoDatabase mongoDatabase, IOptions<Collections> settings)
        {
            _mongoDatabase = mongoDatabase.GetCollection<PlaceDTO>(settings.Value.PlacesCollectionName);
        }

        public async Task<PlaceDTO> CreatePlace(PlaceDTO place)
        {
            try
            {
                await _mongoDatabase.InsertOneAsync(place);
                return await _mongoDatabase.FindAsync("{}", new FindOptions<PlaceDTO>() { Sort = Builders<PlaceDTO>.Sort.Descending("_id") }).Result.FirstOrDefaultAsync();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<List<PlaceDTO>> GetPlacesByFilters(JToken filters)
        {
            List<PlaceDTO> result;
            var sort = Builders<PlaceDTO>.Sort.Descending("_id");
            var options = new FindOptions<PlaceDTO>()
            {
                Sort = sort,
            };
            result = await _mongoDatabase.FindAsync(AddNoSQLFilters(filters), options).Result.ToListAsync();
            return result;
        }

        private FilterDefinition<PlaceDTO> AddNoSQLFilters(JToken filters)
        {
            FilterDefinition<PlaceDTO> filterDefinition = Builders<PlaceDTO>.Filter.And
                (LoopFilters(filters));
            return filterDefinition;
        }

        private FilterDefinition<PlaceDTO>[] LoopFilters(JToken filters)
        {
            List<FilterDefinition<PlaceDTO>> filterList = new List<FilterDefinition<PlaceDTO>>();
            foreach (JProperty filter in filters)
            {
                if (!(filter.Name is null)) filterList.Add(AddNoSQLFilter(filter));
            }
            return filterList.ToArray();
        }

        private FilterDefinition<PlaceDTO> AddNoSQLFilter(JProperty filter)
        {
            switch (filter.Name)
            {
                case "All":
                    return Builders<PlaceDTO>.Filter.Where(u => true);
                case "Id":
                    return Builders<PlaceDTO>.Filter.Where(u => u.Id.Equals(ObjectId.Parse(filter.Value.ToString())));
                case "Type":
                    return Builders<PlaceDTO>.Filter.Where(u => u.Type == (PlaceTypeEnum) Int32.Parse(filter.Value.ToString()));
                default:
                    return null;
            }
        }

        public async Task<PlaceDTO> UpdatePlace(PlaceDTO place)
        {
            try
            {
                await _mongoDatabase.ReplaceOneAsync(AddNoSQLFilters(new JProperty("Id", place.Id)), place);
                return place;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task DeletePlaceById(string id)
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
