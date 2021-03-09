using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using WebService.Core.Entities;

namespace WebService.Core.Interfaces
{
    public interface IPublicationService
    {
        Task<PublicationDTO> CreatePublication(Publication publication);
        Task<List<PublicationDTO>> GetAllPublicationsByUserId(string id);
        Task<List<PublicationDTO>> GetAllPublicationsByProductId(string id);
        Task<List<PublicationDTO>> GetPublicationsByDate(DateTime startDate);
        Task<PublicationDTO> GetPublicationById(string id);
        Task<PublicationDTO> UpdatePublicationById(PublicationDTO publication);
        Task<PublicationDTO> DeletePublicationById(string id);
    }
}
