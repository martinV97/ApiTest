using System.Collections.Generic;
using System.Threading.Tasks;
using WebService.Core.DTOs;

namespace WebService.Core.Interfaces.RepositoryInterfaces
{
    public interface IPlaceRepository
    {
        Task<PlaceDTO> CreatePlace(PlaceDTO place);
        Task<List<PlaceDTO>> GetAllPlacesByType(int type);
        Task<PlaceDTO> GetPlaceById(string id);
        Task<PlaceDTO> UpdatePlaceById(PlaceDTO place);
        Task<PlaceDTO> DeletePlaceById(string id);
    }
}
