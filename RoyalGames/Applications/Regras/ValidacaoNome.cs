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

        public static void ValidarClassificacao(string classificacao)
        {
            if (string.IsNullOrWhiteSpace(classificacao))
            {
                throw new DomainException("Classificação é obrigatória.");
            }
        }
    }
}
