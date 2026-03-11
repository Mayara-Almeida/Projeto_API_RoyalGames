using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RoyalGames.Applications.Services;
using RoyalGames.DTOs.JogoDto;
using RoyalGames.DTOs.LogAlteracaoJogoDto;
using RoyalGames.Exceptions;

namespace RoyalGames.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LogAlteracaoJogoController : ControllerBase
    {
        private readonly LogAlteracaoJogoService _service;

        public LogAlteracaoJogoController(LogAlteracaoJogoService service)
        {
            _service = service;
        }

        [HttpGet]
        public ActionResult Listar()
        {
            return Ok(_service.Listar());
        }

        [HttpGet("Produto/{id}")]
        public ActionResult ListarPorJogo(int id)
        {
            return Ok(_service.ListarPorJogo(id));
        }
    }
    
}
