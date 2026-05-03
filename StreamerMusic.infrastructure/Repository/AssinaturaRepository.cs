using MusicStreamer.Domain.Contracts;
using MusicStreamer.Domain.Entity;

namespace MusicStreamer.infrastructure.Repository
{
    public class AssinaturaRepository(DataContext context) : IAssinaturaRepository
    {
        private readonly DataContext _context = context;

        public async Task<bool> AtivarAssinaturaUsuarioAsync(int usuarioId)
        {
            var assinatura = new AssinaturaEntity
            {
                Id = usuarioId,
                Tipo = 1,
                Status = true,
                DataInicio = DateTime.UtcNow,
                DataFim = DateTime.UtcNow.AddMonths(1),
                RenovacaoAutomatica = true,
                DataFimAutomatica = DateTime.UtcNow.AddMonths(2)
            };
            _context.Assinaturas.Add(assinatura);
            var saveResult = await _context.SaveChangesAsync();
            return saveResult > 0;
        }

        public async Task<bool> CancelarAssinaturaUsuarioAsync(int usuarioId)
        {
            var assinatura = _context.Assinaturas.FirstOrDefault(a => a.Id == usuarioId);
            if (assinatura != null)
            {
                assinatura.Status = false;
                _context.Update(assinatura);
                var saveResult = await _context.SaveChangesAsync();
                return saveResult > 0;
            }
            return false;
        }
    }
}
