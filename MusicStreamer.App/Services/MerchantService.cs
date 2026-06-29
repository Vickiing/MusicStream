using MusicStreamer.App.Contracts;
using MusicStreamer.App.DTOs;
using MusicStreamer.Domain.Repositories;

namespace MusicStreamer.App.Services;

public sealed class MerchantService(IMerchantRepository merchantRepository) : IMerchantService
{
    public async Task<IReadOnlyList<MerchantDto>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        var merchants = await merchantRepository.GetAllAsync(cancellationToken);
        return merchants
            .Select(item => new MerchantDto(item.Id, item.Name, item.Category, item.IsActive))
            .ToList();
    }
}
