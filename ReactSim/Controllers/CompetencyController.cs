using Microsoft.AspNetCore.Mvc;
using ReactSim.DTO.Competency;
using ReactSim.Services;

namespace ReactSim.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class CompetencyController : Controller
    {
        [HttpGet]
        public IEnumerable<Competency> Index()
        {
            var competencies = CompetenciesService.Instance.GetAllCompetencies();

            return competencies.Select(x => new Competency() { Id = x.Id, Name = x.Name, Description = x.Description, Color = x.Color });
        }
    }
}
