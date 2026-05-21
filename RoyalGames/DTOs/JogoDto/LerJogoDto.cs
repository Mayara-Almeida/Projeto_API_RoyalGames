namespace RoyalGames.DTOs.JogoDto
{
    public class LerJogoDto
    {
        public int JogoID { get; set; }

        public string Nome { get; set; } = null!;

        public decimal Preco {  get; set; } 
        
        public string Descricao { get; set; } = null!;

        public bool? StatusJogo { get; set; }

        // Classificaçãoindicativa
        public int? ClassificacaoIndicativaID { get; set; }
        public string? Classificacao {  get; set; }

        // Gênero
        public List<int> GenerosIds { get; set; } = new();
        public List<string> Generos {  get; set; } = new();

        // Plataforma
        public List<int> PlataformasIds { get; set; } = new();
        public List<string> Plataformas { get; set; } = new();

        // Usuário que cadastrou
        public int? UsuarioID { get; set; }
        public string? UsuarioNome { get; set; }
        public string? UsuarioEmail { get; set; }

        public string ImagemUrl { get; set; }
    }
}
