namespace ReactSim.Services
{
    public interface ICompetenciesService
    {
        public IEnumerable<Domain.Model.Competency> GetAllCompetencies();
    }
}
