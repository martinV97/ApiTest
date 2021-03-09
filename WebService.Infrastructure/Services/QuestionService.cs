using Mapster;
using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using WebService.Core.Entities;
using WebService.Core.Interfaces;

namespace WebService.Infrastructure.Repositories
{
    public class QuestionService : IQuestionService
    {
        private readonly IQuestionRepository _questionRepository;
        private readonly IUserRepository _userRepository;
        private readonly IPublicationRepository _publicationRepository;

        public QuestionService(IQuestionRepository questionRepository, IUserRepository userRepository, IPublicationRepository publicationRepository)
        {
            _questionRepository = questionRepository;
            _userRepository = userRepository;
            _publicationRepository = publicationRepository;
        }

        public async Task<QuestionDTO> CreateQuestion(Question question)
        {
            if (await _userRepository.GetUserById(question.UserId) is null)
                throw new ArgumentNullException($"The user: {question.UserId} it doesn't exist");
            if (await _publicationRepository.GetPublicationById(question.PublicationId) is null)
                throw new ArgumentNullException($"The publication: {question.PublicationId} it doesn't exist");
            return await _questionRepository.CreateQuestion(question.Adapt<QuestionDTO>());
        }

        public async Task<List<QuestionDTO>> GetAllQuestionsByPublicationId(string id)
        {
            return await _questionRepository.GetAllQuestionsByPublicationId(id);
        }

        public async Task<List<QuestionDTO>> GetAllQuestionsByUserId(string id)
        {
            return await _questionRepository.GetAllQuestionsByUserId(id);
        }

        public async Task<QuestionDTO> GetQuestionById(string id)
        {
            return await _questionRepository.GetQuestionById(id);
        }

        public async Task<QuestionDTO> UpdateQuestionById(QuestionDTO question)
        {
            if (await _userRepository.GetUserById(question.UserId) is null)
                throw new ArgumentNullException($"The user: {question.UserId} it doesn't exist");
            if (await _publicationRepository.GetPublicationById(question.PublicationId) is null)
                throw new ArgumentNullException($"The publication: {question.PublicationId} it doesn't exist");
            if (question.State)
            {
                if (String.IsNullOrEmpty(question.Answer))
                    throw new ArgumentNullException($"The payment can't be null");
            }
            return await _questionRepository.UpdateQuestionById(question);
        }

        public async Task<QuestionDTO> DeleteQuestionById(string id)
        {
            return await _questionRepository.DeleteQuestionById(id);
        }
    }
}
