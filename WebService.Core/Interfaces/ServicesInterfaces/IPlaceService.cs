using System.Collections.Generic;
using System.Threading.Tasks;
using WebService.Core.DTOs;
using WebService.Core.Entities;

namespace WebService.Core.Interfaces.ServicesInterfaces
{
    public interface IPlaceService
    {
        Task<PlaceDTO> CreatePlace(Place place);
        Task<List<PlaceDTO>> GetAllPlacesByType(int type);
        Task<PlaceDTO> GetPlaceById(string id);
        Task<PlaceDTO> UpdatePlaceById(PlaceDTO place);
        Task<PlaceDTO> DeletePlaceById(string id);
    }
}
