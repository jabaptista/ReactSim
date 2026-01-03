using System;
using System.Linq;
using DomainQuestion = ReactSim.Domain.Model.Question;
using DomainAnswerOption = ReactSim.Domain.Model.AwnserOption;
using DomainMedia = ReactSim.Domain.Model.MultiMediaResource;
using DtoQuestion = ReactSim.DTO.Questions.Question;
using DtoAnswerOption = ReactSim.DTO.Questions.AwnserOption;
using DtoMedia = ReactSim.DTO.Questions.MultiMediaResource;

namespace ReactSim.Adapters
{
    public class QuestionDtoAdapter : IQuestionDtoAdapter
    {
        public DomainQuestion FromDto(DtoQuestion dto)
        {
            ArgumentNullException.ThrowIfNull(dto);

            var builder = DomainQuestion.Builder()
                .WithId(dto.Id)
                .WithDescription(dto.Description)
                .WithCompetencies(dto.Competencies ?? Enumerable.Empty<int>())
                .WithRightAwnser(dto.RightAwnser)
                .WithOptions((dto.Options ?? Enumerable.Empty<DtoAnswerOption>()).Select(o => new DomainAnswerOption(o.Id, o.Text)));

            if (dto.MultiMediaRessorces != null)
            {
                builder.WithMediaResources(dto.MultiMediaRessorces.Select(m => new DomainMedia
                {
                    Caption = m.Caption,
                    Type = m.Type,
                    URL = m.URL
                }));
            }

            return builder.Build();
        }

        public DtoQuestion ToDto(DomainQuestion domain)
        {
            ArgumentNullException.ThrowIfNull(domain);

            return new DtoQuestion
            {
                Id = domain.Id,
                Description = domain.Description,
                Competencies = domain.Competencies,
                RightAwnser = domain.RightAwnser,
                Options = domain.Options.Select(o => new DtoAnswerOption { Id = o.Id, Text = o.Text }).ToList(),
                MultiMediaRessorces = domain.MediaResources?.Select(r => new DtoMedia
                {
                    Caption = r.Caption,
                    URL = r.URL,
                    Type = r.Type
                }).ToList()
            };
        }
    }
}
