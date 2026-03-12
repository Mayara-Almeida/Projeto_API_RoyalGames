using RoyalGames.Applications.Conversoes;
using RoyalGames.Applications.Regras;
using RoyalGames.Domains;
using RoyalGames.DTOs.AutenticacaoDto;
using RoyalGames.DTOs.JogoDto;
using RoyalGames.Exceptions;
using RoyalGames.Interfaces;
using System.Security.Claims;

namespace RoyalGames.Applications.Services
{
    public class JogoService
    {
        private readonly IJogoRepository _repository;
        public JogoService(IJogoRepository repository)
        {
            _repository = repository;
        }

        public List<LerJogoDto> Listar()
        {
            List<Jogo> jogos = _repository.Listar();

            // Converter a Lista em DTO - percorrer cada jogo no banco e converter em Dto
            List<LerJogoDto> jogosDto =
                jogos.Select(JogoParaDto.ConverterParaDto). ToList();

            return jogosDto;
        }

        public LerJogoDto ObterPorId(int id)
        {
            Jogo? jogo = _repository.ObterPorId(id);

            if (jogo == null)
            {
                throw new DomainException("Jogo não encontrado.");
            }

            // Converte o jogo para DTO e devolve
            return JogoParaDto.ConverterParaDto(jogo);
        }

        public byte[] ObterPorImagem(int id)
        {
            byte[] imagem = _repository.ObterPorImagem(id); // Traz a imagem que está no banco

            if(imagem == null || imagem.Length == 0)
            {
                throw new DomainException("Imagem não encontrada.");
            }

            return imagem;
        }

        // Criar validações para cadastro de jogo
        private static void ValidarCadastro(CriarJogoDto jogoDto)
        {
            // Validar nome
            ValidacaoNome.ValidarNome(jogoDto.Nome);

            // Validar preço
            if(jogoDto.Preco < 0)
            {
                throw new DomainException("O preço deve ser maior que zero.");
            }

            // Validar descrição
            if(string.IsNullOrWhiteSpace(jogoDto.Descricao))
            {
                throw new DomainException("Descrição obrigatória.");
            }


            // Validar imagem
            if(jogoDto.Imagem == null || jogoDto.Imagem.Length == 0)
            {
                throw new DomainException("Imagem é obrigatória.");
            }

            // Validar Classificação indicativa
            if(jogoDto.ClassificacaoIndicativaID == null)
            {
                throw new DomainException("Classificação indicativa é obrigatória.");
            }

            // Validar Gênero
            if(jogoDto.GenerosIds == null || jogoDto.GenerosIds.Count == 0)
            {
                throw new DomainException("Jogo é necessário estar vinculado a no mínimo um gênero.");
            }

            // Validar Plataforma
            if(jogoDto.PlataformasIds == null || jogoDto.PlataformasIds.Count == 0)
            {
                throw new DomainException("Jogo é necessário estar vinculado a no mínimo uma plataforma.");
            }
        }

        public LerJogoDto Adicionar(CriarJogoDto jogoDto, int usuarioId)
        {
            ValidarCadastro(jogoDto);

            // Verificar se já existe um jogo com aquele nome
            if(_repository.NomeExiste(jogoDto.Nome))
            {
                throw new DomainException("Jogo já existente.");
            }

            // Criar jogo
            Jogo jogo = new Jogo
            {
                Nome = jogoDto.Nome,
                Preco = jogoDto.Preco,
                Descricao = jogoDto.Descricao,
                Imagem = ImagemParaBytes.ConverterImagem(jogoDto.Imagem),
                StatusJogo = true, // Ao criar sempre inicia como true
                UsuarioID = usuarioId,
                ClassificacaoIndicativaID = jogoDto.ClassificacaoIndicativaID
            };

            // Adicionar Classificação indicativa, gênero e plataforma ao jogo
            _repository.Adicionar(jogo, jogoDto.GenerosIds, jogoDto.PlataformasIds);

            return JogoParaDto.ConverterParaDto(jogo);
        }

        public LerJogoDto Atualizar(int id, AtualizarJogoDto jogoDto)
        {
            HorarioAlteracaoProduto.ValidarHorario();

            Jogo jogoBanco = _repository.ObterPorId(id); // Buscar o produto no banco

            if(jogoBanco == null)
            {
                throw new DomainException("Jogo não encontrado.");
            }

            if(_repository.NomeExiste(jogoDto.Nome, jogoIdAtual: id))
            {
                throw new DomainException("Já existe outro jogo com esse nome.");
            }

            if (jogoDto.Preco < 0)
            {
                throw new DomainException("O preço deve ser maior que zero.");
            }

            if (jogoDto.ClassificacaoIndicativaID == null)
            {
                throw new DomainException("Classificação indicativa é obrigatória.");
            }

            if (jogoDto.GenerosIds == null || jogoDto.GenerosIds.Count == 0)
            {
                throw new DomainException("Jogo é necessário estar vinculado a no mínimo um gênero.");
            }

            if (jogoDto.PlataformasIds == null || jogoDto.PlataformasIds.Count == 0)
            {
                throw new DomainException("Jogo é necessário estar vinculado a no mínimo uma plataforma.");
            }

            jogoBanco.Nome = jogoDto.Nome;
            jogoBanco.Preco = jogoDto.Preco;
            jogoBanco.Descricao = jogoDto.Descricao;
            jogoBanco.ClassificacaoIndicativaID = jogoDto.ClassificacaoIndicativaID;

            if(jogoDto.Imagem != null && jogoDto.Imagem.Length > 0)
            {
                jogoBanco.Imagem = ImagemParaBytes.ConverterImagem(jogoDto.Imagem);
            }

            if(jogoDto.StatusJogo.HasValue)
            {
                jogoBanco.StatusJogo = jogoDto.StatusJogo.Value;
            }

            _repository.Atualizar(jogoBanco, jogoDto.GenerosIds, jogoDto.PlataformasIds);
            return JogoParaDto.ConverterParaDto(jogoBanco);
        }

        public void Remover (int id)
        {
            HorarioAlteracaoProduto.ValidarHorario();

            Jogo jogo = _repository.ObterPorId(id);

            if(jogo == null)
            {
                throw new DomainException("Jogo não encontrado.");
            }

            _repository.Remover(id);
        }
    }
}
