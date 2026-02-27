using System;
using System.Collections.Generic;

namespace RoyalGames.Domains;

public partial class Log_AlteracaoJogo
{
    public int Log_AlteracaJogoID { get; set; }

    public DateTime DataAlteracao { get; set; }

    public string? NomeAnterior { get; set; }

    public decimal? Precoanterior { get; set; }

    public int? JogoID { get; set; }

    public virtual Jogo? Jogo { get; set; }
}
