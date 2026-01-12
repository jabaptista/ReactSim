using System.Threading.Tasks;

namespace ReactSim.Events
{
    public interface IQuestionCreatedObserver
    {
        Task OnQuestionCreatedAsync(QuestionCreatedEvent @event, CancellationToken cancellationToken = default);
    }
}
