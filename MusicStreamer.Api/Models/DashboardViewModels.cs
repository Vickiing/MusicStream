using MusicStreamer.App.DTOs;

namespace MusicStreamer.Api.Models;

public sealed class DashboardViewModel
{
    public Guid UserId { get; init; }
    public string DisplayName { get; init; } = string.Empty;
    public string Email { get; init; } = string.Empty;
    public string Token { get; init; } = string.Empty;
    public string? SearchTerm { get; init; }
    public string? StatusMessage { get; init; }
    public IReadOnlyList<ArtistDto> Artists { get; init; } = [];
    public IReadOnlyList<AlbumDto> Albums { get; init; } = [];
    public IReadOnlyList<TrackDto> Tracks { get; init; } = [];
    public IReadOnlyList<SubscriptionPlanDto> Plans { get; init; } = [];
    public IReadOnlyList<PlaylistDto> Playlists { get; init; } = [];
    public IReadOnlyList<MerchantDto> Merchants { get; init; } = [];
    public IReadOnlyList<TransactionResultDto> Transactions { get; init; } = [];
    public CatalogSearchResultDto? SearchResult { get; init; }
}

public sealed class SessionUserViewModel
{
    public Guid UserId { get; init; }
    public string DisplayName { get; init; } = string.Empty;
    public string Email { get; init; } = string.Empty;
    public string Token { get; init; } = string.Empty;
}

public sealed class ChoosePlanViewModel
{
    public Guid PlanId { get; set; }
}

public sealed class CreatePlaylistViewModel
{
    [System.ComponentModel.DataAnnotations.Required]
    public string Name { get; set; } = string.Empty;
}

public sealed class AddTrackToPlaylistViewModel
{
    public Guid PlaylistId { get; set; }
    public Guid TrackId { get; set; }
}

public sealed class FavoriteTrackViewModel
{
    public Guid TrackId { get; set; }
}

public sealed class FavoriteArtistViewModel
{
    public Guid ArtistId { get; set; }
}

public sealed class AuthorizeTransactionViewModel
{
    public Guid MerchantId { get; set; }
    public decimal Amount { get; set; }
    public DateTimeOffset? RequestedAtUtc { get; set; }
}
