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
    options.UseSqlServer(azureSqlConnection));

builder.Services.AddScoped<IUserAccountRepository, UserAccountRepository>();
builder.Services.AddScoped<ISubscriptionPlanRepository, SubscriptionPlanRepository>();
builder.Services.AddScoped<ICatalogRepository, CatalogRepository>();
builder.Services.AddScoped<IPlaylistRepository, PlaylistRepository>();
builder.Services.AddScoped<IFavoritesRepository, FavoritesRepository>();
builder.Services.AddScoped<IMerchantRepository, MerchantRepository>();
builder.Services.AddScoped<ITransactionRepository, TransactionRepository>();

builder.Services.AddScoped<IPasswordHasher, Pbkdf2PasswordHasher>();
builder.Services.AddScoped<ITokenService, JwtTokenService>();
builder.Services.AddScoped<ITransactionAuthorizationDomainService, TransactionAuthorizationDomainService>();

builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped<ISubscriptionService, SubscriptionService>();
builder.Services.AddScoped<ICatalogService, CatalogService>();
builder.Services.AddScoped<IPlaylistService, PlaylistService>();
builder.Services.AddScoped<IFavoritesService, FavoritesService>();
builder.Services.AddScoped<ITransactionService, TransactionService>();

builder.Services
    .AddAuthentication("Bearer")
    .AddScheme<AuthenticationSchemeOptions, SimpleBearerAuthenticationHandler>("Bearer", _ => { });

builder.Services.AddAuthorization();

var app = builder.Build();

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseSession();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Dashboard}/{action=Root}/{id?}");
app.MapControllers();
app.Run();
