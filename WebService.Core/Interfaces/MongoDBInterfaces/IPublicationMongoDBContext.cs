using MongoDB.Bson;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
using WebService.Core.Entities;

namespace WebService.Core.Interfaces.MongoDBInterfaces
{
    public interface IPublicationMongoDBContext
    {
        Task<PublicationDTO> CreatePublication(PublicationDTO publication);
        Task<List<PublicationDTO>> GetPublicationsByFilters(JToken filters);
        Task<PublicationDTO> UpdatePublication(PublicationDTO publication);
        Task DeletePublicationById(string id);
    }
}
