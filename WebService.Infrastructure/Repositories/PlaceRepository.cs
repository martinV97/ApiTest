using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebService.Core.DTOs;
using WebService.Core.Interfaces.MongoDBInterfaces;
using WebService.Core.Interfaces.RepositoryInterfaces;

namespace WebService.Infrastructure.Repositories
{
    public class PlaceRepository : IPlaceRepository
    {
        private readonly IPlaceMongoDBContext _placeMongoDBContext;

        public PlaceRepository(IPlaceMongoDBContext placeMongoDBContext)
        {
            _placeMongoDBContext = placeMongoDBContext;
        }

        public async Task<PlaceDTO> CreatePlace(PlaceDTO place)
        {
            return await _placeMongoDBContext.CreatePlace(place);
        }

        public async Task<List<PlaceDTO>> GetAllPlacesByType(int type)
        {
            return await _placeMongoDBContext.GetPlacesByFilters(new JObject { new JProperty("Type", type)});
        }

        public async Task<PlaceDTO> GetPlaceById(string id)
        {
            var places = await _placeMongoDBContext.GetPlacesByFilters(new JObject { new JProperty("Id", id) });
            return places.FirstOrDefault();
        }

        public async Task<PlaceDTO> UpdatePlaceById(PlaceDTO place)
        {
            return await _placeMongoDBContext.UpdatePlace(place);
        }

        public async Task<PlaceDTO> DeletePlaceById(string id)
        {
            var deletedPlace = await GetPlaceById(id);
            await _placeMongoDBContext.DeletePlaceById(id);
            return deletedPlace;
        }
    }
}
