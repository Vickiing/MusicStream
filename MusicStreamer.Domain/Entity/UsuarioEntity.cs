using System.ComponentModel.DataAnnotations.Schema;

namespace MusicStreamer.Domain.Entity
{
    
    public class UsuarioEntity
    {
        
        public Guid Id { get; set; }
        public required string Nome { get; set; }
        public required string Email { get; set; }
        public string Senha { get; set; } = string.Empty;
        public int Cpf { get; set; }
        public bool AssinaturaAtiva { get; set; }
        public Guid? AssinaturaId { get; set; }

        public UsuarioEntity() { }
        public static UsuarioEntity Criar(string nome, string email, string senha, int cpf)
        {
            var usuario = new UsuarioEntity
            {
                Id = Guid.NewGuid(),
                Nome = nome,
                Email = email,
                Senha = senha,
                Cpf = cpf,
                AssinaturaAtiva = false,
                AssinaturaId = null
            };
            return usuario;
        }
    }
}
