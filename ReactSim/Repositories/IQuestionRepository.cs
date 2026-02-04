using ReactSim.Domain.Model;
namespace ReactSim.Repositories
{
    public interface IQuestionRepository
    {
        Task<IEnumerable<Question>> GetAllAsync();
        Task<IEnumerable<Question>> GetByActivityAsync(string activityId);
        Task CreateAsync(Question questions);
        Task UpdateAsync(Question question);
        Task DeleteAsync(int questionId);
    }
} 