using Microsoft.EntityFrameworkCore;
using RoyalGames.Contexts;
using RoyalGames.Domains;
using RoyalGames.Interfaces;

namespace RoyalGames.Repositores
{
    public class JogoRepository : IJogoRepository
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
        public void Adicionar(Jogo jogo, int classificacaoIndicativaID, List<int> generosIds, List<int> plataformasIds)
        {
            jogo.ClassificacaoIndicativaID = classificacaoIndicativaID;

            List<Genero> generos = _context.Genero
                .Where(genero => generosIds.Contains(genero.GeneroID))
                .ToList();
            jogo.Genero = generos; // Atribui ao jogo a lista de gêneros que passamos buscando pelo id

            List<Plataforma> plataformas = _context.Plataforma
                .Where(plataforma => plataformasIds.Contains(plataforma.PlataformaID))
                .ToList();
            jogo.Plataforma = plataformas;

            _context.Jogo.Add(jogo);
            _context.SaveChanges();
        }

        public void Atualizar(Jogo jogo, int classificacaoIndicativaID, List<int> generosIds, List<int> plataformasIds)
        {
            Jogo? jogoBanco = _context.Jogo
                .Include(jogo => jogo.ClassificacaoIndicativaID)
                .Include(jogo => jogo.Genero)
                .Include(jogo => jogo.Plataforma)
                .FirstOrDefault(jogoAux => jogoAux.JogoID == jogo.JogoID);

            if(jogoBanco == null)
            {
                return; // Não atualiza
            }

            // Atualização de algumas informações de jogo
            jogoBanco.Nome = jogo.Nome;
            jogoBanco.Preco = jogo.Preco;
            jogoBanco.Descricao = jogo.Descricao;
            jogoBanco.ClassificacaoIndicativaID = jogo.ClassificacaoIndicativaID;

            // Atualização da imagem
            if (jogo.Imagem != null && jogo.Imagem.Length > 0)
            {
                jogoBanco.Imagem = jogo.Imagem;
            }

            // Atualização do status
            if(jogo.StatusJogo.HasValue)
            {
                jogoBanco.StatusJogo = jogo.StatusJogo;
            }

            // Busca de ids de gêneros do banco que vieram da requisição
            var generos = _context.Genero
                .Where(genero => generosIds.Contains(genero.GeneroID))
                .ToList();

            // Remover o vínculo entre jogo e lista de gêneros
            jogoBanco.Genero.Clear();

            // Atualizar os gêneros
            foreach (var genero in generos)
            {
                jogoBanco.Genero.Add(genero);
            }


            // Busca de ids de plataformas do banco que vieram da requisição
            var plataformas = _context.Plataforma
                .Where(plataforma => plataformasIds.Contains(plataforma.PlataformaID))
                .ToList();
            
            // Remover o vínculo entre jogo e lista de gêneros
            jogoBanco.Plataforma.Clear();

            // Atualizar as plataformas
            foreach(var plataforma in plataformas)
            {
                jogoBanco.Plataforma.Add(plataforma);
            }

            _context.SaveChanges(); 
        }

        public void Remover(int id)
        {
            Jogo? jogo = _context.Jogo.FirstOrDefault(jogo => jogo.JogoID == id);

            if(jogo == null)
            {
              return;
            }

            _context.Jogo.Remove(jogo);
            _context.SaveChanges();
        }

    }
}
