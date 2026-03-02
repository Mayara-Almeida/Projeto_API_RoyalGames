using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RoyalGames.Applications.Services;
using RoyalGames.DTOs.UsuarioDto;
using RoyalGames.Exceptions;

namespace RoyalGames.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuarioController : ControllerBase
    {
        private readonly UsuarioService _service;

        public UsuarioController(UsuarioService service)
        {
            _service = service;
        }

        // Lista de todos os usuários
        [HttpGet]
        public ActionResult<List<LerUsuarioDto>> Listar()
        {
            List<LerUsuarioDto> usuarios = _service.Listar();
            return Ok(usuarios);
        }

        // Lista de apenas um usuário buscado pelo id
        [HttpGet("{id}")]
        public ActionResult<LerUsuarioDto> ObterPorId(int id)
        {
            try
            {
                LerUsuarioDto usuario = _service.ObterPorId(id);
                return Ok(usuario);
            }
            catch (DomainException ex)
            {
                return NotFound(ex.Message);
            }
        }

        // Lista do usuário buscado pelo e-mail
        [HttpGet("email/{email}")]
        public ActionResult<LerUsuarioDto> ObterPorEmail(string email)
        {
            try
            {
                LerUsuarioDto usuarioDto = _service.ObterPorEmail(email);
                return Ok(usuarioDto);
            }
            catch (DomainException ex)
            {
                return NotFound(ex.Message); 
            }
        }

        // Adicionar novo usuário
        [HttpPost]
        public ActionResult<LerUsuarioDto> Adicionar(CriarUsuarioDto usuarioDto)
        {
            try
            {
                LerUsuarioDto usuarioNovo = _service.Adicionar(usuarioDto);
                return StatusCode(201, usuarioNovo);
            }
            catch (DomainException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // Atualizar novo usuário com id
        [HttpPut("{id}")]
        public ActionResult<LerUsuarioDto> Atualizar(int id, CriarUsuarioDto usuarioDto)
        {
            try
            {
                LerUsuarioDto usuarioAtualizado = _service.Atualizar(id, usuarioDto);
                return StatusCode(200, usuarioAtualizado);
            }
            catch(DomainException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // Inativar usuário
        [HttpDelete("{id}")]
        public ActionResult Remover(int id)
        {
            try
            {
                _service.Remover(id);
                return NoContent(); // Não retorna nada
            }
            catch (DomainException ex)
            {
                return BadRequest(ex.Message); 
            }
        }
    }
}
