using Mapster;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using WebService.Core.DTOs;
using WebService.Core.Entities;
using WebService.Core.Enumerations;
using WebService.Core.Interfaces.RepositoryInterfaces;
using WebService.Core.Interfaces.ServicesInterfaces;

namespace WebService.Core.Services
{
    public class PlaceService : IPlaceService
    {
        private readonly IPlaceRepository _placeRepository;

        public PlaceService(IPlaceRepository placeRepository)
        {
            _placeRepository = placeRepository;
        }

        public async Task<PlaceDTO> CreatePlace(Place place)
        {
            if (Enum.IsDefined(typeof(PlaceTypeEnum), place.Type))
                return await _placeRepository.CreatePlace(place.Adapt<PlaceDTO>());
            else
                throw new ArgumentException($"The placeType: {place.Type} it doesn't exist");
        }

        public async Task<List<PlaceDTO>> GetAllPlacesByType(int type)
        {
            if (Enum.IsDefined(typeof(PlaceTypeEnum), type))
                return await _placeRepository.GetAllPlacesByType(type);
            else
                throw new ArgumentException($"The placeType: {type} it doesn't exist");
        }

        public async Task<PlaceDTO> GetPlaceById(string id)
        {
            return await _placeRepository.GetPlaceById(id);
        }

        public async Task<PlaceDTO> UpdatePlaceById(PlaceDTO place)
        {
            if (Enum.IsDefined(typeof(PlaceTypeEnum), place.Type))
                return await _placeRepository.UpdatePlaceById(place);
            else
                throw new ArgumentException($"The placeType: {place.Type} it doesn't exist");
        }

        public async Task<PlaceDTO> DeletePlaceById(string id)
        {
            return await _placeRepository.DeletePlaceById(id);
        }
    }
}
