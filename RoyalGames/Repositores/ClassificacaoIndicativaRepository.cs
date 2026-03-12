using Microsoft.EntityFrameworkCore;
using RoyalGames.Contexts;
using RoyalGames.Domains;
using RoyalGames.Interfaces;

namespace RoyalGames.Repositores
{
    public class ClassificacaoIndicativaRepository : IClassificacaoIndicativaRepository
    {
        private readonly RoyalGamesContext _context;

        public ClassificacaoIndicativaRepository(RoyalGamesContext context)
        {
            _context = context;
        }

        public List<ClassificacaoIndicativa> Listar()
        {
            return _context.ClassificacaoIndicativa.ToList();
        }

        public ClassificacaoIndicativa ObterPorId(int id)
        {
            ClassificacaoIndicativa classificacaoIndicativa = _context.ClassificacaoIndicativa.FirstOrDefault(ci => ci.ClassificacaoIndicativaID == id);

            return classificacaoIndicativa;
        }

        public bool ClassificacaoExiste(string classificacao, int? classificacaoIndicativaIdAtual = null)
        {
            var consulta = _context.ClassificacaoIndicativa.AsQueryable();

            if(classificacaoIndicativaIdAtual.HasValue)
            {
                consulta = consulta.Where(classificacaoIndicativa => classificacaoIndicativa.ClassificacaoIndicativaID != classificacaoIndicativaIdAtual.Value);
            }

            return consulta.Any(ci => ci.Classificacao == classificacao);
        }

        public void Adicionar(ClassificacaoIndicativa classificacaoIndicativa)
        {
            _context.ClassificacaoIndicativa.Add(classificacaoIndicativa);
            _context.SaveChanges();
        }

        public void Atualizar(ClassificacaoIndicativa classificacaoIndicativa)
        {
            ClassificacaoIndicativa classificacaoIndicativaBanco = _context.ClassificacaoIndicativa.FirstOrDefault(ci => ci.ClassificacaoIndicativaID == classificacaoIndicativa.ClassificacaoIndicativaID);

            if(classificacaoIndicativaBanco == null)
            {
                return;
            }

            classificacaoIndicativaBanco.Classificacao = classificacaoIndicativa.Classificacao;
            _context.SaveChanges();
        }

        public void Remover(int id)
        {
            ClassificacaoIndicativa classificacaoIndicativaBanco = _context.ClassificacaoIndicativa.FirstOrDefault(ci => ci.ClassificacaoIndicativaID == id);

            if (classificacaoIndicativaBanco == null)
            {
                return;
            }

            _context.ClassificacaoIndicativa.Remove(classificacaoIndicativaBanco);
            _context.SaveChanges();
        }
    }
}
