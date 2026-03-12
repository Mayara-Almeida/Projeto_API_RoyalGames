using RoyalGames.Contexts;
using RoyalGames.Domains;
using RoyalGames.Interfaces;

namespace RoyalGames.Repositores
{
    public class PlataformaRepository : IPlataformaRepository
    {
        private readonly RoyalGamesContext _context;

        public PlataformaRepository(RoyalGamesContext context)
        {
            _context = context;
        }

        public List<Plataforma> Listar()
        {
            return _context.Plataforma.ToList();
        }

        public Plataforma ObterPorId(int id)
        {
            // Busca no banco 
            Plataforma plataforma = _context.Plataforma.FirstOrDefault(p => p.PlataformaID == id);

            return plataforma;
        }
        public bool NomeExiste(string nome, int? plataformaIdAtual = null)
        {
            var consulta = _context.Plataforma.AsQueryable();

            if(plataformaIdAtual.HasValue)
            {
                // Faz uma consulta no banco de plataformas onde o id esteja diferente do valor do id passado na requisição
                consulta = consulta.Where(plataforma => plataforma.PlataformaID != plataformaIdAtual.Value);
            }
            // Retorna caso haja um mesmo produto com aquele nome
            return consulta.Any(p => p.Nome == nome);
        }

        public void Adicionar(Plataforma plataforma)
        {
            _context.Plataforma.Add(plataforma);
            _context.SaveChanges();
        }

        public void Atualizar(Plataforma plataforma)
        {
            Plataforma plataformaBanco = _context.Plataforma.FirstOrDefault(p => p.PlataformaID == plataforma.PlataformaID);

            if(plataformaBanco == null)
            {
                return; 
            }

            plataformaBanco.Nome = plataforma.Nome;
            _context.SaveChanges();
        }

        public void Remover(int id)
        {
            Plataforma plataformaBanco = _context.Plataforma.FirstOrDefault(p => p.PlataformaID == id);

            if (plataformaBanco == null)
            {
                return;
            }

            _context.Plataforma.Remove(plataformaBanco);
            _context.SaveChanges();
        }

    }
}
