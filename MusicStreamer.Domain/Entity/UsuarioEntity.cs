namespace MusicStreamer.Domain.Entity
{
    public class UsuarioEntity
    {
        public int Id { get; set; }
        public required string Nome { get; set; }
        public required string Email { get; set; }
        public string Senha { get; set; } = string.Empty;
        public int Cpf { get; set; }
    }
}
