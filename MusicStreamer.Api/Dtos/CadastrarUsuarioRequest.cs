namespace MusicStreamer.Api.Dtos
{
    public class CadastrarUsuarioRequest
    {
        public required string Nome { get; set; }
        public required string Email { get; set; }
        public required string Senha { get; set; } = string.Empty;
        public required int Cpf { get; set; }
    }
}
