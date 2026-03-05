using RoyalGames.Domains;

namespace RoyalGames.Interfaces
{
    public interface IJogoRepository
    {

        List<Jogo> Listar();

        Jogo? ObterPorId(int id);  

        byte[] ObterPorImagem(int id);

        bool NomeExiste(string nome, int? jogoIdAtual = null);

        void Adicionar(Jogo jogo, int classificacaoIndicativaID, List<int> generosIds, List<int> plataformasIds);

        void Atualizar(Jogo jogo, int classificacaoIndicativaID, List<int> generosIds, List<int> plataformasIds);

        void Remover(int id);
    }
}
