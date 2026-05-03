namespace MusicStreamer.App.Inputs
{
    public class CadastrarUsuarioInput
    {
        public required string Nome { get; set; }
        public required string Email { get; set; }
        public required string Senha { get; set; }
        public required int Cpf { get; set; }
    }
}
