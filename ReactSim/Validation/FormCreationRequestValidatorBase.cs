using ReactSim.DTO.Questions;
using System.Collections.Generic;

namespace ReactSim.Validation
{
    public abstract class FormCreationRequestValidatorBase : IFormCreationRequestValidator
    {
        private IFormCreationRequestValidator? next;

        public IFormCreationRequestValidator SetNext(IFormCreationRequestValidator next)
        {
            this.next = next;
            return next;
        }

        public void Validate(FormCreationRequest request, IList<string> errors)
        {
            HandleValidation(request, errors);
            next?.Validate(request, errors);
        }

        protected abstract void HandleValidation(FormCreationRequest request, IList<string> errors);
    }
}
