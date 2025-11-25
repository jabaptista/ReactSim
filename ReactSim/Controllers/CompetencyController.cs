using Microsoft.AspNetCore.Mvc;
using ReactSim.DTO.Competency;

namespace ReactSim.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CompetencyController : Controller
    {

        [HttpGet]
        public List<Competency> Index()
        {
            return new List<Competency>
            {
                new Competency { Id = 1, Name = "Gestão de Recursos e Equipamentos", Description = "Gestão de Recursos e Equipamentos", Color = "#FF5733" },
                new Competency { Id = 2, Name = "Planeamento e Organização", Description = "Planeamento e Organização", Color = "#33FF57" },
                new Competency { Id = 3, Name = "Liderança sob Pressão e Comunicações", Description = "Liderança sob Pressão e Comunicações", Color = "#3357FF" },
                new Competency { Id = 4, Name = "Tomada de Decisão em Situações Críticas", Description = "Tomada de Decisão em Situações Críticas", Color = "#3357FF" },
                new Competency { Id = 5, Name = "Trabalho em Equipa", Description = "Trabalho em Equipa", Color = "#12CD12" },
            };
        }
    }
}
