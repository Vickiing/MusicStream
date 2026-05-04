using MusicStreamer.Domain.Entity;

namespace MusicStreamer.Domain.Contracts
{
    public interface IUsuarioRepository
    {
        Task<UsuarioEntity?> GetUsuarioByIdAsync(int id);
        Task<UsuarioEntity?> GetUsuarioByEmailAsync(string email);
        Task<List<UsuarioEntity>> GetAllUsuariosAsync();
        Task<bool> CadastrarUsuarioAsync(UsuarioEntity entity);
        Task<bool> AtualizarUsuarioAsync(UsuarioEntity entity);
        Task RemoverUsuarioAsync(int id);
    }
}
