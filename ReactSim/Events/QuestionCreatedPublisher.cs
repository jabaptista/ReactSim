using ReactSim.Domain.Model;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace ReactSim.Events
{
    public class QuestionCreatedPublisher : IQuestionCreatedPublisher
    {
        private readonly IEnumerable<IQuestionCreatedObserver> observers;

        public QuestionCreatedPublisher(IEnumerable<IQuestionCreatedObserver> observers)
        {
            this.observers = observers;
        }

        public async Task PublishAsync(Question question, CancellationToken cancellationToken = default)
        {
            if (question == null)
            {
                return;
            }

            var @event = new QuestionCreatedEvent(question);
            foreach (var observer in observers)
            {
                await observer.OnQuestionCreatedAsync(@event, cancellationToken).ConfigureAwait(false);
            }
        }
    }
}
