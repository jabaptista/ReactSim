using ReactSim.Domain.Model;

namespace ReactSim.Commands
{
    public sealed record UpdateQuestionCommand(Question Question) : ICommand;
}
