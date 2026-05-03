using MusicStreamer.Domain.Contracts;
using MusicStreamer.Domain.Entity;
using MusicStreamer.infrastructure.Contract;

namespace MusicStreamer.infrastructure.Repository
{
    public class AssinaturaRepository(DataContext context) : IAssinatura
    {
        private readonly DataContext _context = context;

        public async Task<bool> AtivarAssinatura(AssinaturaEntity entity)
        {
            var resultado = _context.Add(entity);
            var saveResult = await _context.SaveChangesAsync();
            return saveResult > 0;
        }

        public async Task<bool> DesativarAssinatura(int id)
        {
            var assinatura = _context.Assinaturas.FirstOrDefault(a => a.Id == id);

            if (assinatura != null)
            {
                _context.Remove(assinatura);
                var saveResult = await _context.SaveChangesAsync();
                return saveResult > 0;
            }
            return false;
        }
    }
}
