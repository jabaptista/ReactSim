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
            var dboQuestion = questionAdapter.ToDbo(question);
            await mongoDbRepository.AddOneAsync(dboQuestion).ConfigureAwait(false);
        }

        public async Task<IEnumerable<Question>> GetAllAsync()
        {
            var dboQuestions = await mongoDbRepository.GetAllAsync<dbo.Question>().ConfigureAwait(false);
            return dboQuestions.Select(questionAdapter.FromDbo);
        }
    }
}
