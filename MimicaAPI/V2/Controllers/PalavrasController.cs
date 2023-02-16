using Microsoft.AspNetCore.Mvc;

namespace MimicaAPI.V2.Controllers
{
    [ApiController]
    [Route("api/v{version:apiVersion}/[controller]")]
    //[Route("api/[controller]")]
    [ApiVersion("2.0")]
    public class PalavrasController : ControllerBase 
    {

        /// <summary>
        /// Operação que pega do banco de dados todas as palavras existentes.
        /// </summary>
        /// <param name="query">Filtros de pesquisa</param>
        /// <returns>Listagem de palavras</returns>
        [HttpGet("", Name = "ObterTodos")]
        public string ObterTodos()
        {
            return ("Versão 2.0");
        }
    }
}
