using ReactSim.Domain.Model;
using System;

namespace ReactSim.Events
{
    /// <summary>
    /// Evento emitido quando uma nova questão é criada.
    /// </summary>
    public sealed class QuestionCreatedEvent
    {
        public QuestionCreatedEvent(Question question)
        {
            Question = question ?? throw new ArgumentNullException(nameof(question));
            OccurredAtUtc = DateTime.UtcNow;
        }

        public Question Question { get; }

        public DateTime OccurredAtUtc { get; }
    }
}
