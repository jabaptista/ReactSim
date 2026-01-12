using System.Threading;
using System.Threading.Tasks;

namespace ReactSim.Commands
{
    /// <summary>
    /// Dispatches commands to their respective handlers.
    /// </summary>
    public interface ICommandDispatcher
    {
        Task DispatchAsync<TCommand>(TCommand command, CancellationToken cancellationToken = default)
            where TCommand : ICommand;
    }
}
