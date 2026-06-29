using Microsoft.AspNetCore.Mvc;
using MusicStreamer.App.Contracts;
using MusicStreamer.App.DTOs;

namespace MusicStreamer.Api.Controllers;

[ApiController]
[Route("api/catalog")]
public sealed class CatalogController(IServicoCatalogo catalogService) : ControllerBase
{
    [HttpGet("artists")]
    public async Task<ActionResult<IReadOnlyList<BandaDto>>> GetArtists(CancellationToken cancellationToken)
    {
        return Ok(await catalogService.GetArtistsAsync(cancellationToken));
    }

    [HttpGet("albums")]
    public async Task<ActionResult<IReadOnlyList<AlbumDto>>> GetAlbums(CancellationToken cancellationToken)
    {
        return Ok(await catalogService.GetAlbumsAsync(cancellationToken));
    }

    [HttpGet("search")]
    public async Task<ActionResult<ResultadoBuscaCatalogoDto>> Search([FromQuery] string term, CancellationToken cancellationToken)
    {
        if (string.IsNullOrWhiteSpace(term))
        {
            return BadRequest("Informe um termo para busca.");
        }

        return Ok(await catalogService.SearchAsync(term, cancellationToken));
    }
}

