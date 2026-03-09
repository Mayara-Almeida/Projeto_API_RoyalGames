using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RoyalGames.Applications.Services;
using RoyalGames.DTOs.AutenticacaoDto;
using RoyalGames.DTOs.JogoDto;
using RoyalGames.Exceptions;
using System.Security.Claims;

namespace RoyalGames.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class JogoController : ControllerBase
    {
        private readonly JogoService _service;

        public JogoController(JogoService service)
        {
            _service = service;
        }

        // Autenticação do usuário
        private int ObterUsuarioIdLogado()
        {
            // Busca no token/claims o valor armazenado como id do usuário
            string? idTexto = User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (string.IsNullOrWhiteSpace(idTexto))
            {
                throw new DomainException("Usuário não autenticado.");
            }

            // Converter o ID que veio com o texto para inteiro, pois o usuárioID no sistema está armazenado como int
            return int.Parse(idTexto);
        }

        // Lista de todos os jogos
        [HttpGet]
        public ActionResult<List<LerJogoDto>> Listar()
        {
            List<LerJogoDto> jogos = _service.Listar();
            return Ok(jogos);
        }

        // Informações de somente um jogo pelo id
        [HttpGet("{id}")]
        public ActionResult<LerJogoDto> ObterPorId(int id)
        {
            try
            {
                LerJogoDto jogo = _service.ObterPorId(id);
                return Ok(jogo);
            }
            catch (DomainException ex)
            {
                return NotFound(ex.Message);
            }
        }

        // Imagem relacionada a tal produto pelo id
        [HttpGet("{id}/imagem")]
        public ActionResult ObterPorImagem(int id)
        {
            try
            {
                var imagem = _service.ObterPorImagem(id);
                // Retorna o arquivo para o navegador renderizar imagem
                return File(imagem, "image/jpeg");
            }
            catch (DomainException ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpPost]
        [Consumes("multipart/form-data")]
        [Authorize]

        // [FromForm] -> diz que os dados vem do formulário da requisição (multipart/form-data)
        public ActionResult Adicionar([FromForm] CriarJogoDto jogoDto)
        {
            try
            {
                int usuarioId = ObterUsuarioIdLogado();
                _service.Adicionar(jogoDto, usuarioId);
                return StatusCode(201);
            }
            catch (DomainException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("{id}")]
        [Consumes("multipart/form-data")]
        [Authorize]
        public ActionResult Atuzalizar(int id, [FromForm] AtualizarJogoDto jogoDto)
        {
            try
            {
                _service.Atualizar(id, jogoDto);
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
