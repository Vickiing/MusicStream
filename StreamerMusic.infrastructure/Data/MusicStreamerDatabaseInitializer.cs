using Microsoft.EntityFrameworkCore;

namespace MusicStreamer.infrastructure.Data;

public static class MusicStreamerDatabaseInitializer
{
    public static async Task MigrateAndSeedAsync(MusicStreamerDbContext context, string contentRootPath, CancellationToken cancellationToken = default)
    {
        await context.Database.MigrateAsync(cancellationToken);

        var scriptsDirectory = Path.Combine(contentRootPath, "scripts");
        await ExecuteSeedScriptAsync(context, Path.Combine(scriptsDirectory, "seed-catalogo.sql"), cancellationToken);
        await ExecuteSeedScriptAsync(context, Path.Combine(scriptsDirectory, "seed-musicas-50.sql"), cancellationToken);
    }

    private static async Task ExecuteSeedScriptAsync(MusicStreamerDbContext context, string scriptPath, CancellationToken cancellationToken)
    {
        if (!File.Exists(scriptPath))
        {
            throw new FileNotFoundException($"Seed script not found: {scriptPath}", scriptPath);
        }

        var script = await File.ReadAllTextAsync(scriptPath, cancellationToken);
        await context.Database.ExecuteSqlRawAsync(script, cancellationToken);
    }
}
