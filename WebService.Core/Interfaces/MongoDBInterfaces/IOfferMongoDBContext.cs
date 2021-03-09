using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
using WebService.Core.Entities;

namespace WebService.Core.Interfaces.MongoDBInterfaces
{
    public interface IOfferMongoDBContext
    {
        Task<OfferDTO> CreateOffer(OfferDTO offer);
        Task<List<OfferDTO>> GetOffersByFilters(JToken filters);
        Task<OfferDTO> UpdateOffer(OfferDTO offer);
        Task DeleteOfferById(string id);
    }
}