using AutoFixture;
using Microsoft.AspNetCore.Mvc;
using ReactSim.DTO.Analytics;

namespace ReactSim.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AnalyticsController : Controller
    {
        private readonly IFixture fixture;

        public AnalyticsController()
        {
            this.fixture = new Fixture();
        }

        [HttpGet]
        public IActionResult GetAnalyticsList()
        {
            var result = this.fixture.CreateMany<AnalitysResponse>(5);

            return Ok(result);
        }

        [HttpPost]
        public IActionResult CreateAnalytics(AnalyticsDefinition analyticsDto)
        {
            var response = this.fixture.Create<AnalitysResponse>();
            return Ok(response);

        }
    }
}
