using ReactSim.DTO.Questions;
using System.Collections.Generic;
using System.Linq;

namespace ReactSim.Validation
{
    public class QuestionOptionsValidator : FormCreationRequestValidatorBase
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
                var options = question?.Options?.ToList() ?? new List<AwnserOption>();
                if (options.Count < 2)
                {
                    errors.Add($"Questão {question?.Id} tem de conter pelo menos duas opções.");
                    continue;
                }

                if (!options.Any(option => option.Id == question?.RightAwnser))
                {
                    errors.Add($"Questão {question?.Id} tem de definir uma resposta correcta presente nas opções.");
                }

                if (options.Any(option => string.IsNullOrWhiteSpace(option.Text)))
                {
                    errors.Add($"Questão {question?.Id} contém opções com texto vazio.");
                }
            }
        }
    }
}
