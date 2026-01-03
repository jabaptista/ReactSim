using DomainQuestion = ReactSim.Domain.Model.Question;
using DboQuestion = ReactSim.Repositories.dbo.Question;

namespace ReactSim.Adapters
{
    public interface IQuestionDboAdapter
    {
        DboQuestion ToDbo(DomainQuestion domain);
        DomainQuestion FromDbo(DboQuestion dbo);
    }
}
