using Microsoft.AspNetCore.Mvc;
using MusicStreamer.App.Contracts;
using MusicStreamer.App.DTOs;

namespace MusicStreamer.Api.Controllers;

[ApiController]
[Route("api/catalog")]
public sealed class CatalogoController(IServicoCatalogo servicoCatalogo) : ControllerBase
{
    [HttpGet("artists")]
    public async Task<ActionResult<IReadOnlyList<BandaDto>>> GetArtists(CancellationToken cancellationToken)
    {
        return Ok(await servicoCatalogo.GetArtistsAsync(cancellationToken));
    }

    [HttpGet("albums")]
    public async Task<ActionResult<IReadOnlyList<AlbumDto>>> GetAlbums(CancellationToken cancellationToken)
    {
        return Ok(await servicoCatalogo.GetAlbumsAsync(cancellationToken));
    }

    [HttpGet("search")]
    public async Task<ActionResult<ResultadoBuscaCatalogoDto>> Search([FromQuery] string term, [FromQuery] int page = 1, [FromQuery] int pageSize = 10, CancellationToken cancellationToken = default)
    {
        if (string.IsNullOrWhiteSpace(term))
        {
            return BadRequest("Informe um termo para busca.");
        }

        return Ok(await servicoCatalogo.SearchAsync(term, page, pageSize, cancellationToken));
    }
}

