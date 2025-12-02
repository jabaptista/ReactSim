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

        public IEnumerable<string>? MediaURL { get; }

        public int RightAwnser { get; }

        public static QuestionBuilder Builder() => new QuestionBuilder();

        private Question()
        {
            Description = string.Empty;
            Competencies = new List<int>();
            Options = new List<AwnserOption>();
        }

        internal Question(int id, string description, IEnumerable<int> competencies, IEnumerable<AwnserOption> options, IEnumerable<string>? mediaUrl, int rightAwnser)
        {
            Id = id;
            Description = description ?? string.Empty;
            Competencies = competencies ?? new List<int>();
            Options = options ?? new List<AwnserOption>();
            MediaURL = mediaUrl;
            RightAwnser = rightAwnser;
        }
    }

    public class QuestionBuilder
    {
        private int _id;
        private string _description = string.Empty;
        private readonly List<int> _competencies = new();
        private readonly List<AwnserOption> _options = new();
        private readonly List<string> _media = new();
        private int _rightAwnser;

        public QuestionBuilder WithId(int id)
        {
            _id = id;
            return this;
        }

        public QuestionBuilder WithDescription(string description)
        {
            _description = description ?? string.Empty;
            return this;
        }

        public QuestionBuilder WithCompetencies(IEnumerable<int> competencies)
        {
            _competencies.Clear();
            if (competencies != null)
                _competencies.AddRange(competencies);
            return this;
        }

        public QuestionBuilder AddCompetency(int competency)
        {
            _competencies.Add(competency);
            return this;
        }

        public QuestionBuilder WithOptions(IEnumerable<AwnserOption> options)
        {
            _options.Clear();
            if (options != null)
                _options.AddRange(options);
            return this;
        }

        public QuestionBuilder AddOption(AwnserOption option)
        {
            if (option != null)
                _options.Add(option);
            return this;
        }

        public QuestionBuilder WithMediaUrls(IEnumerable<string> mediaUrls)
        {
            _media.Clear();
            if (mediaUrls != null)
                _media.AddRange(mediaUrls.Where(u => !string.IsNullOrWhiteSpace(u)));
            return this;
        }

        public QuestionBuilder AddMediaUrl(string url)
        {
            if (!string.IsNullOrWhiteSpace(url))
                _media.Add(url);
            return this;
        }

        public QuestionBuilder WithRightAwnser(int rightAwnser)
        {
            _rightAwnser = rightAwnser;
            return this;
        }

        public Question Build()
        {
            return new Question(
                _id,
                _description,
                _competencies.ToList(),
                _options.ToList(),
                _media.Count > 0 ? _media.ToList() : null,
                _rightAwnser
            );
        }
    }
}
