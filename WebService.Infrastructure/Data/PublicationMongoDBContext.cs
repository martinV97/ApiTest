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
    public class PublicationMongoDBContext : IPublicationMongoDBContext
    {
        private IMongoCollection<PublicationDTO> _mongoDatabase;
        public PublicationMongoDBContext(IMongoDatabase mongoDatabase, IOptions<Collections> settings)
        {
            _mongoDatabase = mongoDatabase.GetCollection<PublicationDTO>(settings.Value.PublicationsCollectionName);
        }

        public async Task<PublicationDTO> CreatePublication(PublicationDTO publication)
        {
            try
            {
                await _mongoDatabase.InsertOneAsync(publication);
                return await _mongoDatabase.FindAsync("{}", new FindOptions<PublicationDTO>() { Sort = Builders<PublicationDTO>.Sort.Descending("_id") }).Result.FirstOrDefaultAsync();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<List<PublicationDTO>> GetPublicationsByFilters(JToken filters)
        {
            try
            {
                List<PublicationDTO> result;
                var sort = Builders<PublicationDTO>.Sort.Descending("_id");
                var options = new FindOptions<PublicationDTO>()
                {
                    Sort = sort,
                };
                result = await _mongoDatabase.FindAsync(AddNoSQLFilters(filters), options).Result.ToListAsync();
                return result;
            }
            catch (Exception)
            {
                throw;
            }
        }

        private FilterDefinition<PublicationDTO> AddNoSQLFilters(JToken filters)
        {
            FilterDefinition<PublicationDTO> filterDefinition = Builders<PublicationDTO>.Filter.And
                (LoopFilters(filters));
            return filterDefinition;
        }

        private FilterDefinition<PublicationDTO>[] LoopFilters(JToken filters)
        {
            List<FilterDefinition<PublicationDTO>> filterList = new List<FilterDefinition<PublicationDTO>>();
            foreach (JProperty filter in filters)
            {
                if (!(filter.Name is null)) filterList.Add(AddNoSQLFilter(filter));
            }
            return filterList.ToArray();
        }

        private FilterDefinition<PublicationDTO> AddNoSQLFilter(JProperty filter)
        {
            switch (filter.Name)
            {
                case "All":
                    return Builders<PublicationDTO>.Filter.Where(p => true);
                case "Id":
                    return Builders<PublicationDTO>.Filter.Where(p => p.Id.Equals(ObjectId.Parse(filter.Value.ToString())));
                case "ProductId":
                    return Builders<PublicationDTO>.Filter.Where(p => p.ProductId.Equals(ObjectId.Parse(filter.Value.ToString())));
                case "UserId":
                    return Builders<PublicationDTO>.Filter.Where(p => p.UserId.Equals(ObjectId.Parse(filter.Value.ToString())));
                case "Date":
                    return Builders<PublicationDTO>.Filter.Where(p => p.StartDate >= Convert.ToDateTime(filter.Value.ToString()));
                default:
                    return null;
            }
        }

        public async Task<PublicationDTO> UpdatePublication(PublicationDTO publication)
        {
            try
            {
                await _mongoDatabase.ReplaceOneAsync(AddNoSQLFilters(new JProperty("Id", publication.Id)), publication);
                return publication;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task DeletePublicationById(string id)
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
