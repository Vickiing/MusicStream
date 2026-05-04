using MusicStreamer.App.Contracts;
using MusicStreamer.App.Inputs;
using MusicStreamer.Domain.Contracts;
using MusicStreamer.Domain.Entity;

namespace MusicStreamer.App.Services
{
    public class UsuarioApplicationService(IUsuarioRepository usuarioRepository) : IUsuarioApplicationService
    {
        private readonly IUsuarioRepository _usuarioRepository = usuarioRepository;

        public async Task<bool> CadastrarUsuarioAsync(CadastrarUsuarioInput input)
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

            var usuarioEntity = new UsuarioEntity
            {
                Nome = input.Nome,
                Email = input.Email,
                Senha = input.Senha,
                Cpf = input.Cpf
            };

            return await _usuarioRepository.CadastrarUsuarioAsync(usuarioEntity);
        }

        public Task<List<UsuarioEntity>> GetAllUsuariosAsync()
        {
            return _usuarioRepository.GetAllUsuariosAsync();
        }

        public Task<UsuarioEntity?> GetUsuarioByIdAsync(int id)
        {
            return _usuarioRepository.GetUsuarioByIdAsync(id);
        }

        public Task RemoverUsuarioAsync(int id)
        {
            return _usuarioRepository.RemoverUsuarioAsync(id);
        }
    }
}
