using ReactSim.Domain.Model;
namespace ReactSim.Repositories
{
    public interface IQuestionRepository
    {
        Task<IEnumerable<Question>> GetAllAsync();
        Task CreateAsync(Question questions);
    }
}