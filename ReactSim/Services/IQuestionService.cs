namespace ReactSim.Services
{
    public interface IQuestionService
    {
        public Task<IEnumerable<Domain.Model.Question>> GetQuestionsByActivityAsync(string activityId);

        public Task CreateQuestionsAsync(Domain.Model.Question questions);
    }
}
