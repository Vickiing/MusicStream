using Microsoft.AspNetCore.Authentication;
using Microsoft.EntityFrameworkCore;
using MusicStreamer.App.Abstractions;
using MusicStreamer.App.Contracts;
using MusicStreamer.App.Services;
using MusicStreamer.Api.Security;
using MusicStreamer.Domain.Repositories;
using MusicStreamer.Domain.Services;
using MusicStreamer.infrastructure.Data;
using MusicStreamer.infrastructure.Repositories;
using MusicStreamer.infrastructure.Security;

var builder = WebApplication.CreateBuilder(args);

builder.Logging.ClearProviders();
builder.Logging.AddConsole();

builder.Services.AddControllersWithViews();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddDistributedMemoryCache();
builder.Services.AddSession(options =>
{
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
    options.IdleTimeout = TimeSpan.FromHours(8);
});

var azureSqlConnection = builder.Configuration.GetConnectionString("AzureSqlConnection")
    ?? throw new InvalidOperationException("Configure ConnectionStrings:AzureSqlConnection com a connection string do Azure SQL Server.");

builder.Services.AddDbContext<MusicStreamerDbContext>(options =>
    options.UseSqlServer(azureSqlConnection, sqlServerOptions =>
    {
        sqlServerOptions.EnableRetryOnFailure(5, TimeSpan.FromSeconds(5), null);
    }));

builder.Services.AddScoped<IContaUsuarioRepository, UserAccountRepository>();
builder.Services.AddScoped<IPlanoAssinaturaRepository, SubscriptionPlanRepository>();
builder.Services.AddScoped<ICatalogoRepository, CatalogRepository>();
builder.Services.AddScoped<IPlaylistRepository, PlaylistRepository>();
builder.Services.AddScoped<IRepositorioFavoritos, FavoritesRepository>();
builder.Services.AddScoped<IComercianteRepository, MerchantRepository>();
builder.Services.AddScoped<ITransacaoRepository, TransactionRepository>();

builder.Services.AddScoped<IHashSenha, HashSenhaPbkdf2>();
builder.Services.AddScoped<IServicoToken, ServicoTokenBearerSimples>();
builder.Services.AddScoped<IServicoAutorizacaoTransacao, ServicoAutorizacaoTransacao>();

builder.Services.AddScoped<IServicoAutenticacao, AuthService>();
builder.Services.AddScoped<IServicoPlanosAssinatura, SubscriptionService>();
builder.Services.AddScoped<IServicoCatalogo, CatalogService>();
builder.Services.AddScoped<IServicoComerciantes, MerchantService>();
builder.Services.AddScoped<IServicoPlaylist, PlaylistService>();
builder.Services.AddScoped<IServicoFavoritos, FavoritesService>();
builder.Services.AddScoped<IServicoTransacoes, TransactionService>();

builder.Services
    .AddAuthentication("Bearer")
    .AddScheme<AuthenticationSchemeOptions, ManipuladorAutenticacaoBearerSimples>("Bearer", _ => { });

builder.Services.AddAuthorization();

var app = builder.Build();

await using (var scope = app.Services.CreateAsyncScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<MusicStreamerDbContext>();
    await MusicStreamerDatabaseInitializer.MigrateAndSeedAsync(dbContext, app.Environment.ContentRootPath);
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseSession();
app.UseAuthentication();
app.UseAuthorization();
app.MapGet("/", () => Results.Redirect("/app/account/login"));
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Dashboard}/{action=Root}/{id?}");
app.MapControllers();
app.Run();

