using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using MusicStreamer.Domain.Entities;
using MusicStreamer.Domain.ValueObjects;

namespace MusicStreamer.infrastructure.Data;

public sealed class MusicStreamerDbContext(DbContextOptions<MusicStreamerDbContext> options) : DbContext(options)
{
    public DbSet<UserAccount> UserAccounts => Set<UserAccount>();
    public DbSet<SubscriptionPlan> SubscriptionPlans => Set<SubscriptionPlan>();
    public DbSet<Artist> Artists => Set<Artist>();
    public DbSet<Album> Albums => Set<Album>();
    public DbSet<MusicTrack> MusicTracks => Set<MusicTrack>();
    public DbSet<Playlist> Playlists => Set<Playlist>();
    public DbSet<PlaylistTrack> PlaylistTracks => Set<PlaylistTrack>();
    public DbSet<FavoriteMusic> FavoriteMusics => Set<FavoriteMusic>();
    public DbSet<FavoriteBand> FavoriteBands => Set<FavoriteBand>();
    public DbSet<Merchant> Merchants => Set<Merchant>();
    public DbSet<Transaction> Transactions => Set<Transaction>();
    public DbSet<TransactionNotification> TransactionNotifications => Set<TransactionNotification>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        var emailConverter = new ValueConverter<EmailAddress, string>(
            value => value.Value,
            value => new EmailAddress(value));

        modelBuilder.Entity<UserAccount>(entity =>
        {
            entity.ToTable("UserAccounts");
            entity.HasKey(item => item.Id);
            entity.Property(item => item.DisplayName).HasMaxLength(120).IsRequired();
            entity.Property(item => item.PasswordHash).HasMaxLength(512).IsRequired();
            entity.Property(item => item.Status).HasConversion<int>().IsRequired();
            entity.Property(item => item.Email).HasConversion(emailConverter).HasMaxLength(180).IsRequired();
            entity.HasIndex(item => item.Email).IsUnique();
        });

        modelBuilder.Entity<SubscriptionPlan>(entity =>
        {
            entity.ToTable("SubscriptionPlans");
            entity.HasKey(item => item.Id);
            entity.Property(item => item.Name).HasMaxLength(80).IsRequired();
            entity.Property(item => item.MonthlyPrice).HasPrecision(18, 2);
            entity.Property(item => item.MaxTransactionAmount).HasPrecision(18, 2);
        });

        modelBuilder.Entity<Artist>(entity =>
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
            entity.HasOne(item => item.Artist).WithMany(item => item.Albums).HasForeignKey(item => item.ArtistId);
        });

        modelBuilder.Entity<MusicTrack>(entity =>
        {
            entity.ToTable("MusicTracks");
            entity.HasKey(item => item.Id);
            entity.Property(item => item.Title).HasMaxLength(150).IsRequired();
            entity.Property(item => item.NormalizedTitle).HasMaxLength(150).IsRequired();
            entity.HasIndex(item => item.NormalizedTitle);
            entity.HasIndex(item => new { item.ArtistId, item.NormalizedTitle });
            entity.HasOne(item => item.Artist).WithMany(item => item.Tracks).HasForeignKey(item => item.ArtistId);
            entity.HasOne(item => item.Album).WithMany(item => item.Tracks).HasForeignKey(item => item.AlbumId);
        });

        modelBuilder.Entity<Playlist>(entity =>
        {
            entity.ToTable("Playlists");
            entity.HasKey(item => item.Id);
            entity.Property(item => item.Name).HasMaxLength(120).IsRequired();
        });

        modelBuilder.Entity<PlaylistTrack>(entity =>
        {
            entity.ToTable("PlaylistTracks");
            entity.HasKey(item => item.Id);
            entity.HasIndex(item => new { item.PlaylistId, item.MusicId }).IsUnique();
            entity.HasOne(item => item.Music).WithMany().HasForeignKey(item => item.MusicId);
        });

        modelBuilder.Entity<FavoriteMusic>(entity =>
        {
            entity.ToTable("FavoriteMusics");
            entity.HasKey(item => item.Id);
            entity.HasIndex(item => new { item.UserAccountId, item.TrackId }).IsUnique();
        });

        modelBuilder.Entity<FavoriteBand>(entity =>
        {
            entity.ToTable("FavoriteBands");
            entity.HasKey(item => item.Id);
            entity.HasIndex(item => new { item.UserAccountId, item.ArtistId }).IsUnique();
        });

        modelBuilder.Entity<Merchant>(entity =>
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

        modelBuilder.Entity<TransactionNotification>(entity =>
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
