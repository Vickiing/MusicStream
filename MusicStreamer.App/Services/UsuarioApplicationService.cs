using MusicStreamer.App.Contracts;
using MusicStreamer.App.Inputs;
using MusicStreamer.Domain.Contracts;
using MusicStreamer.Domain.Entity;

namespace MusicStreamer.App.Services
{
    public class UsuarioApplicationService(IUsuarioRepository usuarioRepository) : IUsuarioApplicationService
    {
        private readonly IUsuarioRepository _usuarioRepository = usuarioRepository;

        public async Task<bool> CadastrarUsuarioAsync(UsuarioInput input)
        {
            if (input is null)
            {
                return false;
            }

            var usuarioExistente = await _usuarioRepository.GetUsuarioByEmailAsync(input.Email);
            if (usuarioExistente is not null)
            {
                return false;
            }

            var usuarioEntity = UsuarioEntity.Criar(input.Nome, input.Email, input.Senha, input.Cpf);

            return await _usuarioRepository.CadastrarUsuarioAsync(usuarioEntity);
        }

        public async Task<bool> RemoverUsuarioAsync(Guid id)
        {
            return await _usuarioRepository.RemoverUsuarioAsync(id);
        }
    }
}
