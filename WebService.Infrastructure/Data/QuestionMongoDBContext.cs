using Microsoft.Extensions.Options;
using MongoDB.Bson;
using MongoDB.Driver;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using WebService.Core.Entities;
using WebService.Core.Interfaces.MongoDBInterfaces;

namespace WebService.Infrastructure.Data
{
    public class QuestionMongoDBContext : IQuestionMongoDBContext
    {
        private IMongoCollection<QuestionDTO> _mongoDatabase;
        public QuestionMongoDBContext(IMongoDatabase mongoDatabase, IOptions<Collections> settings)
        {
            _mongoDatabase = mongoDatabase.GetCollection<QuestionDTO>(settings.Value.QuestionsCollectionName);
        }

        public async Task<QuestionDTO> CreateQuestion(QuestionDTO question)
        {
            try
            {
                await _mongoDatabase.InsertOneAsync(question);
                return await _mongoDatabase.FindAsync("{}", new FindOptions<QuestionDTO>() { Sort = Builders<QuestionDTO>.Sort.Descending("_id") }).Result.FirstOrDefaultAsync();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<List<QuestionDTO>> GetQuestionsByFilters(JToken filters)
        {
            try
            {
                List<QuestionDTO> result;
                var sort = Builders<QuestionDTO>.Sort.Descending("_id");
                var options = new FindOptions<QuestionDTO>()
                {
                    Sort = sort,
                };
                result = await _mongoDatabase.FindAsync(AddNoSQLFilters(filters), options).Result.ToListAsync();
                return result;
            }
            catch (Exception)
            {
                throw;
            }
        }

        private FilterDefinition<QuestionDTO> AddNoSQLFilters(JToken filters)
        {
            FilterDefinition<QuestionDTO> filterDefinition = Builders<QuestionDTO>.Filter.And
                (LoopFilters(filters));
            return filterDefinition;
        }

        private FilterDefinition<QuestionDTO>[] LoopFilters(JToken filters)
        {
            List<FilterDefinition<QuestionDTO>> filterList = new List<FilterDefinition<QuestionDTO>>();
            foreach (JProperty filter in filters)
            {
                if (!(filter.Name is null)) filterList.Add(AddNoSQLFilter(filter));
            }
            return filterList.ToArray();
        }

        private FilterDefinition<QuestionDTO> AddNoSQLFilter(JProperty filter)
        {
            switch (filter.Name)
            {
                case "All":
                    return Builders<QuestionDTO>.Filter.Where(q => true);
                case "Id":
                    return Builders<QuestionDTO>.Filter.Where(q => q.Id.Equals(ObjectId.Parse(filter.Value.ToString())));
                case "PublicationId":
                    return Builders<QuestionDTO>.Filter.Where(q => q.PublicationId.Equals(ObjectId.Parse(filter.Value.ToString())));
                case "UserId":
                    return Builders<QuestionDTO>.Filter.Where(q => q.UserId.Equals(ObjectId.Parse(filter.Value.ToString())));
                default:
                    return null;
            }
        }

        public async Task<QuestionDTO> UpdateQuestion(QuestionDTO Question)
        {
            try
            {
                await _mongoDatabase.ReplaceOneAsync(AddNoSQLFilters(new JProperty("Id", Question.Id)), Question);
                return Question;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task DeleteQuestionById(string id)
        {
            try
            {
                await _mongoDatabase.DeleteOneAsync(AddNoSQLFilters(new JObject { new JProperty("Id", id) }));
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
