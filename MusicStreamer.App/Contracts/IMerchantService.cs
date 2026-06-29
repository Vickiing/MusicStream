using MusicStreamer.App.DTOs;

namespace MusicStreamer.App.Contracts;

public interface IMerchantService
{
    Task<IReadOnlyList<MerchantDto>> GetAllAsync(CancellationToken cancellationToken = default);
}
