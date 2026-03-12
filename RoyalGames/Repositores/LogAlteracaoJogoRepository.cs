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

        public bool JogoExiste(int jogoId)
        {
            return _context.Jogo.Any(j => j.JogoID == jogoId);
        }

        public List<Log_AlteracaoJogo> Listar()
        {
            List<Log_AlteracaoJogo> log =
                _context.Log_AlteracaoJogo.OrderByDescending(l => l.DataAlteracao).ToList();

            return log;
        }

        public List<Log_AlteracaoJogo> ListarPorJogo(int jogoId)
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
