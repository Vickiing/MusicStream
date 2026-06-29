using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using MusicStreamer.Domain.Entities;
using MusicStreamer.Domain.Enums;
using MusicStreamer.Domain.ValueObjects;
using MusicStreamer.infrastructure.Data;

namespace MusicStreamer.infrastructure.Migrations;

[DbContext(typeof(MusicStreamerDbContext))]
public sealed class MusicStreamerDbContextModelSnapshot : ModelSnapshot
{
    protected override void BuildModel(ModelBuilder modelBuilder)
    {
        modelBuilder.HasAnnotation("ProductVersion", "10.0.7");

        var emailConverter = new ValueConverter<EmailAddress, string>(
            value => value.Value,
            value => new EmailAddress(value));

        modelBuilder.Entity("MusicStreamer.Domain.Entities.Album", b =>
        {
            b.Property<Guid>("Id").ValueGeneratedOnAdd().HasColumnType("uniqueidentifier");
            b.Property<Guid>("ArtistId").HasColumnType("uniqueidentifier");
            b.Property<int>("ReleaseYear").HasColumnType("int");
            b.Property<string>("NormalizedTitle").IsRequired().HasMaxLength(150).HasColumnType("nvarchar(150)");
            b.Property<string>("Title").IsRequired().HasMaxLength(150).HasColumnType("nvarchar(150)");
            b.HasKey("Id");
            b.HasIndex("ArtistId");
            b.HasIndex("NormalizedTitle");
            b.ToTable("Albums");
        });

        modelBuilder.Entity("MusicStreamer.Domain.Entities.Artist", b =>
        {
            b.Property<Guid>("Id").ValueGeneratedOnAdd().HasColumnType("uniqueidentifier");
            b.Property<string>("Name").IsRequired().HasMaxLength(150).HasColumnType("nvarchar(150)");
            b.Property<string>("NormalizedName").IsRequired().HasMaxLength(150).HasColumnType("nvarchar(150)");
            b.HasKey("Id");
            b.HasIndex("NormalizedName");
            b.ToTable("Artists");
        });

        modelBuilder.Entity("MusicStreamer.Domain.Entities.FavoriteBand", b =>
        {
            b.Property<Guid>("Id").ValueGeneratedOnAdd().HasColumnType("uniqueidentifier");
            b.Property<DateTimeOffset>("CreatedAtUtc").HasColumnType("datetimeoffset");
            b.Property<Guid>("ArtistId").HasColumnType("uniqueidentifier");
            b.Property<Guid>("UserAccountId").HasColumnType("uniqueidentifier");
            b.HasKey("Id");
            b.HasIndex("UserAccountId", "ArtistId").IsUnique();
            b.ToTable("FavoriteBands");
        });

        modelBuilder.Entity("MusicStreamer.Domain.Entities.FavoriteMusic", b =>
        {
            b.Property<Guid>("Id").ValueGeneratedOnAdd().HasColumnType("uniqueidentifier");
            b.Property<DateTimeOffset>("CreatedAtUtc").HasColumnType("datetimeoffset");
            b.Property<Guid>("TrackId").HasColumnType("uniqueidentifier");
            b.Property<Guid>("UserAccountId").HasColumnType("uniqueidentifier");
            b.HasKey("Id");
            b.HasIndex("UserAccountId", "TrackId").IsUnique();
            b.ToTable("FavoriteMusics");
        });

        modelBuilder.Entity("MusicStreamer.Domain.Entities.Merchant", b =>
        {
            b.Property<Guid>("Id").ValueGeneratedOnAdd().HasColumnType("uniqueidentifier");
            b.Property<string>("Category").IsRequired().HasColumnType("nvarchar(max)");
            b.Property<bool>("IsActive").HasColumnType("bit");
            b.Property<string>("Name").IsRequired().HasMaxLength(120).HasColumnType("nvarchar(120)");
            b.HasKey("Id");
            b.ToTable("Merchants");
        });

        modelBuilder.Entity("MusicStreamer.Domain.Entities.MusicTrack", b =>
        {
            b.Property<Guid>("Id").ValueGeneratedOnAdd().HasColumnType("uniqueidentifier");
            b.Property<Guid>("AlbumId").HasColumnType("uniqueidentifier");
            b.Property<int>("DurationSeconds").HasColumnType("int");
            b.Property<Guid>("ArtistId").HasColumnType("uniqueidentifier");
            b.Property<string>("NormalizedTitle").IsRequired().HasMaxLength(150).HasColumnType("nvarchar(150)");
            b.Property<string>("Title").IsRequired().HasMaxLength(150).HasColumnType("nvarchar(150)");
            b.HasKey("Id");
            b.HasIndex("AlbumId");
            b.HasIndex("ArtistId", "NormalizedTitle");
            b.HasIndex("NormalizedTitle");
            b.ToTable("MusicTracks");
        });

        modelBuilder.Entity("MusicStreamer.Domain.Entities.Playlist", b =>
        {
            b.Property<Guid>("Id").ValueGeneratedOnAdd().HasColumnType("uniqueidentifier");
            b.Property<DateTimeOffset>("CreatedAtUtc").HasColumnType("datetimeoffset");
            b.Property<Guid>("UserAccountId").HasColumnType("uniqueidentifier");
            b.Property<string>("Name").IsRequired().HasMaxLength(120).HasColumnType("nvarchar(120)");
            b.HasKey("Id");
            b.ToTable("Playlists");
        });

        modelBuilder.Entity("MusicStreamer.Domain.Entities.PlaylistTrack", b =>
        {
            b.Property<Guid>("Id").ValueGeneratedOnAdd().HasColumnType("uniqueidentifier");
            b.Property<DateTimeOffset>("AddedAtUtc").HasColumnType("datetimeoffset");
            b.Property<Guid>("MusicId").HasColumnType("uniqueidentifier");
            b.Property<Guid>("PlaylistId").HasColumnType("uniqueidentifier");
            b.HasKey("Id");
            b.HasIndex("MusicId");
            b.HasIndex("PlaylistId", "MusicId").IsUnique();
            b.ToTable("PlaylistTracks");
        });

        modelBuilder.Entity("MusicStreamer.Domain.Entities.SubscriptionPlan", b =>
        {
            b.Property<Guid>("Id").ValueGeneratedOnAdd().HasColumnType("uniqueidentifier");
            b.Property<bool>("AllowsNightTransactions").HasColumnType("bit");
            b.Property<bool>("IsActive").HasColumnType("bit");
            b.Property<decimal>("MaxTransactionAmount").HasColumnType("decimal(18,2)");
            b.Property<decimal>("MonthlyPrice").HasColumnType("decimal(18,2)");
            b.Property<string>("Name").IsRequired().HasMaxLength(80).HasColumnType("nvarchar(80)");
            b.HasKey("Id");
            b.ToTable("SubscriptionPlans");
        });

        modelBuilder.Entity("MusicStreamer.Domain.Entities.Transaction", b =>
        {
            b.Property<Guid>("Id").ValueGeneratedOnAdd().HasColumnType("uniqueidentifier");
            b.Property<decimal>("Amount").HasColumnType("decimal(18,2)");
            b.Property<DateTimeOffset?>("AuthorizedAtUtc").HasColumnType("datetimeoffset");
            b.Property<Guid>("MerchantId").HasColumnType("uniqueidentifier");
            b.Property<string>("Reason").IsRequired().HasMaxLength(200).HasColumnType("nvarchar(200)");
            b.Property<DateTimeOffset>("RequestedAtUtc").HasColumnType("datetimeoffset");
            b.Property<int>("Status").HasColumnType("int");
            b.Property<Guid>("UserAccountId").HasColumnType("uniqueidentifier");
            b.HasKey("Id");
            b.HasIndex("UserAccountId", "RequestedAtUtc");
            b.ToTable("Transactions");
        });

        modelBuilder.Entity("MusicStreamer.Domain.Entities.TransactionNotification", b =>
        {
            b.Property<Guid>("Id").ValueGeneratedOnAdd().HasColumnType("uniqueidentifier");
            b.Property<DateTimeOffset>("SentAtUtc").HasColumnType("datetimeoffset");
            b.Property<string>("Channel").IsRequired().HasMaxLength(50).HasColumnType("nvarchar(50)");
            b.Property<string>("Message").IsRequired().HasMaxLength(300).HasColumnType("nvarchar(300)");
            b.Property<Guid>("TransactionId").HasColumnType("uniqueidentifier");
            b.Property<string>("Recipient").IsRequired().HasMaxLength(180).HasColumnType("nvarchar(180)");
            b.Property<int>("Status").HasColumnType("int");
            b.HasKey("Id");
            b.HasIndex("TransactionId");
            b.ToTable("TransactionNotifications");
        });

        modelBuilder.Entity("MusicStreamer.Domain.Entities.UserAccount", b =>
        {
            b.Property<Guid>("Id").ValueGeneratedOnAdd().HasColumnType("uniqueidentifier");
            b.Property<DateTimeOffset>("CreatedAtUtc").HasColumnType("datetimeoffset");
            b.Property<string>("DisplayName").IsRequired().HasMaxLength(120).HasColumnType("nvarchar(120)");
            b.Property<EmailAddress>("Email").IsRequired().HasConversion(emailConverter, null).HasMaxLength(180).HasColumnType("nvarchar(180)");
            b.Property<DateTimeOffset?>("LastLoginAtUtc").HasColumnType("datetimeoffset");
            b.Property<string>("PasswordHash").IsRequired().HasMaxLength(512).HasColumnType("nvarchar(512)");
            b.Property<Guid?>("SubscriptionPlanId").HasColumnType("uniqueidentifier");
            b.Property<int>("Status").HasColumnType("int");
            b.HasKey("Id");
            b.HasIndex("Email").IsUnique();
            b.ToTable("UserAccounts");
        });

        modelBuilder.Entity("MusicStreamer.Domain.Entities.Album", b =>
        {
            b.HasOne("MusicStreamer.Domain.Entities.Artist", "Artist")
                .WithMany("Albums")
                .HasForeignKey("ArtistId")
                .OnDelete(DeleteBehavior.Cascade)
                .IsRequired();

            b.Navigation("Artist");
        });

        modelBuilder.Entity("MusicStreamer.Domain.Entities.MusicTrack", b =>
        {
            b.HasOne("MusicStreamer.Domain.Entities.Album", "Album")
                .WithMany("Tracks")
                .HasForeignKey("AlbumId")
                .OnDelete(DeleteBehavior.Cascade)
                .IsRequired();

            b.HasOne("MusicStreamer.Domain.Entities.Artist", "Artist")
                .WithMany("Tracks")
                .HasForeignKey("ArtistId")
                .OnDelete(DeleteBehavior.Cascade)
                .IsRequired();

            b.Navigation("Album");
            b.Navigation("Artist");
        });

        modelBuilder.Entity("MusicStreamer.Domain.Entities.PlaylistTrack", b =>
        {
            b.HasOne("MusicStreamer.Domain.Entities.MusicTrack", "Music")
                .WithMany()
                .HasForeignKey("MusicId")
                .OnDelete(DeleteBehavior.Cascade)
                .IsRequired();

            b.Navigation("Music");
        });

        modelBuilder.Entity("MusicStreamer.Domain.Entities.TransactionNotification", b =>
        {
            b.HasOne("MusicStreamer.Domain.Entities.Transaction", "Transaction")
                .WithMany("Notifications")
                .HasForeignKey("TransactionId")
                .OnDelete(DeleteBehavior.Cascade)
                .IsRequired();

            b.Navigation("Transaction");
        });

        modelBuilder.Entity("MusicStreamer.Domain.Entities.UserAccount", b =>
        {
            b.HasOne("MusicStreamer.Domain.Entities.SubscriptionPlan", null)
                .WithMany()
                .HasForeignKey("SubscriptionPlanId");
        });

        modelBuilder.Entity("MusicStreamer.Domain.Entities.Album", b =>
        {
            b.Navigation("Tracks");
        });

        modelBuilder.Entity("MusicStreamer.Domain.Entities.Artist", b =>
        {
            b.Navigation("Albums");
            b.Navigation("Tracks");
        });

        modelBuilder.Entity("MusicStreamer.Domain.Entities.Playlist", b =>
        {
            b.Navigation("Tracks");
        });

        modelBuilder.Entity("MusicStreamer.Domain.Entities.Transaction", b =>
        {
            b.Navigation("Notifications");
        });
    }
}
