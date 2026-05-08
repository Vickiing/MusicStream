namespace MusicStreamer.App.Inputs
{
    public class UsuarioInput
    {
        public required Guid Id { get; set; }
        public required string Nome { get; set; }
        public required string Email { get; set; }
        public required string Senha { get; set; }
        public required int Cpf { get; set; }
    }
}
