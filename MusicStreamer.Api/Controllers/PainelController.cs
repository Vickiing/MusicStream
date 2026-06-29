using Microsoft.AspNetCore.Mvc;
using MusicStreamer.Api.Extensions;
using MusicStreamer.Api.Models;
using MusicStreamer.App.Contracts;
using MusicStreamer.App.DTOs;

namespace MusicStreamer.Api.Controllers;

[Route("app")]
public sealed class PainelController(
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
    public async Task<IActionResult> Search([FromQuery] string? term, [FromQuery] int page = 1, [FromQuery] string? message = null, CancellationToken cancellationToken = default)
    {
        PrepareShell("Buscar", "search");
        var model = await BuildModelAsync("search", cancellationToken, term, page, message);
        return View(model);
    }

    [HttpGet("plans")]
    public async Task<IActionResult> Plans([FromQuery] string? message, CancellationToken cancellationToken)
    {
        PrepareShell("Planos", "plans");
        var model = await BuildModelAsync("plans", cancellationToken, message: message);
        return View(model);
    }

    [HttpGet("plans/payment/{planId:guid}")]
    public async Task<IActionResult> PlanPayment(Guid planId, [FromQuery] string? message, CancellationToken cancellationToken)
    {
        PrepareShell("Pagamento", "plans");
        var user = RequireUser();

        var plan = await subscriptionService.GetPlanByIdAsync(planId, cancellationToken);
        if (plan is null)
        {
            return RedirectToAction(nameof(Plans), new { message = "Plano nao encontrado." });
        }

        var plans = await subscriptionService.GetPlansAsync(cancellationToken);
        var currentPlan = user.SubscriptionPlanId.HasValue
            ? plans.FirstOrDefault(item => item.Id == user.SubscriptionPlanId.Value)
            : null;

        ViewData["HasActiveSubscription"] = currentPlan is not null;
        ViewData["CurrentPlanName"] = currentPlan?.Name ?? string.Empty;

        ViewData["UseAppShell"] = true;
        ViewData["Title"] = "Pagamento";
        ViewData["ActiveSection"] = "plans";

        return View(new PagamentoPlanoViewModel
        {
            PlanId = plan.Id,
            PlanName = plan.Name,
            MonthlyPrice = plan.MonthlyPrice,
            PaymentAmount = 50m,
            StatusMessage = currentPlan is not null && plan.MonthlyPrice <= currentPlan.MonthlyPrice
                ? $"Voce ja possui um plano igual ou superior a {currentPlan.Name}."
                : message,
            IsUpgradeAllowed = currentPlan is null || plan.MonthlyPrice > currentPlan.MonthlyPrice
        });
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
    public async Task<IActionResult> ChoosePlan(EscolherPlanoViewModel model, CancellationToken cancellationToken)
    {
        var user = RequireUser();

        var plan = await subscriptionService.GetPlanByIdAsync(model.PlanId, cancellationToken);
        if (plan is null)
        {
            return RedirectToAction(nameof(Plans), new { message = "Plano nao encontrado." });
        }

        if (user.SubscriptionPlanId.HasValue)
        {
            var currentPlans = await subscriptionService.GetPlansAsync(cancellationToken);
            var currentPlan = currentPlans.FirstOrDefault(item => item.Id == user.SubscriptionPlanId.Value);
            if (currentPlan is not null && plan.MonthlyPrice <= currentPlan.MonthlyPrice)
            {
                return RedirectToAction(nameof(Plans), new { message = $"Voce ja possui um plano igual ou superior a {currentPlan.Name}." });
            }
        }

        return RedirectToAction(nameof(PlanPayment), new { planId = model.PlanId });
    }

    [HttpPost("subscriptions/payment")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> ConfirmPlanPayment(PagamentoPlanoViewModel model, CancellationToken cancellationToken)
    {
        var user = RequireUser();
        var plan = await subscriptionService.GetPlanByIdAsync(model.PlanId, cancellationToken);
        if (plan is null)
        {
            return RedirectToAction(nameof(Plans), new { message = "Plano nao encontrado." });
        }

        var currentPlans = await subscriptionService.GetPlansAsync(cancellationToken);
        var currentPlan = user.SubscriptionPlanId.HasValue
            ? currentPlans.FirstOrDefault(item => item.Id == user.SubscriptionPlanId.Value)
            : null;

        if (currentPlan is not null && plan.MonthlyPrice <= currentPlan.MonthlyPrice)
        {
            return RespondPlanPayment(model, plan, false, $"Voce ja possui um plano igual ou superior a {currentPlan.Name}.");
        }

        if (model.PaymentAmount != 50m)
        {
            return RespondPlanPayment(model, plan, false, "Valor invalido para a simulacao.");
        }

        var response = await subscriptionService.ChoosePlanAsync(new EscolherPlanoDto(user.UserId, model.PlanId), cancellationToken);
        if (response is null)
        {
            return RespondPlanPayment(model, plan, false, "Nao foi possivel ativar o plano.");
        }

        HttpContext.Session.SetCurrentUser(new UsuarioSessaoViewModel
        {
            UserId = user.UserId,
            DisplayName = user.DisplayName,
            Email = user.Email,
            Token = user.Token,
            SubscriptionPlanId = model.PlanId
        });

        return RespondPlanPayment(model, plan, true, $"Pagamento concluido. Plano {plan.Name} ativado.");
    }

    [HttpPost("playlists")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> CreatePlaylist(CriarPlaylistViewModel model, CancellationToken cancellationToken)
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
    public async Task<IActionResult> AddTrack(AdicionarMusicaNaPlaylistViewModel model, CancellationToken cancellationToken)
    {
        RequireUser();
        var playlist = await playlistService.AddTrackAsync(new AdicionarMusicaNaPlaylistDto(model.PlaylistId, model.TrackId), cancellationToken);
        if (playlist is null)
        {
            return RedirectToAction(nameof(Playlists), new { message = "Nao foi possivel adicionar a musica." });
        }

        return RedirectToAction(nameof(Playlists), new { message = "Musica associada a playlist." });
    }

    [HttpPost("favorites/tracks")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> FavoriteTrack(MusicaFavoritaViewModel model, CancellationToken cancellationToken)
    {
        var user = RequireUser();
        await favoritesService.FavoriteTrackAsync(user.UserId, model.TrackId, cancellationToken);
        return RedirectToAction(nameof(Search), new { term = model.SearchTerm, message = "Musica favoritada." });
    }

    [HttpPost("favorites/tracks/remove")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> UnfavoriteTrack(MusicaFavoritaViewModel model, CancellationToken cancellationToken)
    {
        var user = RequireUser();
        await favoritesService.UnfavoriteTrackAsync(user.UserId, model.TrackId, cancellationToken);
        return RedirectToAction(nameof(Favorites), new { message = "Musica removida dos favoritos." });
    }

    [HttpPost("favorites/artists")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> FavoriteArtist(BandaFavoritaViewModel model, CancellationToken cancellationToken)
    {
        var user = RequireUser();
        await favoritesService.FavoriteArtistAsync(user.UserId, model.ArtistId, cancellationToken);
        return RedirectToAction(nameof(Search), new { term = model.SearchTerm, message = "Banda favoritada." });
    }

    [HttpPost("favorites/artists/remove")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> UnfavoriteArtist(BandaFavoritaViewModel model, CancellationToken cancellationToken)
    {
        var user = RequireUser();
        await favoritesService.UnfavoriteArtistAsync(user.UserId, model.ArtistId, cancellationToken);
        return RedirectToAction(nameof(Favorites), new { message = "Banda removida dos favoritos." });
    }

    [HttpPost("transactions/authorize")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> AuthorizeTransaction(AutorizarTransacaoViewModel model, CancellationToken cancellationToken)
    {
        var user = RequireUser();
        var result = await transactionService.AuthorizeAsync(
            new AutorizarTransacaoDto(user.UserId, model.MerchantId, model.Amount, DateTimeOffset.UtcNow),
            cancellationToken);

        if (Request.IsJsonRequest())
        {
            return Ok(new
            {
                success = result is not null,
                message = result is null ? "Nao foi possivel processar a transacao." : "Transacao concluida."
            });
        }

        return RedirectToAction(nameof(Transactions), new { message = "Transacao concluida." });
    }

    private async Task<PainelViewModel> BuildModelAsync(
        string activeSection,
        CancellationToken cancellationToken,
        string? term = null,
        int page = 1,
        string? message = null)
    {
        var user = RequireUser();
        ResultadoBuscaCatalogoDto? searchResult = null;
        if (!string.IsNullOrWhiteSpace(term))
        {
            searchResult = await catalogService.SearchAsync(term, page, 10, cancellationToken);
        }

        var plans = await subscriptionService.GetPlansAsync(cancellationToken);
        var currentPlan = user.SubscriptionPlanId.HasValue
            ? plans.FirstOrDefault(plan => plan.Id == user.SubscriptionPlanId.Value)
            : null;

        ViewData["HasActiveSubscription"] = currentPlan is not null;
        ViewData["CurrentPlanName"] = currentPlan?.Name ?? string.Empty;

        return new PainelViewModel
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
            Plans = plans,
            Playlists = await playlistService.GetByUserAsync(user.UserId, cancellationToken),
            Merchants = await merchantService.GetAllAsync(cancellationToken),
            Transactions = await transactionService.GetByUserAsync(user.UserId, cancellationToken),
            SearchResult = searchResult,
            Favorites = await favoritesService.GetByUserAsync(user.UserId, cancellationToken),
            HasActiveSubscription = currentPlan is not null,
            CurrentPlanName = currentPlan?.Name ?? string.Empty,
            CurrentPlanMonthlyPrice = currentPlan?.MonthlyPrice ?? 0m,
            SearchPage = searchResult?.Page ?? page,
            SearchPageSize = searchResult?.PageSize ?? 10
        };
    }

    private void PrepareShell(string title, string activeSection)
    {
        ViewData["UseAppShell"] = true;
        ViewData["Title"] = title;
        ViewData["ActiveSection"] = activeSection;
    }

    private UsuarioSessaoViewModel RequireUser()
    {
        var user = HttpContext.Session.GetCurrentUser();
        if (user is null)
        {
            throw new InvalidOperationException("Sessao nao autenticada.");
        }

        return user;
    }

    private IActionResult RespondPlanPayment(PagamentoPlanoViewModel model, PlanoAssinaturaDto plan, bool success, string message)
    {
        if (Request.IsJsonRequest())
        {
            return Ok(new
            {
                success,
                message,
                planId = plan.Id,
                planName = plan.Name,
                hasActiveSubscription = success,
                currentPlanName = success ? plan.Name : string.Empty
            });
        }

        return RedirectToAction(nameof(PlanPayment), new { planId = model.PlanId, message });
    }
}

file static class HttpRequestExtensions
{
    public static bool IsJsonRequest(this HttpRequest request)
    {
        if (request.Headers.TryGetValue("X-Requested-With", out var requestedWith) && requestedWith == "fetch")
        {
            return true;
        }

        return request.Headers.TryGetValue("Accept", out var acceptValues)
            && acceptValues.Any(value => value?.Contains("application/json", StringComparison.OrdinalIgnoreCase) == true);
    }
}
