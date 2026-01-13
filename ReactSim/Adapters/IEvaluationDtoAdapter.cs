using ReactSim.DTO.Evaluation;
using DomainEvaluationSubmission = ReactSim.Domain.Model.EvaluationSubmission;
using DomainEvaluationResult = ReactSim.Domain.Model.EvaluationResult;

namespace ReactSim.Adapters
{
    public interface IEvaluationDtoAdapter
    {
        DomainEvaluationSubmission FromRequest(SubmitEvaluationRequest request);
        SubmitEvaluationResponse ToResponse(DomainEvaluationResult result);
    }
}
