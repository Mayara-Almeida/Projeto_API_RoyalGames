using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RoyalGames.Applications.Services;
using RoyalGames.DTOs.ClassificacaoIndicativaDto;
using RoyalGames.Exceptions;

namespace RoyalGames.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClassificacaoIndicativaController : ControllerBase
    {
        private readonly ClassificacaoIndicativaService _service;

        public ClassificacaoIndicativaController(ClassificacaoIndicativaService service)
        {
            _service = service;
        }

        [HttpGet]
        public ActionResult<List<LerClassificacaoIndicativaDto>> Listar()
        {
            List<LerClassificacaoIndicativaDto> classificacoes = _service.Listar();
            return Ok(classificacoes);
        }

        [HttpGet("{id}")]
        public ActionResult<LerClassificacaoIndicativaDto> ObterPorId(int id)
        {
            try
            {
                LerClassificacaoIndicativaDto classificacao = _service.ObterPorId(id);
                return Ok(classificacao);
            }
            catch (DomainException ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpPost]
        [Authorize]
        public ActionResult Adicionar(CriarClassificacaoIndicativaDto criarDto)
        {
            try
            {
                _service.Adicionar(criarDto);
                return StatusCode(201);
            }
            catch (DomainException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("{id}")]
        [Authorize]
        public ActionResult Atualizar(int id, CriarClassificacaoIndicativaDto criarDto)
        {
            try
            {
                _service.Atualizar(id, criarDto);
                return NoContent();
            }
            catch (DomainException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("{id}")]
        [Authorize]
        public ActionResult Remover(int id)
        {
            try
            {
                _service.Remover(id);
                return NoContent();
            }
            catch (DomainException ex)
            {
                return BadRequest(ex.Message);
            }
        }

    }
}
