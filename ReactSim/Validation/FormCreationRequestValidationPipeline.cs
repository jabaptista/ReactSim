using ReactSim.DTO.Questions;
using System;
using System.Collections.Generic;

namespace ReactSim.Validation
{
    public class FormCreationRequestValidationPipeline : IFormCreationRequestValidationPipeline
    {
        private readonly IFormCreationRequestValidator? firstValidator;

        public FormCreationRequestValidationPipeline(IEnumerable<IFormCreationRequestValidator> validators)
        {
            IFormCreationRequestValidator? head = null;
            IFormCreationRequestValidator? current = null;

            foreach (var validator in validators)
            {
                if (head == null)
                {
                    head = validator;
                    current = validator;
                    continue;
                }

                current = current?.SetNext(validator);
            }

            firstValidator = head;
        }

        public IReadOnlyCollection<string> Validate(FormCreationRequest request)
        {
            if (firstValidator == null)
            {
                return Array.Empty<string>();
            }

            var errors = new List<string>();
            firstValidator.Validate(request, errors);
            return errors;
        }
    }
}
