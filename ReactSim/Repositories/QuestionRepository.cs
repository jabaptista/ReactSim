using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MongoDB.Driver;
using ReactSim.Adapters;
using DomainQuestion = ReactSim.Domain.Model.Question;
using DboQuestion = ReactSim.Repositories.dbo.Question;

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

		public async Task CreateAsync(DomainQuestion question)
        {
            var dboQuestion = questionAdapter.ToDbo(question);
            await mongoDbRepository.AddOneAsync(dboQuestion).ConfigureAwait(false);
        }

		public async Task<IEnumerable<DomainQuestion>> GetAllAsync()
        {
			var dboQuestions = await mongoDbRepository.GetAllAsync<DboQuestion>().ConfigureAwait(false);
            return dboQuestions.Select(questionAdapter.FromDbo);
        }

		public async Task UpdateAsync(DomainQuestion question)
		{
			ArgumentNullException.ThrowIfNull(question);
			if (question.Id <= 0)
			{
				throw new ArgumentOutOfRangeException(nameof(question.Id));
			}

			var dboQuestion = questionAdapter.ToDbo(question);
			var filter = Builders<DboQuestion>.Filter.Eq("_id", question.Id);
			var update = Builders<DboQuestion>.Update
				.Set(q => q.Description, dboQuestion.Description)
				.Set(q => q.Competencies, dboQuestion.Competencies)
				.Set(q => q.Options, dboQuestion.Options)
				.Set(q => q.MediaResources, dboQuestion.MediaResources)
				.Set(q => q.RightAwnser, dboQuestion.RightAwnser);

			await mongoDbRepository.UpdateOneAsync(filter, update).ConfigureAwait(false);
		}

		public async Task DeleteAsync(int questionId)
		{
			if (questionId <= 0)
			{
				throw new ArgumentOutOfRangeException(nameof(questionId));
			}

			var filter = Builders<DboQuestion>.Filter.Eq("_id", questionId);
			await mongoDbRepository.DeleteOneAsync(filter).ConfigureAwait(false);
		}
    }
}
