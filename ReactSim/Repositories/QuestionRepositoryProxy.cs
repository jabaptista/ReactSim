using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging;
using ReactSim.Domain.Model;

namespace ReactSim.Repositories
{
    /// <summary>
    /// Proxy para o <see cref="IQuestionRepository"/> que acrescenta cache e registo de actividade antes
    /// de delegar para o repositorio real (acesso MongoDB).
    /// </summary>
    public class QuestionRepositoryProxy : IQuestionRepository
    {
        private const string CacheKeyPrefix = "QuestionRepositoryProxy.GetByActivity";
        private readonly QuestionRepository innerRepository;
        private readonly ILogger<QuestionRepositoryProxy> logger;
        private readonly IMemoryCache cache;

        public QuestionRepositoryProxy(
            QuestionRepository innerRepository,
            ILogger<QuestionRepositoryProxy> logger,
            IMemoryCache cache)
        {
            this.innerRepository = innerRepository;
            this.logger = logger;
            this.cache = cache;
        }

        public async Task<IEnumerable<Question>> GetByActivityAsync(string activityId)
        {
            var cacheKey = $"{CacheKeyPrefix}:{activityId}";

            if (!cache.TryGetValue(cacheKey, out var cachedQuestionsObj) || cachedQuestionsObj is not IEnumerable<Question> cachedQuestions)
            {
                logger.LogInformation("[Proxy] A carregar perguntas da base de dados para ActivityId {ActivityId}.", activityId);
                cachedQuestions = await innerRepository.GetByActivityAsync(activityId).ConfigureAwait(false);
                cache.Set(cacheKey, cachedQuestions, TimeSpan.FromMinutes(5));
            }
            else
            {
                logger.LogDebug("[Proxy] Perguntas servidas a partir da cache em memoria.");
            }

            return cachedQuestions;
        }

        public async Task CreateAsync(Question question)
        {
            logger.LogInformation("[Proxy] A encaminhar criacao da pergunta {QuestionId}.", question?.Id);
            await innerRepository.CreateAsync(question).ConfigureAwait(false);
            cache.Remove($"{CacheKeyPrefix}:{question?.ActivityId}");
        }

        public Task<IEnumerable<Question>> GetAllAsync()
        {
            return innerRepository.GetAllAsync();
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
