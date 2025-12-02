namespace ReactSim.Services
{
    public class CompetenciesService : ICompetenciesService
    {
        public static CompetenciesService Instance { get; } = new CompetenciesService();

        private CompetenciesService() { }

        public IEnumerable<Domain.Model.Competency> GetAllCompetencies()
        {
            return new List<Domain.Model.Competency>
            {
                new Domain.Model.Competency { Id = 1, Name = "Gestão de Recursos e Equipamentos", Description = "Gestão de Recursos e Equipamentos", Color = "#FF5733" },
                new Domain.Model.Competency { Id = 2, Name = "Planeamento e Organização", Description = "Planeamento e Organização", Color = "#33FF57" },
                new Domain.Model.Competency { Id = 3, Name = "Liderança sob Pressão e Comunicações", Description = "Liderança sob Pressão e Comunicações", Color = "#3357FF" },
                new Domain.Model.Competency { Id = 4, Name = "Tomada de Decisão em Situações Críticas", Description = "Tomada de Decisão em Situações Críticas", Color = "#3357FF" },
                new Domain.Model.Competency { Id = 5, Name = "Trabalho em Equipa", Description = "Trabalho em Equipa", Color = "#12CD12" },
            };
        }
    }
}
