using Microsoft.EntityFrameworkCore;
using MusicStreamer.Domain.Entities;
using MusicStreamer.Domain.ValueObjects;

namespace MusicStreamer.infrastructure.Data;

public sealed class MusicStreamerDbContext(DbContextOptions<MusicStreamerDbContext> options) : DbContext(options)
{
    public DbSet<ContaUsuario> UserAccounts => Set<ContaUsuario>();
    public DbSet<PlanoAssinatura> SubscriptionPlans => Set<PlanoAssinatura>();
    public DbSet<Banda> Artists => Set<Banda>();
    public DbSet<Album> Albums => Set<Album>();
    public DbSet<Musica> MusicTracks => Set<Musica>();
    public DbSet<Playlist> Playlists => Set<Playlist>();
    public DbSet<FaixaPlaylist> PlaylistTracks => Set<FaixaPlaylist>();
    public DbSet<MusicaFavorita> FavoriteMusics => Set<MusicaFavorita>();
    public DbSet<BandaFavorita> FavoriteBands => Set<BandaFavorita>();
    public DbSet<Comerciante> Merchants => Set<Comerciante>();
    public DbSet<Transaction> Transactions => Set<Transaction>();
    public DbSet<NotificacaoTransacao> TransactionNotifications => Set<NotificacaoTransacao>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<ContaUsuario>(entity =>
        {
            entity.ToTable("UserAccounts");
            entity.HasKey(item => item.Id);
            entity.Property(item => item.DisplayName).HasMaxLength(120).IsRequired();
            entity.Property(item => item.PasswordHash).HasMaxLength(512).IsRequired();
            entity.Property(item => item.Status).HasConversion<int>().IsRequired();
            entity.Property(item => item.Email)
                .HasConversion(
                    value => value.Value,
                    value => new EnderecoEmail(value))
                .HasMaxLength(180)
                .IsRequired();
            entity.HasIndex(item => item.Email).IsUnique();
        });

        modelBuilder.Entity<PlanoAssinatura>(entity =>
        {
            entity.ToTable("SubscriptionPlans");
            entity.HasKey(item => item.Id);
            entity.Property(item => item.Name).HasMaxLength(80).IsRequired();
            entity.Property(item => item.MonthlyPrice).HasPrecision(18, 2);
            entity.Property(item => item.MaxTransactionAmount).HasPrecision(18, 2);
        });

        modelBuilder.Entity<Banda>(entity =>
        {
            entity.ToTable("Artists");
            entity.HasKey(item => item.Id);
            entity.Property(item => item.Name).HasMaxLength(150).IsRequired();
            entity.Property(item => item.NormalizedName).HasMaxLength(150).IsRequired();
            entity.HasIndex(item => item.NormalizedName);
        });

        modelBuilder.Entity<Album>(entity =>
        {
            entity.ToTable("Albums");
            entity.HasKey(item => item.Id);
            entity.Property(item => item.Title).HasMaxLength(150).IsRequired();
            entity.Property(item => item.NormalizedTitle).HasMaxLength(150).IsRequired();
            entity.HasIndex(item => item.NormalizedTitle);
            entity.HasOne(item => item.Banda).WithMany(item => item.Albums).HasForeignKey(item => item.ArtistId);
        });

        modelBuilder.Entity<Musica>(entity =>
        {
            entity.ToTable("MusicTracks");
            entity.HasKey(item => item.Id);
            entity.Property(item => item.Title).HasMaxLength(150).IsRequired();
            entity.Property(item => item.NormalizedTitle).HasMaxLength(150).IsRequired();
            entity.HasIndex(item => item.NormalizedTitle);
            entity.HasIndex(item => new { item.ArtistId, item.NormalizedTitle });
            entity.HasOne(item => item.Banda)
                .WithMany(item => item.Tracks)
                .HasForeignKey(item => item.ArtistId)
                .OnDelete(DeleteBehavior.NoAction);
            entity.HasOne(item => item.Album).WithMany(item => item.Tracks).HasForeignKey(item => item.AlbumId);
        });

        modelBuilder.Entity<Playlist>(entity =>
        {
            entity.ToTable("Playlists");
            entity.HasKey(item => item.Id);
            entity.Property(item => item.Name).HasMaxLength(120).IsRequired();
            entity.HasMany(item => item.Tracks)
                .WithOne(item => item.Playlist)
                .HasForeignKey(item => item.PlaylistId)
                .OnDelete(DeleteBehavior.Cascade);
        });

        modelBuilder.Entity<FaixaPlaylist>(entity =>
        {
            entity.ToTable("PlaylistTracks");
            entity.HasKey(item => item.Id);
            entity.HasIndex(item => new { item.PlaylistId, item.MusicId }).IsUnique();
            entity.HasOne(item => item.Music).WithMany().HasForeignKey(item => item.MusicId);
        });

        modelBuilder.Entity<MusicaFavorita>(entity =>
        {
            entity.ToTable("FavoriteMusics");
            entity.HasKey(item => item.Id);
            entity.HasIndex(item => new { item.UserAccountId, item.TrackId }).IsUnique();
        });

        modelBuilder.Entity<BandaFavorita>(entity =>
        {
            entity.ToTable("FavoriteBands");
            entity.HasKey(item => item.Id);
            entity.HasIndex(item => new { item.UserAccountId, item.ArtistId }).IsUnique();
        });

        modelBuilder.Entity<Comerciante>(entity =>
        {
            entity.ToTable("Merchants");
            entity.HasKey(item => item.Id);
            entity.Property(item => item.Name).HasMaxLength(120).IsRequired();
        });

        modelBuilder.Entity<Transaction>(entity =>
        {
            entity.ToTable("Transactions");
            entity.HasKey(item => item.Id);
            entity.Property(item => item.Status).HasConversion<int>().IsRequired();
            entity.Property(item => item.Amount).HasColumnType("decimal(18,2)");
            entity.Property(item => item.Reason).HasMaxLength(200).IsRequired();
            entity.HasIndex(item => new { item.UserAccountId, item.RequestedAtUtc });
        });

        modelBuilder.Entity<NotificacaoTransacao>(entity =>
        {
            entity.ToTable("TransactionNotifications");
            entity.HasKey(item => item.Id);
            entity.Property(item => item.Status).HasConversion<int>().IsRequired();
            entity.Property(item => item.Recipient).HasMaxLength(180).IsRequired();
            entity.Property(item => item.Channel).HasMaxLength(50).IsRequired();
            entity.Property(item => item.Message).HasMaxLength(300).IsRequired();
        });
    }
}
