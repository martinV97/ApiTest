using System.Collections.Generic;
using System.Threading.Tasks;
using WebService.Core.Entities;

namespace WebService.Core.Interfaces
{
    public interface IOfferRepository
    {
        Task<OfferDTO> CreateOffer(OfferDTO offer);
        Task<List<OfferDTO>> GetAllOffersByPublicationId(string id);
        Task<List<OfferDTO>> GetAllOffersByUserId(string id);
        Task<OfferDTO> GetOfferById(string id);
        Task<OfferDTO> UpdateOfferById(OfferDTO offer);
        Task<OfferDTO> DeleteOfferById(string id);
    }
}
