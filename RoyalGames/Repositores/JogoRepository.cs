using Microsoft.EntityFrameworkCore;
using RoyalGames.Contexts;
using RoyalGames.Domains;

namespace RoyalGames.Repositores
{
    public class JogoRepository
    {
        private readonly RoyalGamesContext _context;

        public JogoRepository(RoyalGamesContext context)
        {
            _context = context;
        }

        public List<Jogo> Listar()
        {
            List<Jogo> jogos = _context.Jogo
                .Include(jogo => jogo.ClassificacaoIndicativa)
                .Include(jogo => jogo.Plataforma)
                .Include(jogo => jogo.Genero)
                .Include(jogo => jogo.Usuario)
                .ToList();

            return jogos;
        }

       public Jogo ObterPorId(int id)
       {
            Jogo? jogo = _context.Jogo
                 .Include(jogo => jogo.ClassificacaoIndicativa)
                 .Include(jogo => jogo.Plataforma)
                 .Include(jogo => jogo.Genero)
                 .Include(jogo => jogo.Usuario)
                 .FirstOrDefault(jogoDb => jogoDb.JogoID == id);

            return jogo;
       }

        public byte[] ObterPorImagem(int id)
        {
            var jogo = _context.Jogo
                .Where(jogo => jogo.JogoID == id)
                .Select(jogo => jogo.Imagem)
                .FirstOrDefault();

            return jogo;
        }

        public bool NomeExiste(string nome, int? jogoIdAtual = null)
        {
            var jogoConsultado = _context.Jogo.AsQueryable();

            if (jogoIdAtual.HasValue)
            {
                jogoConsultado = jogoConsultado.Where(jogo => jogo.JogoID != jogoIdAtual.Value);
            }

            return jogoConsultado.Any(jogo => jogo.Nome == nome);
        }

        // Para cada jogo adicionado é necessário passar uma clasificação, um gênero e uma plataforma
        //public void Adicionar(Jogo jogo, List<int> classificacaiIndicativaID, List<int> generosIds, List<int> plataformasIds)
        //{

        //}

    }
}
