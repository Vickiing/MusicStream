using System.ComponentModel.DataAnnotations.Schema;

namespace MusicStreamer.Domain.Entity
{
    [Table("Usuarios")]
    public class UsuarioEntity
    {
        [Column("Id")]
        public int Id { get; set; }
        [Column("Nome")]
        public required string Nome { get; set; }
        [Column("Email")]
        public required string Email { get; set; }
        [Column("Senha")]
        public string Senha { get; set; } = string.Empty;
        [Column("Cpf")]
        public int Cpf { get; set; }
        [Column("AssinaturaAtiva")]
        public bool AssinaturaAtiva { get; set; }
        [Column("IdAssinatura")]
        public int? AssinaturaId { get; set; }
    }
}
