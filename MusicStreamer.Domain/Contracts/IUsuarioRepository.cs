using MusicStreamer.Domain.Entity;

namespace MusicStreamer.Domain.Contracts
{
    public interface IUsuarioRepository
    {
        Task<UsuarioEntity?> GetUsuarioByIdAsync(Guid id);
        Task<UsuarioEntity?> GetUsuarioByEmailAsync(string email);
        Task<List<UsuarioEntity>> GetAllUsuariosAsync();
        Task<bool> CadastrarUsuarioAsync(UsuarioEntity entity);
        Task<bool> AtualizarUsuarioAsync(UsuarioEntity entity);
        Task<bool> RemoverUsuarioAsync(Guid id);
    }
}
