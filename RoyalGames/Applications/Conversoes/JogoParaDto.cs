using RoyalGames.Domains;
using RoyalGames.DTOs.JogoDto;

namespace RoyalGames.Applications.Conversoes
{
    public class JogoParaDto
    {
        public static LerJogoDto ConverterParaDto(Jogo jogo)
        {
            return new LerJogoDto
            {
                JogoID = jogo.JogoID,
                Nome = jogo.Nome,
                Preco = jogo.Preco,
                Descricao = jogo.Descricao,
                StatusJogo = jogo.StatusJogo,

                ClassificacaoIndicativaID = jogo.ClassificacaoIndicativaID,
                Classificacao = jogo.ClassificacaoIndicativa?.Classificacao,

                GenerosIds = jogo.Genero.Select(genero => genero.GeneroID).ToList(),
                Generos = jogo.Genero.Select(genero => genero.Nome).ToList(),

                PlataformasIds = jogo.Plataforma.Select(plataforma => plataforma.PlataformaID).ToList(),
                Plataformas = jogo.Plataforma.Select(plataforma => plataforma.Nome).ToList(),

                UsuarioID = jogo.UsuarioID,
                UsuarioNome = jogo.Usuario?.Nome,
                UsuarioEmail = jogo.Usuario?.Email
            };
        }
    }
}
