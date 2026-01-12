using System;
using System.Threading;
using System.Threading.Tasks;
using ReactSim.Services;

namespace ReactSim.Commands
{
    public class CreateQuestionCommandHandler : ICommandHandler<CreateQuestionCommand>
    {
        private readonly IQuestionService questionService;

        public CreateQuestionCommandHandler(IQuestionService questionService)
        {
            this.questionService = questionService;
        }

        public async Task HandleAsync(CreateQuestionCommand command, CancellationToken cancellationToken = default)
        {
            if (command == null)
            {
                throw new ArgumentNullException(nameof(command));
            }

            await questionService.CreateQuestionsAsync(command.Question).ConfigureAwait(false);
        }
    }
}
