using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;

namespace ReactSim.Commands
{
    /// <summary>
    /// Resolves and executes handlers for incoming commands.
    /// </summary>
    public class CommandDispatcher : ICommandDispatcher
    {
        private readonly IServiceProvider serviceProvider;

        public CommandDispatcher(IServiceProvider serviceProvider)
        {
            this.serviceProvider = serviceProvider;
        }

        public Task DispatchAsync<TCommand>(TCommand command, CancellationToken cancellationToken = default)
            where TCommand : ICommand
        {
            if (command == null)
            {
                throw new ArgumentNullException(nameof(command));
            }

            using var scope = serviceProvider.CreateScope();
            var handler = scope.ServiceProvider.GetService<ICommandHandler<TCommand>>();
            if (handler == null)
            {
                throw new InvalidOperationException($"No handler registered for command type {typeof(TCommand).Name}.");
            }

            return handler.HandleAsync(command, cancellationToken);
        }
    }
}
