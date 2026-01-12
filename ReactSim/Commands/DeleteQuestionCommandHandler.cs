using System;
using System.Threading;
using System.Threading.Tasks;
using ReactSim.Services;

namespace ReactSim.Commands
{
    public class DeleteQuestionCommandHandler : ICommandHandler<DeleteQuestionCommand>
    {
        private readonly IQuestionService questionService;

        public DeleteQuestionCommandHandler(IQuestionService questionService)
        {
            this.questionService = questionService;
        }

        public async Task HandleAsync(DeleteQuestionCommand command, CancellationToken cancellationToken = default)
        {
            if (command == null)
            {
                throw new ArgumentNullException(nameof(command));
            }

            await questionService.DeleteQuestionAsync(command.QuestionId).ConfigureAwait(false);
        }
    }
}
