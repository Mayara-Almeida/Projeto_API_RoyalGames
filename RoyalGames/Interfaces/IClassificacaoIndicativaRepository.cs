using RoyalGames.Domains;

namespace RoyalGames.Interfaces
{
    public interface IClassificacaoIndicativaRepository
    {
        List<ClassificacaoIndicativa> Listar();

        ClassificacaoIndicativa ObterPorId(int id);

        public bool ClassificacaoExiste(string classificacao, int? classificacaoIndicativaIdAtual = null);

        public void Adicionar(ClassificacaoIndicativa classificacaoIndicativa);

        public void Atualizar(ClassificacaoIndicativa classificacaoIndicativa);

        public void Remover(int id);
    }
}
