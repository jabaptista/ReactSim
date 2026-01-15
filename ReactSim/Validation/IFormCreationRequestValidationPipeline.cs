using ReactSim.DTO.Questions;
using System.Collections.Generic;

namespace ReactSim.Validation
{
    public interface IFormCreationRequestValidationPipeline
    {
        IReadOnlyCollection<string> Validate(FormCreationRequest request);
    }
}
