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

        [HttpPost("{activityId}")]
        public async Task<IActionResult> DeployActivity([FromRoute] string activityId)
         {
            await activityService.DeployAsync(activityId).ConfigureAwait(false);
            var processUrl = $"{Environment.GetEnvironmentVariable("EXECUTION_PATH")}/api/Activity/{activityId}/process";

            return Ok(processUrl);
        }


        [HttpPost("{activityId}/process")]
        public async Task<IActionResult> ProcessActivity([FromRoute] string activityId, [FromBody] DeployActivityRequest deployActivityRequest)
        {
            await activityService.RegisterStartAsync(activityId, deployActivityRequest.InventRAstdID).ConfigureAwait(false);
            var challangeUrl = $"{Environment.GetEnvironmentVariable("EXECUTION_PATH")}/challange.html?activityID={deployActivityRequest.activityID}&InvenRAstdID={deployActivityRequest.InventRAstdID}";

            return Ok(challangeUrl);
        }
    }
}
