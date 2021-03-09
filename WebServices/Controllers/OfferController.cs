using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using WebService.Core.Entities;
using WebService.Core.Interfaces;
namespace WebServices.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OfferController : ControllerBase
    {
        private readonly IOfferService _offerService;

        public OfferController(IOfferService offerService)
        {
            _offerService = offerService;
        }

        ///<summary>
        ///Creates a Offer
        ///</summary>
        [HttpPost]
        [Route("createOffer")]
        [ProducesResponseType(typeof(OfferDTO), 200)]
        public async Task<IActionResult> CreateOffer(Offer Offer)
        {
            var result = await _offerService.CreateOffer(Offer);
            return Ok(result);
        }

        ///<summary>
        ///Finds all Offers by userId
        ///</summary>
        [HttpGet]
        [Route("getOfferByUserId")]
        [ProducesResponseType(typeof(List<OfferDTO>), 200)]
        public async Task<IActionResult> GetAllOfferByUserId(string id)
        {
            var result = await _offerService.GetAllOffersByUserId(id);
            return Ok(result);
        }

        ///<summary>
        ///Finds all Offers by publicationtId 
        ///</summary>
        [HttpGet]
        [Route("getOfferByProductId")]
        [ProducesResponseType(typeof(List<OfferDTO>), 200)]
        public async Task<IActionResult> GetAllOfferByPublicationId(string id)
        {
            var result = await _offerService.GetAllOffersByPublicationId(id);
            return Ok(result);
        }

        ///<summary>
        ///Find a Offer by Id
        ///</summary>
        [HttpGet]
        [Route("getOfferById")]
        [ProducesResponseType(typeof(OfferDTO), 200)]
        public async Task<IActionResult> GetOfferById(string id)
        {
            var result = await _offerService.GetOfferById(id);
            return Ok(result);
        }

        ///<summary>
        ///Update a Offer
        ///</summary>
        [HttpPut]
        [Route("updateOffer")]
        [ProducesResponseType(typeof(OfferDTO), 200)]
        public async Task<IActionResult> UpdateOffer(OfferDTO Offer)
        {
            var result = await _offerService.UpdateOfferById(Offer);
            return Ok(result);
        }

        ///<summary>
        ///Delete a Offer by id
        ///</summary>
        [HttpDelete]
        [Route("deleteOffer")]
        [ProducesResponseType(typeof(OfferDTO), 200)]
        public async Task<IActionResult> DeleteOffer(string id)
        {
            var result = await _offerService.DeleteOfferById(id);
            return Ok(result);
        }
    }
}
