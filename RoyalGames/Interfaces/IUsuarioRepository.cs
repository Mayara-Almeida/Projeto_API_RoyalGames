using RoyalGames.Domains;
using System.Security.Cryptography;

namespace RoyalGames.Interfaces
{
    public interface IUsuarioRepository
    {
        List<Usuario> Listar();

        Usuario? ObterPorId(int id); // O ponto de interrogação permite o retorno de valores nulos(não existem)

        Usuario? ObterPorEmail(string email);

        bool EmailExiste(string email);

        void Adicionar(Usuario usuario);

        void Atualizar(Usuario usuario);

        void Remover(int id);
    }
}
