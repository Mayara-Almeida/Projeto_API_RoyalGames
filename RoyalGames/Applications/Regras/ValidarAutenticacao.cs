using RoyalGames.Exceptions;

namespace RoyalGames.Applications.Regras
{
    public class ValidarAutenticacao
    {
        public static void ValidarAutenticacaoLogin(int usuarioId)
        {
            if (usuarioId == null)
            {
                throw new DomainException("Usuário não autenticado.");
            }
        }
    }
}
