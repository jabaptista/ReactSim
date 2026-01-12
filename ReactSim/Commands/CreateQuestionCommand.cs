using ReactSim.Domain.Model;

namespace ReactSim.Commands
{
    public sealed record CreateQuestionCommand(Question Question) : ICommand;
}
