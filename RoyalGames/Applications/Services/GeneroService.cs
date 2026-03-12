using RoyalGames.Domains;
using RoyalGames.DTOs.CategoriaDto;
using RoyalGames.Exceptions;
using RoyalGames.Interfaces;

namespace RoyalGames.Applications.Services
{
    public class GeneroService
    {
        private readonly IGeneroRepository _repository;

        public GeneroService(IGeneroRepository repository)
        {
            _repository = repository;
        }

        public List<LerGeneroDto> Listar()
        {
            List<Genero> generos = _repository.Listar();

            // converte cada categoria para LerCategoriaDto
            List<LerGeneroDto> genero = generos.Select(genero => new LerGeneroDto
            {
                GeneroID = genero.GeneroID,
                Nome = genero.Nome
            }).ToList();

            // Retorna a lista já convertida em DTO
            return genero;
        }

        public LerGeneroDto ObterPorId(int id)
        {
            Genero genero = _repository.ObterPorId(id);

            if (genero == null)
            {
                throw new DomainException("Gênero não encontrado.");
            }

            LerGeneroDto generoDto = new LerGeneroDto
            {
                GeneroID = genero.GeneroID,
                Nome = genero.Nome
            };

            return generoDto;
        }

        private static void ValidarNome(string nome)
        {
            if (string.IsNullOrWhiteSpace(nome))
            {
                throw new DomainException("Nome é obrigatório.");
            }
        }

        public void Adicionar(CriarGeneroDto criarDto)
        {
            ValidarNome(criarDto.Nome);

            if (_repository.NomeExiste(criarDto.Nome))
            {
                throw new DomainException("Categoria já existente.");
            }

            Genero genero = new Genero
            {
                Nome = criarDto.Nome,
            };

            _repository.Adicionar(genero);
        }

        public void Atualizar(int id, CriarGeneroDto criarDto)
        {
            ValidarNome(criarDto.Nome); // valida se o campo nome foi preenchido

            Genero GeneroBanco= _repository.ObterPorId(id);

            if (GeneroBanco == null)
            {
                throw new DomainException("Genero não encontrado.");
            }

            // categoriaIdAtual: id -> categoriaIdAtual recebe id
            if (_repository.NomeExiste(criarDto.Nome, generoIdAtual: id))
            {
                throw new DomainException("Já existe outro genero com esse nome.");
            }

            GeneroBanco.Nome = criarDto.Nome;
            _repository.Atualizar(GeneroBanco);
        }

        public void Remover(int id)
        {
            Genero GeneroBanco = _repository.ObterPorId(id);

            if (GeneroBanco == null)
            {
                throw new DomainException("Genero não encontrado.");
            }

            _repository.Remover(id);
        }

    }
}
