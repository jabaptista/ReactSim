using ReactSim.Domain.Model;
using DboEvaluationResult = ReactSim.Repositories.dbo.EvaluationResult;

namespace ReactSim.Adapters
{
    public interface IEvaluationDboAdapter
    {
        DboEvaluationResult ToDbo(EvaluationResult domain);
    }
}
