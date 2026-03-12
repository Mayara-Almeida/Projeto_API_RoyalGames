    using RoyalGames.Applications.Regras;
using RoyalGames.Domains;
using RoyalGames.DTOs.PlataformaDto;
using RoyalGames.Exceptions;
using RoyalGames.Interfaces;

namespace RoyalGames.Applications.Services
{
    public class PlataformaService
    {
        private readonly IPlataformaRepository _repository;

        public PlataformaService(IPlataformaRepository repository)
        {
            _repository = repository;
        }

        public List<LerPlataformaDto> Listar()
        {
            List<Plataforma> plataformas = _repository.Listar();

            // Converter a Lista para Dto de leitura
            List<LerPlataformaDto> plataformaDto = plataformas.Select(plataforma => new LerPlataformaDto
            {
                PlataformaID = plataforma.PlataformaID, 
                Nome = plataforma.Nome
            }).ToList();

            return plataformaDto;
        }

        public LerPlataformaDto ObterporId(int id)
        {
            Plataforma plataforma = _repository.ObterPorId(id);

            if( plataforma == null )
            {
                throw new DomainException("Plataforma não encontrada.");
            }

            LerPlataformaDto plataformaDto = new LerPlataformaDto
            {
                PlataformaID = plataforma.PlataformaID,
                Nome = plataforma.Nome
            };

            return plataformaDto;
        }

        public void Adicionar(CriarPlataformaDto criarDto)
        {
            ValidacaoNome.ValidarNome(criarDto.Nome);

            if(_repository.NomeExiste(criarDto.Nome))
            {
                throw new DomainException("Plataforma já existe.");
            }

            Plataforma plataforma = new Plataforma
            {
                Nome = criarDto.Nome
            };

            _repository.Adicionar(plataforma);
        }

        public void Atualizar(int id, CriarPlataformaDto criarDto)
        {
             

            // Consulta para ver se existe uma plataforma com esse id
            Plataforma plataformaBanco = _repository.ObterPorId(id);

            if(plataformaBanco == null)
            {
                throw new DomainException("Plataforma não encontrada.");
            }
            // plataformaIdAtual: id -> A plataformaIdAtual vai receber o id passado na requsição para realiar as validações necessárias
            if (_repository.NomeExiste(criarDto.Nome, plataformaIdAtual: id))
            {
                throw new DomainException("Já existe outra plataforma com esse nome.");
            }

            plataformaBanco.Nome = criarDto.Nome;
            _repository.Atualizar(plataformaBanco);
        }

        public void Remover(int id)
        {
            Plataforma plataformaBanco = _repository.ObterPorId(id);

            if (plataformaBanco == null)
            {
                throw new DomainException("Plataforma não encontrada.");
            }

            _repository.Remover(id);
        }
    }
}
