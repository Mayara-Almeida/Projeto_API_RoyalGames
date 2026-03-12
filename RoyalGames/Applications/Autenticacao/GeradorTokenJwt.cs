using Microsoft.IdentityModel.Tokens;
using RoyalGames.Domains;
using RoyalGames.Exceptions;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
<<<<<<< HEAD
using RoyalGames.Domains;
using RoyalGames.Exceptions;
=======
>>>>>>> develop

namespace RoyalGames.Applications.Autenticacao
{
    public class GeradorTokenJwt
    {
        private readonly IConfiguration _config;

        public GeradorTokenJwt(IConfiguration config)
        {
            _config = config;
        }

        public string GerarToken(Usuario usuario)
        {
            var chave = _config["Jwt:Key"]!; // Chave para assinar o token

            var issuer = _config["Jwt:Issuer"]!; // Quem gerou o token

            var audience = _config["Jwt:Audience"]!; // Para quem gerou o token

            var expiraEmMinutos = int.Parse(_config["Jwt:ExpiraEmMinutos"]!); // Define por quanto tempo o token estará válido

            var keyBytes = Encoding.UTF8.GetBytes(chave); // Converte chave para bytes

            if (keyBytes.Length < 32) // Exige chave com no mínimo 32 caracteres, para aumentar segurança
            {
                throw new DomainException("Jwt: Key precisa ter pelo menos 32 caracteres (256 biits).");
            }

            var securityKey = new SymmetricSecurityKey(keyBytes); // Chave de segurança usado para assinar o token

            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256); // Algoritmo de assinatura

            // Claims -> informações do usuário que vão dentro do token
            var claims = new List<Claim>
           {
               new Claim (ClaimTypes.NameIdentifier, usuario.UsuarioID.ToString()), // Id do usuário

               new Claim(ClaimTypes.Name, usuario.Nome), // Nome do usuário

               new Claim(ClaimTypes.Email, usuario.Email) // E-mail do usuário
           };

            // Cria o token Jwt com todas as informações 
            var token = new JwtSecurityToken(
                issuer: issuer,                                         // Quem gerou o token
                audience: audience,                                     // Quem pode usar o token
                claims: claims,                                         // Dados do usuário
                expires: DateTime.Now.AddMinutes(expiraEmMinutos),      // Validade do token
                signingCredentials: credentials                         // Assinatura de segurança
            );

            // Converte o token para string e essa string é enviada para o cliente
            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
