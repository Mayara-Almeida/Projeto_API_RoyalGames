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

        // compara a hash SHA256 para validar a senha
        private static bool VerificarSenha(string senhaDigitada, byte[] senhaHashBanco)
        {
            // Transformar a senha digitada em hash
            using var sha = System.Security.Cryptography.SHA256.Create();
            var hashDigitado = sha.ComputeHash(System.Text.Encoding.UTF8.GetBytes(senhaDigitada));

            // Comparar com a senha do senhaHashBanco 
            return hashDigitado.SequenceEqual(senhaHashBanco);
        }

        public TokenDto Login(LoginDto loginDto)
        {
            // Comparar e-mail digitado com e-mail armazenado no banco
            Usuario usuario = _repository.ObterPorEmail(loginDto.Email);

            if(usuario == null)
            {
                throw new DomainException("E-mail ou senha inválidos.");
            }

            // Comparar senha digitada com senha armazenada no banco
            if (!VerificarSenha(loginDto.Senha, usuario.Senha))
            {
                throw new DomainException("E-mail ou senha inválidos.");
            }

            if (usuario.StatusUsuario == false)
            {
                throw new DomainException("Usuário está inativado.");
            }

            // Gerando o token 
            var token = _tokenJwt.GerarToken(usuario);

            TokenDto novoToken = new TokenDto { Token = token };

            return novoToken;  
        }
    }
}
