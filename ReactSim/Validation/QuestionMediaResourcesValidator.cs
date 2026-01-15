using ReactSim.DTO.Questions;
using System.Collections.Generic;
using System.Linq;

namespace ReactSim.Validation
{
    public class QuestionMediaResourcesValidator : FormCreationRequestValidatorBase
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
                if (question?.MultiMediaRessorces == null)
                {
                    continue;
                }

                foreach (var media in question.MultiMediaRessorces)
                {
                    if (media == null)
                    {
                        errors.Add($"Questão {question?.Id} contém recursos multimédia inválidos.");
                        continue;
                    }

                    if (string.IsNullOrWhiteSpace(media.Type) || string.IsNullOrWhiteSpace(media.URL))
                    {
                        errors.Add($"Questão {question?.Id} contém recursos multimédia sem tipo ou URL.");
                    }
                }
            }
        }
    }
}
