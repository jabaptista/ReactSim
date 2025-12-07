using ReactSim.Domain.Model;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Data;
using System.Resources;

namespace ReactSim.Repositories
{
    public class QuestionRepository : IQuestionRepository
    {
        private readonly IMongoDbRepository mongoDbRepository;

        public QuestionRepository(IMongoDbRepository mongoDbRepository)
        {
            this.mongoDbRepository = mongoDbRepository;
        }

        public Task CreateAsync(Question question)
        {
            var dbo = new dbo.Question()
            {
                Id = question.Id,
                Description = question.Description,
                Competencies = question.Competencies,
                MediaResources = question.MediaResources?.Select(resource => new dbo.MultiMediaResource() { Caption = resource.Caption, URL = resource.URL, Type = resource.Type}),
                RightAwnser = question.RightAwnser,
                Options = question.Options.Select(q => new dbo.AwnserOption { Id = q.Id, Text = q.Text })
            };

            mongoDbRepository.AddOneAsync(dbo);

            return Task.CompletedTask;
        }

        public Task<IEnumerable<Question>> GetAllAsync()
        {
            return mongoDbRepository
                .GetAllAsync<dbo.Question>()
                .ContinueWith(previousTask =>
            {
                return previousTask.Result.Select(dbo => Question.Builder()
                    .WithId(dbo.Id.AsInt32)
                    .WithDescription(dbo.Description)
                    .WithCompetencies(dbo.Competencies)
                    .WithMediaResources(dbo.MediaResources?.Select(resource=> new MultiMediaResource() { Caption = resource.Caption, Type = resource.Type, URL = resource.URL } ))
                    .WithRightAwnser(dbo.RightAwnser)
                    .WithOptions(dbo.Options.Select(o => new AwnserOption(o.Id, o.Text)))
                    .Build()
                );

            }, CancellationToken.None);


        }
    }
}
