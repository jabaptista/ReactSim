using Microsoft.AspNetCore.Mvc;
using ReactSim.DTO.Competency;
using ReactSim.Services;

namespace ReactSim.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class CompetencyController : ControllerBase
    {
        private readonly ICompetenciesService competenciesService;

        public CompetencyController(ICompetenciesService competenciesService)
        {
            this.competenciesService = competenciesService;
        }

        [HttpGet]
        public ActionResult<IEnumerable<Competency>> Get()
        {
            var competencies = competenciesService.GetAllCompetencies();
            var result = competencies.Select(x => new Competency() { Id = x.Id, Name = x.Name, Description = x.Description, Color = x.Color });
            return Ok(result);
        }
    }
}
