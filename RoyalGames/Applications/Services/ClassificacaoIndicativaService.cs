using RoyalGames.Applications.Regras;
using RoyalGames.Domains;
using RoyalGames.DTOs.ClassificacaoIndicativaDto;
using RoyalGames.Exceptions;
using RoyalGames.Interfaces;

namespace RoyalGames.Applications.Services
{
    public class ClassificacaoIndicativaService
    {
        private readonly IClassificacaoIndicativaRepository _repository;

        public ClassificacaoIndicativaService(IClassificacaoIndicativaRepository repository)
        {
            _repository = repository;
        }

        public List<LerClassificacaoIndicativaDto> Listar()
        {
            List<ClassificacaoIndicativa> classificacoes = _repository.Listar();

            List<LerClassificacaoIndicativaDto> classificacaoDto = classificacoes.Select(classificacao => new LerClassificacaoIndicativaDto
            {
                ClassificacaoIndicativaID = classificacao.ClassificacaoIndicativaID,
                Classificacao = classificacao.Classificacao
            }).ToList();

            return classificacaoDto;
        }

        public LerClassificacaoIndicativaDto ObterPorId(int id)
        {
            ClassificacaoIndicativa classificacaoIndicativa = _repository.ObterPorId(id);

            if(classificacaoIndicativa == null)
            {
                throw new DomainException("Classificação indicativa não encontrada.");
            }

            LerClassificacaoIndicativaDto classificacaoIndicativaDto = new LerClassificacaoIndicativaDto
            {
                ClassificacaoIndicativaID = classificacaoIndicativa.ClassificacaoIndicativaID,
                Classificacao = classificacaoIndicativa.Classificacao
            };

            return classificacaoIndicativaDto;
        }

        public void Adicionar(CriarClassificacaoIndicativaDto criarDto)
        {
            ValidacaoNome.ValidarClassificacao(criarDto.Classificacao);

            if(_repository.ClassificacaoExiste(criarDto.Classificacao))
            {
                throw new DomainException("Classificação indicativa já existe.");
            }

            ClassificacaoIndicativa classificacaoIndicativa = new ClassificacaoIndicativa
            {
                Classificacao = criarDto.Classificacao
            };

            _repository.Adicionar(classificacaoIndicativa);
        }

        public void Atualizar(int id, CriarClassificacaoIndicativaDto criarDto)
        {
            ValidacaoNome.ValidarClassificacao(criarDto.Classificacao);

            ClassificacaoIndicativa classificacaoIndicativaBanco = _repository.ObterPorId(id);

            if (classificacaoIndicativaBanco == null)
            {
                throw new DomainException("Classificação indicativa não encontrada.");
            }

            if (_repository.ClassificacaoExiste(criarDto.Classificacao, classificacaoIndicativaIdAtual: id))
            {
                throw new DomainException("Já existe outra classificação indicativa com essa idade.");
            }

            classificacaoIndicativaBanco.Classificacao = criarDto.Classificacao;
            _repository.Atualizar(classificacaoIndicativaBanco);
        }

        public void Remover(int id)
        {
            ClassificacaoIndicativa classificacaoIndicativaBanco = _repository.ObterPorId(id);

            if (classificacaoIndicativaBanco == null)
            {
                throw new DomainException("Classificação indicativa não encontrada.");
            }

            _repository.Remover(id);
        }
    }
}
