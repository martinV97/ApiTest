using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebService.Core.Entities;
using WebService.Core.Interfaces;
using WebService.Core.Interfaces.MongoDBInterfaces;

namespace WebService.Infrastructure.Repositories
{
    public class PublicationRepository : IPublicationRepository
    {
        private readonly IPublicationMongoDBContext _publicationMongoDBContext;

        public PublicationRepository(IPublicationMongoDBContext publicationMongoDBContext)
        {
            _publicationMongoDBContext = publicationMongoDBContext;
        }

        public async Task<PublicationDTO> CreatePublication(PublicationDTO publication)
        {
            return await _publicationMongoDBContext.CreatePublication(publication);
        }

        public async Task<List<PublicationDTO>> GetAllPublicationsByProductId(string id)
        {
            return await _publicationMongoDBContext.GetPublicationsByFilters(new JObject { new JProperty("ProductId", id) });
        }

        public async Task<List<PublicationDTO>> GetAllPublicationsByUserId(string id)
        {
            return await _publicationMongoDBContext.GetPublicationsByFilters(new JObject { new JProperty("UserId", id) });
        }

        public async Task<PublicationDTO> GetPublicationById(string id)
        {
            var publications = await _publicationMongoDBContext.GetPublicationsByFilters(new JObject { new JProperty("Id", id) });
            return publications.FirstOrDefault();
        }

        public async Task<List<PublicationDTO>> GetPublicationsByDate(DateTime startDate)
        {
            return await _publicationMongoDBContext.GetPublicationsByFilters(new JObject { new JProperty("Date", startDate) });
        }

        public async Task<PublicationDTO> UpdatePublicationById(PublicationDTO publication)
        {
            return await _publicationMongoDBContext.UpdatePublication(publication);
        }

        public async Task<PublicationDTO> DeletePublicationById(string id)
        {
            var publicationDeleted = await GetPublicationById(id);
            await _publicationMongoDBContext.DeletePublicationById(id);
            return publicationDeleted;
        }
    }
}
