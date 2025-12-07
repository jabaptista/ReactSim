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



            //var questions = new List<ActivityQuestion>
            //{
            //    new ActivityQuestion
            //    {
            //        id = "q1",
            //        title = "Cenário Situacional 1",
            //        description = "Incêndio urbano num 3º andar de um edifício antigo no centro histórico...",
            //        media = new ActivityMedia { type = "image", url = "https://images.unsplash.com/photo-1577962917302-cd874c4e31d2?w=800", caption = "Exemplo de incêndio urbano em edifício antigo" },
            //        options = new List<ActivityOption>
            //        {
            //            new ActivityOption { id = "opt1", text = "Ordenar entrada imediata pela escada com linha de ataque, ignorando a ventilação." },
            //            new ActivityOption { id = "opt2", text = "Estabelecer comando, solicitar auto-escada para salvamento no 4º andar e iniciar combate defensivo." },
            //            new ActivityOption { id = "opt3", text = "Dividir a equipa: binómio para busca e salvamento imediato (4º andar) e binómio para contenção do fogo no 3º andar." },
            //            new ActivityOption { id = "opt4", text = "Aguardar reforços antes de qualquer atuação." }
            //        }
            //    },
            //    new ActivityQuestion
            //    {
            //        id = "q2",
            //        title = "Cenário Situacional 2",
            //        description = "Acidente rodoviário com veículo pesado transportando materiais perigosos...",
            //        media = new ActivityMedia { type = "video", url = "https://www.w3schools.com/html/mov_bbb.mp4", caption = "Simulação de acidente com materiais perigosos" },
            //        options = new List<ActivityOption>
            //        {cah
            //            new ActivityOption { id = "opt1", text = "Aproximar-se imediatamente para iniciar desencarceramento das vítimas." },
            //            new ActivityOption { id = "opt2", text = "Estabelecer perímetro de segurança, identificar o produto, solicitar GIPS e gerir o trânsito." },
            //            new ActivityOption { id = "opt3", text = "Fazer arrefecimento do tanque com água enquanto se procede ao desencarceramento." },
            //            new ActivityOption { id = "opt4", text = "Evacuar apenas as vítimas que conseguem sair autonomamente e aguardar." }
            //        }
            //    }


            var processUrl = Url.Action(
                      action: nameof(ProcessActivity),
                      controller: "Activity",
                      values: new { id },
                      protocol: Request?.Scheme
                  ) ?? $"{Request?.Scheme}://{Environment.GetEnvironmentVariable("EXECUTION_PATH")}/api/Activity/{id}/process";

            return Ok(processUrl);
        }


        [HttpPost("{id:int}/process")]
        public IActionResult ProcessActivity([FromRoute] int id, [FromBody] DeployActivityRequest deployActivityRequest)
        {
            var challangeUrl = $"{Environment.GetEnvironmentVariable("EXECUTION_PATH")}/challange.html?activityID={deployActivityRequest.activityID}&InvenRAstdID={deployActivityRequest.InventRAstdID}";

            return Ok(challangeUrl);
        }
    }
}
