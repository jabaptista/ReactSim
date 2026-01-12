using System;
using System.Threading;
using System.Threading.Tasks;
using ReactSim.Services;

namespace ReactSim.Commands
{
    public class UpdateQuestionCommandHandler : ICommandHandler<UpdateQuestionCommand>
    {
        private readonly IQuestionService questionService;

        public UpdateQuestionCommandHandler(IQuestionService questionService)
        {
            this.questionService = questionService;
        }

        public async Task HandleAsync(UpdateQuestionCommand command, CancellationToken cancellationToken = default)
        {
            if (command == null)
            {
                throw new ArgumentNullException(nameof(command));
            }

            await questionService.UpdateQuestionAsync(command.Question).ConfigureAwait(false);
        }
    }
}
