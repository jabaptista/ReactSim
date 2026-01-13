using System.Threading.Tasks;
using ReactSim.Domain.Model;

namespace ReactSim.Repositories
{
    public interface IEvaluationRepository
    {
        Task SaveAsync(EvaluationResult result);
    }
}
