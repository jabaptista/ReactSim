namespace ReactSim.Services
{
    public interface IQuestionService
    {
        public Task<IEnumerable<Domain.Model.Question>> GetAllQuestionsAsync();

        public Task CreateQuestionsAsync(Domain.Model.Question questions);
    }
}
