using ReactSim.DTO.Questions;
using System.Collections.Generic;
using System.Linq;

namespace ReactSim.Domain.Model
{
    public class Question
    {
        public int Id { get; }
        public string Description { get; }
        public IEnumerable<int> Competencies { get; }

        public IEnumerable<AwnserOption> Options { get; }

        public IEnumerable<MultiMediaResource>? MediaResources { get; }

        public int RightAwnser { get; }

        public static QuestionBuilder Builder() => new QuestionBuilder();

        private Question()
        {
            Description = string.Empty;
            Competencies = new List<int>();
            Options = new List<AwnserOption>();
        }

        internal Question(int id, string description, IEnumerable<int> competencies, IEnumerable<AwnserOption> options, IEnumerable<MultiMediaResource>? mediaResources, int rightAwnser)
        {
            Id = id;
            Description = description ?? string.Empty;
            Competencies = competencies ?? new List<int>();
            Options = options ?? new List<AwnserOption>();
            MediaResources = mediaResources;
            RightAwnser = rightAwnser;
        }
    }

    public class QuestionBuilder
    {
        private int id;
        private string description = string.Empty;
        private readonly List<int> competencies = new();
        private readonly List<AwnserOption> options = new();
        private readonly List<MultiMediaResource> media = new();
        private int rightAwnser;

        public QuestionBuilder WithId(int id)
        {
            this.id = id;
            return this;
        }

        public QuestionBuilder WithDescription(string description)
        {
            this.description = description ?? string.Empty;
            return this;
        }

        public QuestionBuilder WithCompetencies(IEnumerable<int> competencies)
        {
            this.competencies.Clear();
            if (competencies != null)
                this.competencies.AddRange(competencies);
            return this;
        }

        public QuestionBuilder AddCompetency(int competency)
        {
            this.competencies.Add(competency);
            return this;
        }

        public QuestionBuilder WithOptions(IEnumerable<AwnserOption> options)
        {
            this.options.Clear();
            if (options != null)
                this.options.AddRange(options);
            return this;
        }

        public QuestionBuilder AddOption(AwnserOption option)
        {
            if (option != null)
                this.options.Add(option);
            return this;
        }

        public QuestionBuilder WithMediaResources(IEnumerable<MultiMediaResource> mediaResources)
        {
            this.media.Clear();
            if (mediaResources != null)
                media.AddRange(mediaResources.Select(m => new MultiMediaResource() {URL = m.URL, Caption = m.Caption, Type = m.Type }));
            return this;
        }

        public QuestionBuilder AddMediaResource(MultiMediaResource resource)
        {
            this.media.Add(resource);
            return this;
        }

        public QuestionBuilder WithRightAwnser(int rightAwnser)
        {
            this.rightAwnser = rightAwnser;
            return this;
        }

        public Question Build()
        {
            return new Question(
                id,
                description,
                competencies.ToList(),
                options.ToList(),
                media.Count > 0 ? media.ToList() : null,
                rightAwnser
            );
        }
    }
}
