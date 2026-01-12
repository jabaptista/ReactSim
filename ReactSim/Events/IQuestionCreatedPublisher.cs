using ReactSim.Domain.Model;
using System.Threading;
using System.Threading.Tasks;

namespace ReactSim.Events
{
    public interface IQuestionCreatedPublisher
    {
        Task PublishAsync(Question question, CancellationToken cancellationToken = default);
    }
}
