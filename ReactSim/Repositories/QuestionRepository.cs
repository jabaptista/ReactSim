using System;
using MongoDB.Driver;
using ReactSim.Adapters;
using ReactSim.Domain.Model;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ReactSim.Repositories
{
    public class QuestionRepository : IQuestionRepository
    {
        private readonly IMongoDbRepository mongoDbRepository;
        private readonly IQuestionDboAdapter questionAdapter;

        public QuestionRepository(IMongoDbRepository mongoDbRepository, IQuestionDboAdapter questionAdapter)
        {
            this.mongoDbRepository = mongoDbRepository;
            this.questionAdapter = questionAdapter;
        }

        public async Task CreateAsync(Question question)
        {
            if (question == null)
            {
                throw new ArgumentNullException(nameof(question));
            }

            var dboQuestion = questionAdapter.ToDbo(question);
            await mongoDbRepository.AddOneAsync(dboQuestion).ConfigureAwait(false);
        }

        public async Task<IEnumerable<Question>> GetByActivityAsync(string activityId)
        {
            var filter = Builders<dbo.Question>.Filter.Eq(q => q.ActivityId, activityId);
            var dboQuestions = await mongoDbRepository.GetManyAsync(filter).ConfigureAwait(false);
            return dboQuestions.Select(questionAdapter.FromDbo);
        }

        public async Task<IEnumerable<Question>> GetAllAsync()
        {
            var dboQuestions = await mongoDbRepository.GetAllAsync<dbo.Question>().ConfigureAwait(false);
            return dboQuestions.Select(questionAdapter.FromDbo);
        }

        public Task UpdateAsync(Question question)
        {
            throw new NotImplementedException();
        }

        public Task DeleteAsync(int questionId)
        {
            throw new NotImplementedException();
        }
    }
}

