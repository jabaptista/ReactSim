using ReactSim.Domain.Model;

namespace ReactSim.Services
{
	public interface IQuestionService
	{
		Task<IEnumerable<Question>> GetAllQuestionsAsync();
		Task CreateQuestionsAsync(Question question);
		Task UpdateQuestionAsync(Question question);
		Task DeleteQuestionAsync(int questionId);
	}
}
