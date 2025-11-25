using Microsoft.AspNetCore.Mvc;
using ReactSim.DTO.Questions;

namespace ReactSim.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class QuestionController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(FormCreationRequest formCreationRequest)
        {
            return Ok();
        }
    }
}
