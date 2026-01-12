using System;
using System.Collections.Generic;
using ReactSim.Domain.Model;
using ReactSim.Repositories;
using System;

namespace ReactSim.Services
{
    public class QuestionService : IQuestionService
    {
        private readonly IQuestionRepository repo;

        public QuestionService(IQuestionRepository repo)
        {
            this.repo = repo;
        }

        public async Task CreateQuestionsAsync(Question question)
        {
            if (question == null)
            {
                throw new ArgumentNullException(nameof(question));
            }

            await repo.CreateAsync(question).ConfigureAwait(false);
        }

        public Task<IEnumerable<Question>> GetAllQuestionsAsync()
        {
            return repo.GetAllAsync();
        }

        public Task UpdateQuestionAsync(Question question)
        {
            if (question == null)
            {
                throw new ArgumentNullException(nameof(question));
            }

            return repo.UpdateAsync(question);
        }

        public Task DeleteQuestionAsync(int questionId)
        {
            if (questionId <= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(questionId));
            }

            return repo.DeleteAsync(questionId);
        }
    }
}
