using MusicStreamer.App.Inputs;
using MusicStreamer.Domain.Entity;

namespace MusicStreamer.App.Contracts
{
    public interface IUsuarioApplicationService
    {
        Task<bool> CadastrarUsuarioAsync(CadastrarUsuarioInput input);
        Task<UsuarioEntity?> GetUsuarioByIdAsync(int id);
        Task<List<UsuarioEntity>> GetAllUsuariosAsync();
        Task RemoverUsuarioAsync(int id);
    }
}
