using ReactSim.DTO.Questions;
using System.Collections.Generic;

namespace ReactSim.Validation
{
    public interface IFormCreationRequestValidator
    {
        IFormCreationRequestValidator SetNext(IFormCreationRequestValidator next);

        void Validate(FormCreationRequest request, IList<string> errors);
    }
}
