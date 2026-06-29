using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MusicStreamer.Api.Contracts;
using MusicStreamer.App.Contracts;
using MusicStreamer.App.DTOs;

namespace MusicStreamer.Api.Controllers;

[ApiController]
[Authorize]
[Route("api/favorites")]
public sealed class FavoritosController(IServicoFavoritos servicoFavoritos) : ControllerBase
{
    [HttpPost("tracks")]
    public async Task<ActionResult<ResumoFavoritosDto>> FavoriteTrack([FromBody] FavoritarMusicaRequest request, CancellationToken cancellationToken)
    {
        return Ok(await servicoFavoritos.FavoriteTrackAsync(request.UserId, request.TrackId, cancellationToken));
    }

    [HttpDelete("tracks/{trackId:guid}")]
    public async Task<ActionResult<ResumoFavoritosDto>> UnfavoriteTrack(Guid trackId, [FromQuery] Guid userId, CancellationToken cancellationToken)
    {
        return Ok(await servicoFavoritos.UnfavoriteTrackAsync(userId, trackId, cancellationToken));
    }

    [HttpPost("artists")]
    public async Task<ActionResult<ResumoFavoritosDto>> FavoriteArtist([FromBody] FavoritarBandaRequest request, CancellationToken cancellationToken)
    {
        return Ok(await servicoFavoritos.FavoriteArtistAsync(request.UserId, request.ArtistId, cancellationToken));
    }

    [HttpDelete("artists/{artistId:guid}")]
    public async Task<ActionResult<ResumoFavoritosDto>> UnfavoriteArtist(Guid artistId, [FromQuery] Guid userId, CancellationToken cancellationToken)
    {
        return Ok(await servicoFavoritos.UnfavoriteArtistAsync(userId, artistId, cancellationToken));
    }
}

