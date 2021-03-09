using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using WebService.Core.Entities;
using WebService.Core.Interfaces;

namespace WebServices.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class QuestionController : ControllerBase
    {
        private readonly IQuestionService _questionService;

        public QuestionController (IQuestionService questionService)
        {
            _questionService = questionService;
        }

        ///<summary>
        ///Creates a Question
        ///</summary>
        [HttpPost]
        [Route("createQuestion")]
        [ProducesResponseType(typeof(QuestionDTO), 200)]
        public async Task<IActionResult> CreateQuestion(Question Question)
        {
            var result = await _questionService.CreateQuestion(Question);
            return Ok(result);
        }

        ///<summary>
        ///Finds all Questions by userId
        ///</summary>
        [HttpGet]
        [Route("getQuestionByUserId")]
        [ProducesResponseType(typeof(List<QuestionDTO>), 200)]
        public async Task<IActionResult> GetAllQuestionByUserId(string id)
        {
            var result = await _questionService.GetAllQuestionsByUserId(id);
            return Ok(result);
        }

        ///<summary>
        ///Finds all Questions by publicationtId 
        ///</summary>
        [HttpGet]
        [Route("getQuestionByProductId")]
        [ProducesResponseType(typeof(List<QuestionDTO>), 200)]
        public async Task<IActionResult> GetAllQuestionByPublicationId(string id)
        {
            var result = await _questionService.GetAllQuestionsByPublicationId(id);
            return Ok(result);
        }

        ///<summary>
        ///Find a Question by Id
        ///</summary>
        [HttpGet]
        [Route("getQuestionById")]
        [ProducesResponseType(typeof(QuestionDTO), 200)]
        public async Task<IActionResult> GetQuestionById(string id)
        {
            var result = await _questionService.GetQuestionById(id);
            return Ok(result);
        }

        ///<summary>
        ///Update a Question
        ///</summary>
        [HttpPut]
        [Route("updateQuestion")]
        [ProducesResponseType(typeof(QuestionDTO), 200)]
        public async Task<IActionResult> UpdateQuestion(QuestionDTO Question)
        {
            var result = await _questionService.UpdateQuestionById(Question);
            return Ok(result);
        }

        ///<summary>
        ///Delete a Question by id
        ///</summary>
        [HttpDelete]
        [Route("deleteQuestion")]
        [ProducesResponseType(typeof(QuestionDTO), 200)]
        public async Task<IActionResult> DeleteQuestion(string id)
        {
            var result = await _questionService.DeleteQuestionById(id);
            return Ok(result);
        }
    }
}
