using MongoDB.Bson;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebService.Core.Entities;
using WebService.Core.Interfaces;
using WebService.Core.Interfaces.MongoDBInterfaces;

namespace WebService.Infrastructure.Repositories
{
    public class QuestionRepository : IQuestionRepository
    {
        private readonly IQuestionMongoDBContext _questionMongoDBContext;

        public QuestionRepository(IQuestionMongoDBContext questionMongoDBContext)
        {
            _questionMongoDBContext = questionMongoDBContext;
        }

        public async Task<QuestionDTO> CreateQuestion(QuestionDTO Question)
        {
            return await _questionMongoDBContext.CreateQuestion(Question);
        }

        public async Task<List<QuestionDTO>> GetAllQuestionsByPublicationId(string id)
        {
            return await _questionMongoDBContext.GetQuestionsByFilters(new JObject { new JProperty("PublicationId", id) });
        }

        public async Task<List<QuestionDTO>> GetAllQuestionsByUserId(string id)
        {
            return await _questionMongoDBContext.GetQuestionsByFilters(new JObject { new JProperty("UserId", id) });
        }

        public async Task<QuestionDTO> GetQuestionById(string id)
        {
            var Questions = await _questionMongoDBContext.GetQuestionsByFilters(new JObject { new JProperty("Id", id) });
            return Questions.FirstOrDefault();
        }

        public async Task<QuestionDTO> UpdateQuestionById(QuestionDTO Question)
        {
            return await _questionMongoDBContext.UpdateQuestion(Question);
        }

        public async Task<QuestionDTO> DeleteQuestionById(string id)
        {
            var QuestionDeleted = await GetQuestionById(id);
            await _questionMongoDBContext.DeleteQuestionById(id);
            return QuestionDeleted;
        }
    }
}
