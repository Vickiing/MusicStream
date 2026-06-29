using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MusicStreamer.Api.Contracts;
using MusicStreamer.App.Contracts;
using MusicStreamer.App.DTOs;

namespace MusicStreamer.Api.Controllers;

[ApiController]
[Authorize]
[Route("api/playlists")]
public sealed class PlaylistsController(IPlaylistService playlistService) : ControllerBase
{
    [HttpPost]
    public async Task<ActionResult<PlaylistDto>> Create([FromBody] CreatePlaylistRequest request, CancellationToken cancellationToken)
    {
        var playlist = await playlistService.CreateAsync(new CreatePlaylistDto(request.UserId, request.Name), cancellationToken);
        return CreatedAtAction(nameof(GetByUser), new { userId = request.UserId }, playlist);
    }

    [HttpPost("{playlistId:guid}/tracks")]
    public async Task<ActionResult<PlaylistDto>> AddTrack(Guid playlistId, [FromBody] AddTrackToPlaylistRequest request, CancellationToken cancellationToken)
    {
        var playlist = await playlistService.AddTrackAsync(new AddTrackToPlaylistDto(playlistId, request.TrackId), cancellationToken);
        return playlist is null ? NotFound() : Ok(playlist);
    }

    [HttpGet("user/{userId:guid}")]
    public async Task<ActionResult<IReadOnlyList<PlaylistDto>>> GetByUser(Guid userId, CancellationToken cancellationToken)
    {
        return Ok(await playlistService.GetByUserAsync(userId, cancellationToken));
    }
}
