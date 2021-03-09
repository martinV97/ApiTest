using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using WebService.Core.DTOs;
using WebService.Core.Entities;
using WebService.Core.Interfaces.ServicesInterfaces;


namespace WebServices.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class PlaceController : ControllerBase
    {
        private readonly IPlaceService _placeService;

        public PlaceController(IPlaceService placeService)
        {
            _placeService = placeService;
        }

        /// <summary>
        /// Create a Place
        /// </summary>
        /// <param name="Place">Place object</param>
        /// <returns>Place object with id</returns>
        [HttpPost]
        [Route("createPlace")]
        public async Task<IActionResult> CreatePlace(Place Place)
        {
            var result = await _placeService.CreatePlace(Place);
            return Ok(result);
        }

        /// <summary>
        /// Get a lis of Places by category
        /// </summary>
        /// <param name="type">Category id</param>
        /// <returns>Place list</returns>
        [HttpGet]
        [Route("getPlacesByType")]
        public async Task<IActionResult> GetPlacesByType(int type)
        {
            var result = await _placeService.GetAllPlacesByType(type);
            return Ok(result);
        }

        /// <summary>
        /// Get Place by id
        /// </summary>
        /// <param name="id">Place id</param>
        /// <returns>Place object</returns>
        [HttpGet]
        [Route("getPlaceById")]
        public async Task<IActionResult> GetPlaceById(string id)
        {
            var result = await _placeService.GetPlaceById(id);
            return Ok(result);
        }

        /// <summary>
        /// Update Place
        /// </summary>
        /// <param name="Place">Place object</param>
        /// <returns>Updated Place</returns>
        [HttpPut]
        [Route("updatePlace")]
        public async Task<IActionResult> UpdatePlace(PlaceDTO Place)
        {
            var result = await _placeService.UpdatePlaceById(Place);
            return Ok(result);
        }

        /// <summary>
        /// Delete Place
        /// </summary>
        /// <param name="id">Place object</param>
        /// <returns>Deleted Place object</returns>
        [HttpDelete]
        [Route("deletePlaceById")]
        public async Task<IActionResult> DeletePlaceById(string id)
        {
            var result = await _placeService.DeletePlaceById(id);
            return Ok(result);
        }
    }
}
