using MusicStreamer.App.Contracts;
using MusicStreamer.App.Services;
using MusicStreamer.Domain.Contracts;
using MusicStreamer.infrastructure;
using MusicStreamer.infrastructure.Repository;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddScoped<DataContext>();
builder.Services.AddScoped<IUsuarioRepository, UsuarioRepository>();
builder.Services.AddScoped<IUsuarioApp, UsuarioApp>();

var app = builder.Build();

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();
