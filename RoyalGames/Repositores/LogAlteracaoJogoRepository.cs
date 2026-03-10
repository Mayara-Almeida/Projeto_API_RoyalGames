using RoyalGames.Contexts;
using RoyalGames.Domains;
using RoyalGames.Interfaces;

namespace RoyalGames.Repositores
{
    public class LogAlteracaoJogoRepository : ILogAlteracaoJogoRepository
    {
        private readonly RoyalGamesContext _context;

        public LogAlteracaoJogoRepository(RoyalGamesContext context)
        {
            _context = context;
        }

        public List<Log_AlteracaoJogo> Listar()
        {
            List<Log_AlteracaoJogo> log =
                _context.Log_AlteracaoJogo.OrderByDescending(l => l.DataAlteracao).ToList();

            return log;
        }

        public List<Log_AlteracaoJogo> ListarPorProduto(int jogoId)
        {
            List<Log_AlteracaoJogo> alteracoesJogo =
                _context.Log_AlteracaoJogo
                .Where(log => log.JogoID == jogoId)
                .OrderByDescending(log => log.DataAlteracao)
                .ToList();

            return alteracoesJogo;
        }
    }
}
