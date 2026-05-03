using Microsoft.EntityFrameworkCore;
using MusicStreamer.Domain.Contracts;
using MusicStreamer.Domain.Entity;

namespace MusicStreamer.infrastructure.Repository
{
    public class UsuarioRepository(DataContext context) : IUsuarioRepository
    {
        private readonly DataContext _context = context;

        public async Task<bool> CadastrarUsuarioAsync(UsuarioEntity entity)
        {
            _context.Usuarios.Add(entity);
            var result = await _context.SaveChangesAsync();
            return result > 0;
        }

        public Task<List<UsuarioEntity>> GetAllUsuariosAsync()
        {
            var usuarios = _context.Usuarios.ToList();
            return Task.FromResult(usuarios);
        }

        public async Task<UsuarioEntity?> GetUsuarioByIdAsync(int id)
        {
            return await _context.Usuarios.FirstOrDefaultAsync(u => u.Id == id);
        }

        public async Task<UsuarioEntity?> GetUsuarioByEmailAsync(string email)
        {
            return await _context.Usuarios.FirstOrDefaultAsync(u => u.Email == email);
        }

        public async Task RemoverUsuarioAsync(int id)
        {
            var usuario = await _context.Usuarios.FirstOrDefaultAsync(u => u.Id == id);
            if (usuario != null)
            {
                _context.Usuarios.Remove(usuario);
                await _context.SaveChangesAsync();
            }
        }
    }
}
