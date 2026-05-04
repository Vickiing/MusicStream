using MusicStreamer.App.Contracts;
using MusicStreamer.App.Services;
using MusicStreamer.Domain.Contracts;
using MusicStreamer.Domain.Services;
using MusicStreamer.infrastructure;
using MusicStreamer.infrastructure.Repository;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddScoped<DataContext>();
builder.Services.AddScoped<IUsuarioRepository, UsuarioRepository>();
builder.Services.AddScoped<IUsuarioApplicationService, UsuarioApplicationService>();
builder.Services.AddScoped<IAssinaturaRepository, AssinaturaRepository>();
builder.Services.AddScoped<IPlanoAssinaturaDomainService, PlanoAssinaturaDomainService>();
builder.Services.AddScoped<MusicStreamer.App.Contracts.IPagamentoApplicationService, MusicStreamer.App.Services.PagamentoApplicationService>();
builder.Services.AddScoped<IAssinaturaApplicationService, AssinaturaApplicationService>();

var app = builder.Build();

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();
