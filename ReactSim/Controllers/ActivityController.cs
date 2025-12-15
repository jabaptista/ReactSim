using AutoFixture;
using Microsoft.AspNetCore.Mvc;
using ReactSim.DTO.Activity;
using System.Runtime.ConstrainedExecution;

namespace ReactSim.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ActivityController : ControllerBase
    {
        public ActivityController()
        {
        }

        [HttpPost("{id:int}")]
        public IActionResult DeployActivity([FromRoute] int id)
        {
            var processUrl = $"{Environment.GetEnvironmentVariable("EXECUTION_PATH")}api/Activity/{id}/process";

            return Ok(processUrl);
        }


        [HttpPost("{id:int}/process")]
        public IActionResult ProcessActivity([FromRoute] int id, [FromBody] DeployActivityRequest deployActivityRequest)
        {
            var challangeUrl = $"{Environment.GetEnvironmentVariable("EXECUTION_PATH")}challange.html?activityID={deployActivityRequest.activityID}&InvenRAstdID={deployActivityRequest.InventRAstdID}";

            return Ok(challangeUrl);
        }
    }
}
