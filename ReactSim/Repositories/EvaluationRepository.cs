using System;
using System.Threading.Tasks;
using ReactSim.Adapters;
using ReactSim.Domain.Model;

namespace ReactSim.Repositories
{
    public class EvaluationRepository : IEvaluationRepository
    {
        private readonly IMongoDbRepository mongoDbRepository;
        private readonly IEvaluationDboAdapter evaluationDboAdapter;

        public EvaluationRepository(IMongoDbRepository mongoDbRepository, IEvaluationDboAdapter evaluationDboAdapter)
        {
            this.mongoDbRepository = mongoDbRepository;
            this.evaluationDboAdapter = evaluationDboAdapter;
        }

        public async Task SaveAsync(EvaluationResult result)
        {
            ArgumentNullException.ThrowIfNull(result);

            var dbo = evaluationDboAdapter.ToDbo(result);
            await mongoDbRepository.AddOneAsync(dbo).ConfigureAwait(false);
        }
    }
}
