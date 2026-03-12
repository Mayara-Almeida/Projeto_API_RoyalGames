using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RoyalGames.Applications.Services;
using RoyalGames.DTOs.AutenticacaoDto;
using RoyalGames.Exceptions;

namespace RoyalGames.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AutenticacaoController : ControllerBase
    {
        private readonly AutenticacaoService _service;

        public AutenticacaoController(AutenticacaoService service)
        {
            _service = service;
        }

<<<<<<< HEAD
        [HttpPost("login")]
=======
        [HttpPost]
>>>>>>> develop
        public ActionResult<TokenDto> Login(LoginDto loginDto)
        {
            try
            {
                var token = _service.Login(loginDto);
<<<<<<< HEAD

=======
>>>>>>> develop
                return Ok(token);
            }
            catch (DomainException ex)
            {
<<<<<<< HEAD

=======
>>>>>>> develop
                return BadRequest(ex.Message);
            }
        }
    }
}