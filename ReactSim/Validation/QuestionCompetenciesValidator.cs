using ReactSim.DTO.Questions;
using System.Collections.Generic;
using System.Linq;

namespace ReactSim.Validation
{
    public class QuestionCompetenciesValidator : FormCreationRequestValidatorBase
    {
        protected override void HandleValidation(FormCreationRequest request, IList<string> errors)
        {
            if (request?.Questions == null)
            {
                errors.Add("O pedido de criação tem de incluir perguntas válidas.");
                return;
            }

            foreach (var question in request.Questions)
            {
                var competencies = question?.Competencies ?? Enumerable.Empty<int>();
                if (!competencies.Any())
                {
                    errors.Add($"Questão {question?.Id} tem de incluir pelo menos uma competência.");
                }
            }
        }
    }
}
