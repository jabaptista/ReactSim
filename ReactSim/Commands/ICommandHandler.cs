using System.Threading;
using System.Threading.Tasks;

namespace ReactSim.Commands
{
    /// <summary>
    /// Handles execution of a specific command type.
    /// </summary>
    /// <typeparam name="TCommand">Command type handled.</typeparam>
    public interface ICommandHandler<in TCommand> where TCommand : ICommand
    {
        Task HandleAsync(TCommand command, CancellationToken cancellationToken = default);
    }
}
