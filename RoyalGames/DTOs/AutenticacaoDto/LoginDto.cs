namespace RoyalGames.DTOs.AutenticacaoDto
{
    public class LoginDto
    {
        //estou criando uma string email onde ela tem que receber diferente de nulo 
        public string Email { get; set; } = null!;

        //o mesmo na senha 
        public string Senha { get; set; } = null!;
    }
}
