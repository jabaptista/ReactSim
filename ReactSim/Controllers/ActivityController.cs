using AutoFixture;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ReactSim.DTO;
using ReactSim.DTO.Activity;

namespace ReactSim.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ActivityController : ControllerBase
    {
        private readonly IFixture fixture;
        public ActivityController()
        {
           this.fixture = new Fixture();
        }

        [HttpGet]
        public IActionResult GetActivities()
        {
            var config = this.fixture.Create<JsonConfigParameters>();

            return Ok(config);
        }

        [HttpPost]
        public IActionResult DeployActivity(DeployActivityRequest deployActivityDto)
        {
            var response = this.fixture.Create<DeployActivityResponse>();
            return Ok(response);
        }
    }
}
