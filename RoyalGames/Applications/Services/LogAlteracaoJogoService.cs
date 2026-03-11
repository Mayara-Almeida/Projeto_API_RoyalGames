using RoyalGames.Domains;
using RoyalGames.DTOs.LogAlteracaoJogoDto;
using RoyalGames.Exceptions;
using RoyalGames.Interfaces;

namespace RoyalGames.Applications.Services
{
    public class LogAlteracaoJogoService
    {
        private readonly ILogAlteracaoJogoRepository _repository;

        public LogAlteracaoJogoService(ILogAlteracaoJogoRepository repository)
        {
            _repository = repository;
        }

        public List<LerLogJogoDto> Listar()
        {
            List<Log_AlteracaoJogo> logs = _repository.Listar();

            List<LerLogJogoDto> listaLogJogo = logs.Select(log => new LerLogJogoDto
            {
                LogId = log.Log_AlteracaJogoID,
                JogoId = log.JogoID,
                NomeAnterior = log.NomeAnterior,
                PrecoAnterior = log.Precoanterior,
                DataAlteracao = log.DataAlteracao
            }).ToList();

            return listaLogJogo;
        }

        public List<LerLogJogoDto> ListarPorJogo(int jogoId)
        {
            List<Log_AlteracaoJogo> logs = _repository.ListarPorJogo(jogoId);

            if(logs == null)
            {
                throw new DomainException("Jogo não encontrado.");
            }

            List<LerLogJogoDto> listaLogJogo = logs.Select(log => new LerLogJogoDto
            {
                LogId = log.Log_AlteracaJogoID,
                JogoId = log.JogoID,
                NomeAnterior = log.NomeAnterior,
                PrecoAnterior = log.Precoanterior,
                DataAlteracao = log.DataAlteracao
            }).ToList();

            return listaLogJogo;

        }
    }
}
