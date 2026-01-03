using DomainQuestion = ReactSim.Domain.Model.Question;
using DtoQuestion = ReactSim.DTO.Questions.Question;

namespace ReactSim.Adapters
{
    public interface IQuestionDtoAdapter
    {
        DomainQuestion FromDto(DtoQuestion dto);
        DtoQuestion ToDto(DomainQuestion domain);
    }
}
