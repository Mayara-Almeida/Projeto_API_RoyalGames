using RoyalGames.Exceptions;

namespace RoyalGames.Applications.Regras
{
    public class ValidacaoNome
    {
        public static void ValidarNome(string nome)
        {
            if (string.IsNullOrWhiteSpace(nome))
            {
                throw new DomainException("Nome é obrigatório.");
            }
        }
    }
}
