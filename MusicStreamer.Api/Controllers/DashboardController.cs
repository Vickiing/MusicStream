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
    public IActionResult Root() => RedirectToAction(nameof(Home));

    [HttpGet("dashboard")]
    public IActionResult Index() => RedirectToAction(nameof(Home));

    [HttpGet("home")]
    public async Task<IActionResult> Home(CancellationToken cancellationToken)
    {
        PrepareShell("Inicio", "home");
        var model = await BuildModelAsync("home", cancellationToken);
        return View(model);
    }

    [HttpGet("catalog")]
    public async Task<IActionResult> Catalog(CancellationToken cancellationToken)
    {
        PrepareShell("Catalogo", "catalog");
        var model = await BuildModelAsync("catalog", cancellationToken);
        return View(model);
    }

    [HttpGet("search")]
    public async Task<IActionResult> Search([FromQuery] string? term, [FromQuery] string? message, CancellationToken cancellationToken)
    {
        PrepareShell("Buscar", "search");
        var model = await BuildModelAsync("search", cancellationToken, term, message);
        return View(model);
    }

    [HttpGet("plans")]
    public async Task<IActionResult> Plans([FromQuery] string? message, CancellationToken cancellationToken)
    {
        PrepareShell("Planos", "plans");
        var model = await BuildModelAsync("plans", cancellationToken, message: message);
        return View(model);
    }

    [HttpGet("playlists")]
    public async Task<IActionResult> Playlists([FromQuery] string? message, CancellationToken cancellationToken)
    {
        PrepareShell("Playlists", "playlists");
        var model = await BuildModelAsync("playlists", cancellationToken, message: message);
        return View(model);
    }

    [HttpGet("favorites")]
    public async Task<IActionResult> Favorites([FromQuery] string? message, CancellationToken cancellationToken)
    {
        PrepareShell("Favoritos", "favorites");
        var model = await BuildModelAsync("favorites", cancellationToken, message: message);
        return View(model);
    }

    [HttpGet("transactions")]
    public async Task<IActionResult> Transactions([FromQuery] string? message, CancellationToken cancellationToken)
    {
        PrepareShell("Transacoes", "transactions");
        var model = await BuildModelAsync("transactions", cancellationToken, message: message);
        return View(model);
    }

    [HttpPost("subscriptions/choose-plan")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> ChoosePlan(ChoosePlanViewModel model, CancellationToken cancellationToken)
    {
        var user = RequireUser();
        await subscriptionService.ChoosePlanAsync(new EscolherPlanoDto(user.UserId, model.PlanId), cancellationToken);
        return RedirectToAction(nameof(Plans), new { message = "Pagamento simulado com sucesso. Plano ativado." });
    }

    [HttpPost("playlists")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> CreatePlaylist(CreatePlaylistViewModel model, CancellationToken cancellationToken)
    {
        var user = RequireUser();
        if (!ModelState.IsValid)
        {
            return RedirectToAction(nameof(Playlists), new { message = "Informe o nome da playlist." });
        }

        await playlistService.CreateAsync(new CriarPlaylistDto(user.UserId, model.Name), cancellationToken);
        return RedirectToAction(nameof(Playlists), new { message = "Playlist criada." });
    }

    [HttpPost("playlists/add-track")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> AddTrack(AddTrackToPlaylistViewModel model, CancellationToken cancellationToken)
    {
        RequireUser();
        await playlistService.AddTrackAsync(new AdicionarMusicaNaPlaylistDto(model.PlaylistId, model.TrackId), cancellationToken);
        return RedirectToAction(nameof(Playlists), new { message = "Musica associada a playlist." });
    }

    [HttpPost("favorites/tracks")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> FavoriteTrack(FavoriteTrackViewModel model, CancellationToken cancellationToken)
    {
        var user = RequireUser();
        await favoritesService.FavoriteTrackAsync(user.UserId, model.TrackId, cancellationToken);
        return RedirectToAction(nameof(Search), new { term = model.SearchTerm, message = "Musica favoritada." });
    }

    [HttpPost("favorites/tracks/remove")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> UnfavoriteTrack(FavoriteTrackViewModel model, CancellationToken cancellationToken)
    {
        var user = RequireUser();
        await favoritesService.UnfavoriteTrackAsync(user.UserId, model.TrackId, cancellationToken);
        return RedirectToAction(nameof(Favorites), new { message = "Musica removida dos favoritos." });
    }

    [HttpPost("favorites/artists")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> FavoriteArtist(FavoriteArtistViewModel model, CancellationToken cancellationToken)
    {
        var user = RequireUser();
        await favoritesService.FavoriteArtistAsync(user.UserId, model.ArtistId, cancellationToken);
        return RedirectToAction(nameof(Search), new { term = model.SearchTerm, message = "Banda favoritada." });
    }

    [HttpPost("favorites/artists/remove")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> UnfavoriteArtist(FavoriteArtistViewModel model, CancellationToken cancellationToken)
    {
        var user = RequireUser();
        await favoritesService.UnfavoriteArtistAsync(user.UserId, model.ArtistId, cancellationToken);
        return RedirectToAction(nameof(Favorites), new { message = "Banda removida dos favoritos." });
    }

    [HttpPost("transactions/authorize")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> AuthorizeTransaction(AuthorizeTransactionViewModel model, CancellationToken cancellationToken)
    {
        var user = RequireUser();
        await transactionService.AuthorizeAsync(
            new AutorizarTransacaoDto(user.UserId, model.MerchantId, model.Amount, model.RequestedAtUtc ?? DateTimeOffset.UtcNow),
            cancellationToken);

        return RedirectToAction(nameof(Transactions), new { message = "Transacao simulada com sucesso." });
    }

    private async Task<DashboardViewModel> BuildModelAsync(
        string activeSection,
        CancellationToken cancellationToken,
        string? term = null,
        string? message = null)
    {
        var user = RequireUser();
        ResultadoBuscaCatalogoDto? searchResult = null;
        if (!string.IsNullOrWhiteSpace(term))
        {
            searchResult = await catalogService.SearchAsync(term, cancellationToken);
        }

        return new DashboardViewModel
        {
            UserId = user.UserId,
            DisplayName = user.DisplayName,
            Email = user.Email,
            Token = user.Token,
            SearchTerm = term,
            StatusMessage = message,
            ActiveSection = activeSection,
            Artists = await catalogService.GetArtistsAsync(cancellationToken),
            Albums = await catalogService.GetAlbumsAsync(cancellationToken),
            Tracks = await catalogService.GetTracksAsync(cancellationToken),
            Plans = await subscriptionService.GetPlansAsync(cancellationToken),
            Playlists = await playlistService.GetByUserAsync(user.UserId, cancellationToken),
            Merchants = await merchantService.GetAllAsync(cancellationToken),
            Transactions = await transactionService.GetByUserAsync(user.UserId, cancellationToken),
            SearchResult = searchResult,
            Favorites = await favoritesService.GetByUserAsync(user.UserId, cancellationToken)
        };
    }

    private void PrepareShell(string title, string activeSection)
    {
        ViewData["UseAppShell"] = true;
        ViewData["Title"] = title;
        ViewData["ActiveSection"] = activeSection;
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
