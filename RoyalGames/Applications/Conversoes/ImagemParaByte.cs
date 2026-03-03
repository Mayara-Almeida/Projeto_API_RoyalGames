namespace RoyalGames.Applications.Conversoes
{
    public class ImagemParaByte
    {

        public static byte[] ConverterImagem(IFormFile imagem)
        {
            using var ms = new MemoryStream();
            imagem.CopyTo(ms);
            return ms.ToArray();
        }
    }
}
