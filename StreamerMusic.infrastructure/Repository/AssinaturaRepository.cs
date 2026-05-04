using Microsoft.EntityFrameworkCore;
using MusicStreamer.Domain.Contracts;
using MusicStreamer.Domain.Entity;

namespace MusicStreamer.infrastructure.Repository
{
    public class AssinaturaRepository(DataContext context) : IAssinaturaRepository
    {
        private readonly DataContext _context = context;

        public async Task<AssinaturaEntity?> CadastrarAssinaturaAsync(AssinaturaEntity entity)
        {
            _context.Assinaturas.Add(entity);
            var saveResult = await _context.SaveChangesAsync();
            return saveResult > 0 ? entity : null;
        }

        public async Task<bool> CancelarAssinaturaUsuarioAsync(int usuarioId)
        {
            var assinatura = await _context.Assinaturas.FirstOrDefaultAsync(a => a.UsuarioId == usuarioId && a.Ativa);
            if (assinatura != null)
            {
                assinatura.Ativa = false;
                _context.Update(assinatura);
                var saveResult = await _context.SaveChangesAsync();
                return saveResult > 0;
            }
            return false;
        }
    }
}
