using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
using WebService.Core.DTOs;

namespace WebService.Core.Interfaces.MongoDBInterfaces
{
    public interface IPlaceMongoDBContext
    {
        Task<PlaceDTO> CreatePlace(PlaceDTO place);
        Task<List<PlaceDTO>> GetPlacesByFilters(JToken filters);
        Task<PlaceDTO> UpdatePlace(PlaceDTO place);
        Task DeletePlaceById(string id);
    }
}
