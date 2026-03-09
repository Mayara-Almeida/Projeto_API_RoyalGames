using RoyalGames.Domains;

namespace RoyalGames.Interfaces
{
    public interface IPlataformaRepository
    {
        List<Plataforma> Listar();

        Plataforma ObterPorId(int id);

        bool NomeExiste(string nome, int? plataformaIdAtual = null);

        public void Adicionar(Plataforma plataforma);

        public void Atualizar(Plataforma plataforma);
        public void Remover(int id);
    }
}
