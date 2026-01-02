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
        private const string CacheKey = "QuestionRepositoryProxy.GetAll";
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

        public async Task<IEnumerable<Question>> GetAllAsync()
        {
            if (!cache.TryGetValue(CacheKey, out var cachedQuestionsObj) || cachedQuestionsObj is not IEnumerable<Question> cachedQuestions)
            {
                logger.LogInformation("[Proxy] A carregar perguntas da base de dados.");
                cachedQuestions = await innerRepository.GetAllAsync().ConfigureAwait(false);
                cache.Set(CacheKey, cachedQuestions, TimeSpan.FromMinutes(5));
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
            cache.Remove(CacheKey);
        }
    }
}
