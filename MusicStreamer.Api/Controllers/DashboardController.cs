using Microsoft.AspNetCore.Mvc;
using MusicStreamer.Api.Extensions;
using MusicStreamer.Api.Models;
using MusicStreamer.App.Contracts;
using MusicStreamer.App.DTOs;

namespace MusicStreamer.Api.Controllers;

[Route("app")]
public sealed class DashboardController(
    IServicoCatalogo catalogService,
    IServicoPlanosAssinatura subscriptionService,
    IServicoPlaylist playlistService,
    IServicoFavoritos favoritesService,
    IServicoTransacoes transactionService,
    IServicoComerciantes merchantService) : Controller
{
    [HttpGet("")]
    public IActionResult Root()
    {
        return RedirectToAction(nameof(Index));
    }

    [HttpGet("dashboard")]
    public async Task<IActionResult> Index([FromQuery] string? term, [FromQuery] string? message, CancellationToken cancellationToken)
    {
        var user = HttpContext.Session.GetCurrentUser();
        if (user is null)
        {
            return RedirectToAction("Login", "AccountMvc");
        }

        ResultadoBuscaCatalogoDto? searchResult = null;
        if (!string.IsNullOrWhiteSpace(term))
        {
            searchResult = await catalogService.SearchAsync(term, cancellationToken);
        }

        var viewModel = new DashboardViewModel
        {
            UserId = user.UserId,
            DisplayName = user.DisplayName,
            Email = user.Email,
            Token = user.Token,
            SearchTerm = term,
            StatusMessage = message,
            Artists = await catalogService.GetArtistsAsync(cancellationToken),
            Albums = await catalogService.GetAlbumsAsync(cancellationToken),
            Tracks = await catalogService.GetTracksAsync(cancellationToken),
            Plans = await subscriptionService.GetPlansAsync(cancellationToken),
            Playlists = await playlistService.GetByUserAsync(user.UserId, cancellationToken),
            Merchants = await merchantService.GetAllAsync(cancellationToken),
            Transactions = await transactionService.GetByUserAsync(user.UserId, cancellationToken),
            SearchResult = searchResult
        };

        return View(viewModel);
    }

    [HttpPost("subscriptions/choose-plan")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> ChoosePlan(ChoosePlanViewModel model, CancellationToken cancellationToken)
    {
        var user = RequireUser();
        await subscriptionService.ChoosePlanAsync(new EscolherPlanoDto(user.UserId, model.PlanId), cancellationToken);
        return RedirectToAction(nameof(Index), new { message = "Plano atualizado com sucesso." });
    }

    [HttpPost("playlists")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> CreatePlaylist(CreatePlaylistViewModel model, CancellationToken cancellationToken)
    {
        var user = RequireUser();
        if (!ModelState.IsValid)
        {
            return RedirectToAction(nameof(Index), new { message = "Informe o nome da playlist." });
        }

        await playlistService.CreateAsync(new CriarPlaylistDto(user.UserId, model.Name), cancellationToken);
        return RedirectToAction(nameof(Index), new { message = "Playlist criada." });
    }

    [HttpPost("playlists/add-track")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> AddTrack(AddTrackToPlaylistViewModel model, CancellationToken cancellationToken)
    {
        RequireUser();
        await playlistService.AddTrackAsync(new AdicionarMusicaNaPlaylistDto(model.PlaylistId, model.TrackId), cancellationToken);
        return RedirectToAction(nameof(Index), new { message = "Musica associada a playlist." });
    }

    [HttpPost("favorites/tracks")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> FavoriteTrack(FavoriteTrackViewModel model, CancellationToken cancellationToken)
    {
        var user = RequireUser();
        await favoritesService.FavoriteTrackAsync(user.UserId, model.TrackId, cancellationToken);
        return RedirectToAction(nameof(Index), new { message = "Musica favoritada." });
    }

    [HttpPost("favorites/artists")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> FavoriteArtist(FavoriteArtistViewModel model, CancellationToken cancellationToken)
    {
        var user = RequireUser();
        await favoritesService.FavoriteArtistAsync(user.UserId, model.ArtistId, cancellationToken);
        return RedirectToAction(nameof(Index), new { message = "Banda favoritada." });
    }

    [HttpPost("transactions/authorize")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> AuthorizeTransaction(AuthorizeTransactionViewModel model, CancellationToken cancellationToken)
    {
        var user = RequireUser();
        await transactionService.AuthorizeAsync(
            new AutorizarTransacaoDto(user.UserId, model.MerchantId, model.Amount, model.RequestedAtUtc ?? DateTimeOffset.UtcNow),
            cancellationToken);

        return RedirectToAction(nameof(Index), new { message = "Transacao processada." });
    }

    private SessionUserViewModel RequireUser()
    {
        var user = HttpContext.Session.GetCurrentUser();
        if (user is null)
        {
            throw new InvalidOperationException("Sessao nao autenticada.");
        }

        return user;
    }
}

