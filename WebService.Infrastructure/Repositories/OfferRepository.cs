using MongoDB.Bson;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebService.Core.Entities;
using WebService.Core.Interfaces;
using WebService.Core.Interfaces.MongoDBInterfaces;

namespace WebService.Infrastructure.Repositories
{
    public class OfferRepository : IOfferRepository
    {
        private readonly IOfferMongoDBContext _offerMongoDBContext;

        public OfferRepository(IOfferMongoDBContext offerMongoDBContext)
        {
            this._offerMongoDBContext = offerMongoDBContext;
        }

        public async Task<OfferDTO> CreateOffer(OfferDTO offer)
        {
            return await _offerMongoDBContext.CreateOffer(offer);
        }

        public async Task<List<OfferDTO>> GetAllOffersByPublicationId(string id)
        {
            return await _offerMongoDBContext.GetOffersByFilters(new JObject { new JProperty("PublicationId", id) });
        }

        public async Task<List<OfferDTO>> GetAllOffersByUserId(string id)
        {
            return await _offerMongoDBContext.GetOffersByFilters(new JObject { new JProperty("UserId", id) });
        }

        public async Task<OfferDTO> GetOfferById(string id)
        {
            var offers = await _offerMongoDBContext.GetOffersByFilters(new JObject { new JProperty("Id", id) });
            return offers.FirstOrDefault();
        }

        public async Task<OfferDTO> UpdateOfferById(OfferDTO offer)
        {
            return await _offerMongoDBContext.UpdateOffer(offer);
        }

        public async Task<OfferDTO> DeleteOfferById(string id)
        {
            var offerDeleted = await GetOfferById(id);
            await _offerMongoDBContext.DeleteOfferById(id);
            return offerDeleted;
        }
    }
}
