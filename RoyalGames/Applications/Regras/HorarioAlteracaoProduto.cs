using RoyalGames.Exceptions;

namespace RoyalGames.Applications.Regras
{
    public class HorarioAlteracaoProduto
    {
        public static void ValidarHorario()
        {
            var agora = DateTime.Now.TimeOfDay; // Pega horário atual
            var abertura = new TimeSpan(21, 0, 0); 
            var fechamento = new TimeSpan(23, 0, 0);

            // Verificar se o estabelecimento está aberto
            var estaAberto = agora >= abertura && agora <= fechamento;

            if(estaAberto) 
            {
                throw new DomainException("O jogo só pode ser alterado fora de horário de funcionamento do estabelecimento.");
            }
        }
    }
}
