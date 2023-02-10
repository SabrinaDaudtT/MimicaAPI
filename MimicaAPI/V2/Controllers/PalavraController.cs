using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MimicaAPI.V2.Controllers
{
    [ApiController]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiVersion("2.0")]
    public class PalavraController : ControllerBase
    {
        [HttpGet("", Name = "ObterTodos")]
        public string ObterTodos()
        {
            return "Versão 2.0";
        }
    }
}
