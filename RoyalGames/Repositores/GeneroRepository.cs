using RoyalGames.Contexts;
using RoyalGames.Domains;
using RoyalGames.Interfaces;


namespace RoyalGames.Repositores
{
    public class GeneroRepository : IGeneroRepository
    {
        private readonly RoyalGamesContext _context;

        public GeneroRepository(RoyalGamesContext context)
        {
            _context = context; 
        }
        
        public List<Genero> Listar()
        {
            return _context.Genero.ToList();
        }

        public Genero ObterPorId(int id)
        {
            Genero genero = _context.Genero.FirstOrDefault(c => c.GeneroID == id);

            return genero;
        }

        public bool NomeExiste(string nome, int? generoIdAtual = null)
        {
            var consulta = _context.Genero.AsQueryable();

            if (generoIdAtual.HasValue)
            {
                consulta = consulta.Where(genero => genero.GeneroID != generoIdAtual.Value);
            }

            return consulta.Any(c => c.Nome == nome);
        }

        public void Adicionar(Genero genero)
        {
            _context.Genero.Add(genero);
            _context.SaveChanges();
        }

        public void Atualizar(Genero genero)
        {
            Genero generoBanco = _context.Genero.FirstOrDefault(c => c.GeneroID == genero.GeneroID);

            if (generoBanco == null)
            {
                return;
            }

            generoBanco.Nome = genero.Nome;

            _context.SaveChanges();
        }

        public void Remover(int id)
        {
            Genero generoBanco = _context.Genero.FirstOrDefault(c => c.GeneroID == id);

            if (generoBanco == null)
            {
                return;
            }

            _context.Genero.Remove(generoBanco);
            _context.SaveChanges();
        }

    }
}
