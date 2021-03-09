using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using WebService.Core.Entities;
using WebService.Core.Interfaces;

namespace WebServices.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PublicationController : ControllerBase
    {
        private readonly IPublicationService _publicationService;

        public PublicationController(IPublicationService productService)
        {
            _publicationService = productService;
        }

        ///<summary>
        ///Creates a Publication
        ///</summary>
        [HttpPost]
        [Route("createPublication")]
        [ProducesResponseType(typeof(PublicationDTO), 200)]
        public async Task<IActionResult> CreatePublication(Publication publication)
        {
            var result = await _publicationService.CreatePublication(publication);
            return Ok(result);
        }

        ///<summary>
        ///Finds all publications by userId
        ///</summary>
        [HttpGet]
        [Route("getPublicationByUserId")]
        [ProducesResponseType(typeof(List<PublicationDTO>), 200)]
        public async Task<IActionResult> GetAllPublicationByUserId(string id)
        {
            var result = await _publicationService.GetAllPublicationsByUserId(id);
            return Ok(result);
        }

        ///<summary>
        ///Finds all publications by productId
        ///</summary>
        [HttpGet]
        [Route("getPublicationByProductId")]
        [ProducesResponseType(typeof(List<PublicationDTO>), 200)]
        public async Task<IActionResult> GetAllPublicationByProductId(string id)
        {
            var result = await _publicationService.GetAllPublicationsByProductId(id);
            return Ok(result);
        }

        ///<summary>
        ///Finds all publications by date
        ///</summary>
        [HttpGet]
        [Route("getPublicationsByDate")]
        [ProducesResponseType(typeof(List<PublicationDTO>), 200)]
        public async Task<IActionResult> GetPublicationByDate(DateTime date)
        {
            var result = await _publicationService.GetPublicationsByDate(date);
            return Ok(result);
        }

        ///<summary>
        ///Find a publication by Id
        ///</summary>
        [HttpGet]
        [Route("getPublicationById")]
        [ProducesResponseType(typeof(PublicationDTO), 200)]
        public async Task<IActionResult> GetPublicationById(string id)
        {
            var result = await _publicationService.GetPublicationById(id);
            return Ok(result);
        }

        ///<summary>
        ///Update a publication
        ///</summary>
        [HttpPut]
        [Route("updatePublication")]
        [ProducesResponseType(typeof(PublicationDTO), 200)]
        public async Task<IActionResult> UpdatePublication(PublicationDTO publication)
        {
            var result = await _publicationService.UpdatePublicationById(publication);
            return Ok(result);
        }

        ///<summary>
        ///Delete a publication by id
        ///</summary>
        [HttpDelete]
        [Route("deletePublication")]
        [ProducesResponseType(typeof(PublicationDTO), 200)]
        public async Task<IActionResult> DeletePublication(string id)
        {
            var result = await _publicationService.DeletePublicationById(id);
            return Ok(result);
        }
    }
}
