using MusicStreamer.App.Contracts;
using MusicStreamer.App.DTOs;
using MusicStreamer.Domain.Repositories;

namespace MusicStreamer.App.Services;

public sealed class ServicoComerciantes(IComercianteRepository merchantRepository) : IServicoComerciantes
{
    public async Task<IReadOnlyList<ComercianteDto>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        var merchants = await merchantRepository.GetAllAsync(cancellationToken);
        return merchants
            .Select(item => new ComercianteDto(item.Id, item.Name, item.Category, item.IsActive))
            .ToList();
    }
}


