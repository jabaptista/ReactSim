namespace ReactSim.Commands
{
    public sealed record DeleteQuestionCommand(int QuestionId) : ICommand;
}
