using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MusicStreamer.Api.Contracts;
using MusicStreamer.App.Contracts;
using MusicStreamer.App.DTOs;

namespace MusicStreamer.Api.Controllers;

[ApiController]
[Authorize]
[Route("api/favorites")]
public sealed class FavoritesController(IServicoFavoritos favoritesService) : ControllerBase
{
    [HttpPost("tracks")]
    public async Task<ActionResult<ResumoFavoritosDto>> FavoriteTrack([FromBody] FavoriteTrackRequest request, CancellationToken cancellationToken)
    {
        return Ok(await favoritesService.FavoriteTrackAsync(request.UserId, request.TrackId, cancellationToken));
    }

    [HttpDelete("tracks/{trackId:guid}")]
    public async Task<ActionResult<ResumoFavoritosDto>> UnfavoriteTrack(Guid trackId, [FromQuery] Guid userId, CancellationToken cancellationToken)
    {
        return Ok(await favoritesService.UnfavoriteTrackAsync(userId, trackId, cancellationToken));
    }

    [HttpPost("artists")]
    public async Task<ActionResult<ResumoFavoritosDto>> FavoriteArtist([FromBody] FavoriteArtistRequest request, CancellationToken cancellationToken)
    {
        return Ok(await favoritesService.FavoriteArtistAsync(request.UserId, request.ArtistId, cancellationToken));
    }

    [HttpDelete("artists/{artistId:guid}")]
    public async Task<ActionResult<ResumoFavoritosDto>> UnfavoriteArtist(Guid artistId, [FromQuery] Guid userId, CancellationToken cancellationToken)
    {
        return Ok(await favoritesService.UnfavoriteArtistAsync(userId, artistId, cancellationToken));
    }
}

