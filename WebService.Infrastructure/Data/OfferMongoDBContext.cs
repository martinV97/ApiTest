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
    public class OfferMongoDBContext : IOfferMongoDBContext
    {
        private IMongoCollection<OfferDTO> _mongoDatabase;
        public OfferMongoDBContext(IMongoDatabase mongoDatabase, IOptions<Collections> settings)
        {
            _mongoDatabase = mongoDatabase.GetCollection<OfferDTO>(settings.Value.OfferCollectionName);
        }

        public async Task<OfferDTO> CreateOffer(OfferDTO offer)
        {
            try
            {
                await _mongoDatabase.InsertOneAsync(offer);
                return await _mongoDatabase.FindAsync("{}", new FindOptions<OfferDTO>() { Sort = Builders<OfferDTO>.Sort.Descending("_id") }).Result.FirstOrDefaultAsync();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<List<OfferDTO>> GetOffersByFilters(JToken filters)
        {
            List<OfferDTO> result;
            var sort = Builders<OfferDTO>.Sort.Descending("_id");
            var options = new FindOptions<OfferDTO>()
            {
                Sort = sort,
            };
            result = await _mongoDatabase.FindAsync(AddNoSQLFilters(filters), options).Result.ToListAsync();
            return result;
        }

        private FilterDefinition<OfferDTO> AddNoSQLFilters(JToken filters)
        {
            FilterDefinition<OfferDTO> filterDefinition = Builders<OfferDTO>.Filter.And
                (LoopFilters(filters));
            return filterDefinition;
        }

        private FilterDefinition<OfferDTO>[] LoopFilters(JToken filters)
        {
            List<FilterDefinition<OfferDTO>> filterList = new List<FilterDefinition<OfferDTO>>();
            foreach (JProperty filter in filters)
            {
                if (!(filter.Name is null)) filterList.Add(AddNoSQLFilter(filter));
            }
            return filterList.ToArray();
        }

        private FilterDefinition<OfferDTO> AddNoSQLFilter(JProperty filter)
        {
            switch (filter.Name)
            {
                case "All":
                    return Builders<OfferDTO>.Filter.Where(o => true);
                case "Id":
                    return Builders<OfferDTO>.Filter.Where(o => o.Id.Equals(ObjectId.Parse(filter.Value.ToString())));
                case "PublicationId":
                    return Builders<OfferDTO>.Filter.Where(o => o.PublicationId.Equals(ObjectId.Parse(filter.Value.ToString())));
                case "UserId":
                    return Builders<OfferDTO>.Filter.Where(o => o.UserId.Equals(ObjectId.Parse(filter.Value.ToString())));
                default:
                    return null;
            }
        }

        public async Task<OfferDTO> UpdateOffer(OfferDTO offer)
        {
            try
            {
                await _mongoDatabase.ReplaceOneAsync(AddNoSQLFilters(new JProperty("Id", offer.Id)), offer);
                return offer;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task DeleteOfferById(string id)
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
