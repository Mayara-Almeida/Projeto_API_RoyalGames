using RoyalGames.Applications.Autenticacao;
using RoyalGames.Domains;
using RoyalGames.DTOs.AutenticacaoDto;
using RoyalGames.Exceptions;
using RoyalGames.Interfaces;


namespace RoyalGames.Applications.Services
{
    public class AutenticacaoService
    {
        private readonly IUsuarioRepository _repository;
        private readonly GeradorTokenJwt _tokenJwt;

        public AutenticacaoService(IUsuarioRepository repository, GeradorTokenJwt tokenJwt)
        {
            _repository = repository;
            _tokenJwt = tokenJwt; 
        }

        private static bool VerificarSenha(string senhaDigitada, byte[] senhaHashBanco)
        {
            using var sha = System.Security.Cryptography.SHA256.Create();
            var hashDigitado = sha.ComputeHash(System.Text.Encoding.UTF8.GetBytes(senhaDigitada));

            return hashDigitado.SequenceEqual(senhaHashBanco);
        }

        public TokenDto Login(LoginDto loginDto)
        {
            Usuario usuario = _repository.ObterPorEmail(loginDto.Email);

            if (usuario == null)
            {
                throw new DomainException("E-mail ou senha inválidos");
            }

            // comparar a senha digitada com a senha armazenada
            if (!VerificarSenha(loginDto.Senha, usuario.Senha))
            {
                throw new DomainException("E-mail ou senha inválidos");
            }

            // gerando o token
            var token = _tokenJwt.GerarToken(usuario);

            TokenDto novoToken = new TokenDto { Token = token };

            return novoToken;
        }

    }
}
