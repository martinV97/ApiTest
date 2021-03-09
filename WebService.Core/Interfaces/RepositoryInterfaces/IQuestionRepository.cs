using System.Collections.Generic;
using System.Threading.Tasks;
using WebService.Core.Entities;

namespace WebService.Core.Interfaces
{
    public interface IQuestionRepository
    {
        Task<QuestionDTO> CreateQuestion(QuestionDTO question);
        Task<List<QuestionDTO>> GetAllQuestionsByPublicationId(string id);
        Task<List<QuestionDTO>> GetAllQuestionsByUserId(string id);
        Task<QuestionDTO> GetQuestionById(string id);
        Task<QuestionDTO> UpdateQuestionById(QuestionDTO question);
        Task<QuestionDTO> DeleteQuestionById(string id);
    }
}
