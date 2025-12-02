using ReactSim.Domain.Model;
using ReactSim.Repositories;

namespace ReactSim.Services
{
    public class QuestionService : IQuestionService
    {
        private readonly IQuestionRepository _repo;

        public QuestionService(IQuestionRepository repo)
        {
            _repo = repo;
        }

        public Task CreateQuestionsAsync(Question question)
        {
            _repo.CreateAsync(question);

            return Task.CompletedTask;
            
        }

        public Task<IEnumerable<Question>> GetAllQuestionsAsync()
        {
            return _repo.GetAllAsync();
        }
    }
}
