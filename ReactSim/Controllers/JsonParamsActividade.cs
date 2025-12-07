using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace ReactSim.Controllers
{

    [Route("api/json-params-actividade")]
    [ApiController]
    public class JsonParamsActividadeController : Controller
    {
        [HttpGet]
        public IActionResult Index()
        {
            var parms = new List<Dictionary<string, string>>
            {
                new Dictionary<string, string>
                {
                    { "resumo", "Descrição da simulação." },
                    { "type", "text/plain" }
                },
                new Dictionary<string, string>
                {
                    { "categoria", "Categoria a que se destina" },
                    { "type", "text/plain" }
                },
                new Dictionary<string, string>
                {
                    { "nivel", "Nível de dificuldade" },
                    { "type", "text/plain" }
                }
            };

            return Ok(parms);
        }
    }
}
