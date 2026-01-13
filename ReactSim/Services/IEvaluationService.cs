using System.Threading.Tasks;
using ReactSim.Domain.Model;

namespace ReactSim.Services
{
    public interface IEvaluationService
    {
        Task<EvaluationResult> EvaluateAsync(EvaluationSubmission submission);
    }
}
