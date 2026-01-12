using System;
using ReactSim.Domain.Model;
using ReactSim.Events;
using ReactSim.Repositories;

namespace ReactSim.Services
{
    public class QuestionService : IQuestionService
    {
        private readonly IQuestionRepository _repo;
        private readonly IQuestionCreatedPublisher questionCreatedPublisher;

        public QuestionService(IQuestionRepository repo, IQuestionCreatedPublisher questionCreatedPublisher)
        {
            _repo = repo;
            this.questionCreatedPublisher = questionCreatedPublisher;
        }

        public async Task CreateQuestionsAsync(Question question)
        {
            if (question == null)
            {
                throw new ArgumentNullException(nameof(question));
            }

            await _repo.CreateAsync(question).ConfigureAwait(false);
            await questionCreatedPublisher.PublishAsync(question).ConfigureAwait(false);
            
        }

        public Task<IEnumerable<Question>> GetAllQuestionsAsync()
        {
            return _repo.GetAllAsync();
        }
    }
}
