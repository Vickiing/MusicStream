using MusicStreamer.App.Inputs;
using MusicStreamer.Domain.Entity;

namespace MusicStreamer.App.Contracts
{
    public interface IUsuarioApplicationService
    {
        Task<bool> CadastrarUsuarioAsync(UsuarioInput input);
        Task<bool> RemoverUsuarioAsync(Guid id);
    }
}
