namespace RoyalGames.DTOs.JogoDto
{
    public class AtualizarJogoDto
    {
        public string Nome { get; set; } = null!;

        public decimal Preco { get; set; }

        public string Descricao { get; set; } = null!;

        public IFormFile Imagem { get; set; } = null!;

        public int ClassificacaoIndicativaID { get; set; }

        public List<int> GenerosIds { get; set; } = new();

        public List<int> PlataformasIds { get; set; } = new();

        public bool? StatusJogo { get; set; }
    }
}
