using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
using WebService.Core.Entities;

namespace WebService.Core.Interfaces.MongoDBInterfaces
{
    public interface IQuestionMongoDBContext
    {
        Task<QuestionDTO> CreateQuestion(QuestionDTO question);
        Task<List<QuestionDTO>> GetQuestionsByFilters(JToken filters);
        Task<QuestionDTO> UpdateQuestion(QuestionDTO question);
        Task DeleteQuestionById(string id);
    }
}
