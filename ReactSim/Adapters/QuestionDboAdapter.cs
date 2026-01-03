using System;
using System.Collections.Generic;
using System.Linq;
using DomainAnswerOption = ReactSim.Domain.Model.AwnserOption;
using DomainMedia = ReactSim.Domain.Model.MultiMediaResource;
using DomainQuestion = ReactSim.Domain.Model.Question;
using DboAnswerOption = ReactSim.Repositories.dbo.AwnserOption;
using DboMedia = ReactSim.Repositories.dbo.MultiMediaResource;
using DboQuestion = ReactSim.Repositories.dbo.Question;

namespace ReactSim.Adapters
{
    public class QuestionDboAdapter : IQuestionDboAdapter
    {
        public DboQuestion ToDbo(DomainQuestion domain)
        {
            ArgumentNullException.ThrowIfNull(domain);

            var dbo = new DboQuestion
            {
                Description = domain.Description,
                Competencies = domain.Competencies?.ToList() ?? new List<int>(),
                Options = domain.Options.Select(o => new DboAnswerOption
                {
                    Id = o.Id,
                    Text = o.Text
                }).ToList(),
                MediaResources = domain.MediaResources?.Select(m => new DboMedia
                {
                    Caption = m.Caption,
                    URL = m.URL,
                    Type = m.Type
                }).ToList(),
                RightAwnser = domain.RightAwnser
            };

            if (domain.Id > 0)
            {
                dbo.SetInt32Id(domain.Id);
            }

            return dbo;
        }

        public DomainQuestion FromDbo(DboQuestion dbo)
        {
            ArgumentNullException.ThrowIfNull(dbo);

            var builder = DomainQuestion.Builder()
                .WithId(dbo.GetInt32Id() ?? 0)
                .WithDescription(dbo.Description)
                .WithCompetencies(dbo.Competencies ?? Enumerable.Empty<int>())
                .WithRightAwnser(dbo.RightAwnser)
                .WithOptions((dbo.Options ?? Enumerable.Empty<DboAnswerOption>()).Select(o => new DomainAnswerOption(o.Id, o.Text)));

            if (dbo.MediaResources != null)
            {
                builder.WithMediaResources(dbo.MediaResources.Select(r => new DomainMedia
                {
                    Caption = r.Caption,
                    Type = r.Type,
                    URL = r.URL
                }));
            }

            return builder.Build();
        }
    }
}
