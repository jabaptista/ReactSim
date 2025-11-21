using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ReactSim.DTO;

namespace ReactSim.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ActivityController : ControllerBase
    {

        [HttpGet]
        public IActionResult GetActivities()
        {
            var config = new JsonConfigParameters
            {
                name = "Activity One",
                config_url = "https://example.com/config1",
                json_params_url = "https://example.com/params1",
                user_url = "https://example.com/user1",
                analytics_url = "https://example.com/analytics1",
                analytics_list_url= "https://example.com/analytics_list1"
            };

            return Ok(config);
        }

        [HttpPost]
        public IActionResult DeployActivity()
        {
            return Ok("Atividade deployed");
        }
    }
}
