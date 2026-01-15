using AutoFixture;
using Microsoft.AspNetCore.Mvc;
using ReactSim.DTO.Activity;
using ReactSim.Services;
using System.Runtime.ConstrainedExecution;

namespace ReactSim.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ActivityController : ControllerBase
    {
        private readonly IActivityService activityService;

        public ActivityController(IActivityService activityService)
        {
            this.activityService = activityService;
        }

        [HttpPost("{id:int}")]
        public async Task<IActionResult> DeployActivity([FromRoute] int id)
         {
            await activityService.DeployAsync(id.ToString()).ConfigureAwait(false);
            var processUrl = $"{Environment.GetEnvironmentVariable("EXECUTION_PATH")}api/Activity/{id}/process";

            return Ok(processUrl);
        }


        [HttpPost("{id:int}/process")]
        public async Task<IActionResult> ProcessActivity([FromRoute] int id, [FromBody] DeployActivityRequest deployActivityRequest)
        {
            await activityService.RegisterStartAsync(id.ToString(), deployActivityRequest.InventRAstdID).ConfigureAwait(false);
            var challangeUrl = $"{Environment.GetEnvironmentVariable("EXECUTION_PATH")}challange.html?activityID={deployActivityRequest.activityID}&InvenRAstdID={deployActivityRequest.InventRAstdID}";

            return Ok(challangeUrl);
        }
    }
}
