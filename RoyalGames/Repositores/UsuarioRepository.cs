using RoyalGames.Contexts;
using RoyalGames.Domains;
using RoyalGames.Interfaces;

namespace RoyalGames.Repositores
{
    public class UsuarioRepository : IUsuarioRepository
    {
        private readonly RoyalGamesContext _context;

        public UsuarioRepository(RoyalGamesContext context)
        {
            _context = context;
        }

        public List<Usuario> Listar()
        {
            return _context.Usuario.ToList();
        }

        public Usuario? ObterPorId(int id)
        {
            return _context.Usuario.Find(id);
        }

        public Usuario? ObterPorEmail(string email)
        {
            return _context.Usuario.FirstOrDefault(usuario => usuario.Email == email);
        }

        public bool EmailExiste(string email)
        {
            return _context.Usuario.Any(usuario => usuario.Email == email);
        }

        public void Adicionar(Usuario usuario)
        {
            _context.Usuario.Add(usuario);
            _context.SaveChanges();
        }

        public void Atualizar(Usuario usuario)
        {
            // Verificar se usuário existe
            Usuario? usuarioBanco = _context.Usuario.FirstOrDefault(usuarioAux => usuarioAux.UsuarioID == usuario.UsuarioID);

            if(usuarioBanco == null)
            {
                return;
            }

            usuarioBanco.Nome = usuario.Nome;
            usuarioBanco.Email = usuario.Email;
            usuarioBanco.Senha = usuario.Senha;

            _context.SaveChanges();
        }

        public void Remover(int id)
        {
            // Verificar se o usuário existe
            Usuario usuario = _context.Usuario.FirstOrDefault(usuarioAux  => usuarioAux.UsuarioID == id);

            if(usuario == null)
            {
                return; 
            }

            _context.Usuario.Remove(usuario);
            _context.SaveChanges();
        }
    }
}
