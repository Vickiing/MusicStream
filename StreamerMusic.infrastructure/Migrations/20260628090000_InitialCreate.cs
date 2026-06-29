using System;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using MusicStreamer.infrastructure.Data;

namespace MusicStreamer.infrastructure.Migrations;

[DbContext(typeof(MusicStreamerDbContext))]
[Migration("20260628090000_InitialCreate")]
public partial class InitialCreate : Migration
{
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.CreateTable(
            name: "Artists",
            columns: table => new
            {
                Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                Name = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                NormalizedName = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false)
            },
            constraints: table => table.PrimaryKey("PK_Artists", x => x.Id));

        migrationBuilder.CreateTable(
            name: "Merchants",
            columns: table => new
            {
                Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                Name = table.Column<string>(type: "nvarchar(120)", maxLength: 120, nullable: false),
                Category = table.Column<string>(type: "nvarchar(max)", nullable: false),
                IsActive = table.Column<bool>(type: "bit", nullable: false)
            },
            constraints: table => table.PrimaryKey("PK_Merchants", x => x.Id));

        migrationBuilder.CreateTable(
            name: "SubscriptionPlans",
            columns: table => new
            {
                Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                Name = table.Column<string>(type: "nvarchar(80)", maxLength: 80, nullable: false),
                MonthlyPrice = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                MaxTransactionAmount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                AllowsNightTransactions = table.Column<bool>(type: "bit", nullable: false),
                IsActive = table.Column<bool>(type: "bit", nullable: false)
            },
            constraints: table => table.PrimaryKey("PK_SubscriptionPlans", x => x.Id));

        migrationBuilder.CreateTable(
            name: "UserAccounts",
            columns: table => new
            {
                Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                DisplayName = table.Column<string>(type: "nvarchar(120)", maxLength: 120, nullable: false),
                Email = table.Column<string>(type: "nvarchar(180)", maxLength: 180, nullable: false),
                PasswordHash = table.Column<string>(type: "nvarchar(512)", maxLength: 512, nullable: false),
                Status = table.Column<int>(type: "int", nullable: false),
                SubscriptionPlanId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                CreatedAtUtc = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                LastLoginAtUtc = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true)
            },
            constraints: table => table.PrimaryKey("PK_UserAccounts", x => x.Id));

        migrationBuilder.CreateTable(
            name: "Albums",
            columns: table => new
            {
                Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                ArtistId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                Title = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                NormalizedTitle = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                ReleaseYear = table.Column<int>(type: "int", nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_Albums", x => x.Id);
                table.ForeignKey(
                    name: "FK_Albums_Artists_ArtistId",
                    column: x => x.ArtistId,
                    principalTable: "Artists",
                    principalColumn: "Id",
                    onDelete: ReferentialAction.Cascade);
            });

        migrationBuilder.CreateTable(
            name: "FavoriteBands",
            columns: table => new
            {
                Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                UserAccountId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                ArtistId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                CreatedAtUtc = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false)
            },
            constraints: table => table.PrimaryKey("PK_FavoriteBands", x => x.Id));

        migrationBuilder.CreateTable(
            name: "Playlists",
            columns: table => new
            {
                Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                UserAccountId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                Name = table.Column<string>(type: "nvarchar(120)", maxLength: 120, nullable: false),
                CreatedAtUtc = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false)
            },
            constraints: table => table.PrimaryKey("PK_Playlists", x => x.Id));

        migrationBuilder.CreateTable(
            name: "Transactions",
            columns: table => new
            {
                Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                UserAccountId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                MerchantId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                Amount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                Status = table.Column<int>(type: "int", nullable: false),
                RequestedAtUtc = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                AuthorizedAtUtc = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                Reason = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false)
            },
            constraints: table => table.PrimaryKey("PK_Transactions", x => x.Id));

        migrationBuilder.CreateTable(
            name: "MusicTracks",
            columns: table => new
            {
                Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                ArtistId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                AlbumId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                Title = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                NormalizedTitle = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                DurationSeconds = table.Column<int>(type: "int", nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_MusicTracks", x => x.Id);
                table.ForeignKey(
                    name: "FK_MusicTracks_Albums_AlbumId",
                    column: x => x.AlbumId,
                    principalTable: "Albums",
                    principalColumn: "Id",
                    onDelete: ReferentialAction.Cascade);
                table.ForeignKey(
                    name: "FK_MusicTracks_Artists_ArtistId",
                    column: x => x.ArtistId,
                    principalTable: "Artists",
                    principalColumn: "Id",
                    onDelete: ReferentialAction.NoAction);
            });

        migrationBuilder.CreateTable(
            name: "FavoriteMusics",
            columns: table => new
            {
                Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                UserAccountId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                TrackId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                CreatedAtUtc = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false)
            },
            constraints: table => table.PrimaryKey("PK_FavoriteMusics", x => x.Id));

        migrationBuilder.CreateTable(
            name: "PlaylistTracks",
            columns: table => new
            {
                Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                PlaylistId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                MusicId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                AddedAtUtc = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_PlaylistTracks", x => x.Id);
                table.ForeignKey(
                    name: "FK_PlaylistTracks_Playlists_PlaylistId",
                    column: x => x.PlaylistId,
                    principalTable: "Playlists",
                    principalColumn: "Id",
                    onDelete: ReferentialAction.Cascade);
                table.ForeignKey(
                    name: "FK_PlaylistTracks_MusicTracks_MusicId",
                    column: x => x.MusicId,
                    principalTable: "MusicTracks",
                    principalColumn: "Id",
                    onDelete: ReferentialAction.Cascade);
            });

        migrationBuilder.CreateTable(
            name: "TransactionNotifications",
            columns: table => new
            {
                Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                TransactionId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                Recipient = table.Column<string>(type: "nvarchar(180)", maxLength: 180, nullable: false),
                Channel = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                Message = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: false),
                Status = table.Column<int>(type: "int", nullable: false),
                SentAtUtc = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_TransactionNotifications", x => x.Id);
                table.ForeignKey(
                    name: "FK_TransactionNotifications_Transactions_TransactionId",
                    column: x => x.TransactionId,
                    principalTable: "Transactions",
                    principalColumn: "Id",
                    onDelete: ReferentialAction.Cascade);
            });

        migrationBuilder.CreateIndex(name: "IX_Albums_ArtistId", table: "Albums", column: "ArtistId");
        migrationBuilder.CreateIndex(name: "IX_Albums_NormalizedTitle", table: "Albums", column: "NormalizedTitle");
        migrationBuilder.CreateIndex(name: "IX_Artists_NormalizedName", table: "Artists", column: "NormalizedName");
        migrationBuilder.CreateIndex(name: "IX_FavoriteBands_UserAccountId_ArtistId", table: "FavoriteBands", columns: new[] { "UserAccountId", "ArtistId" }, unique: true);
        migrationBuilder.CreateIndex(name: "IX_FavoriteMusics_UserAccountId_TrackId", table: "FavoriteMusics", columns: new[] { "UserAccountId", "TrackId" }, unique: true);
        migrationBuilder.CreateIndex(name: "IX_MusicTracks_AlbumId", table: "MusicTracks", column: "AlbumId");
        migrationBuilder.CreateIndex(name: "IX_MusicTracks_ArtistId_NormalizedTitle", table: "MusicTracks", columns: new[] { "ArtistId", "NormalizedTitle" });
        migrationBuilder.CreateIndex(name: "IX_MusicTracks_NormalizedTitle", table: "MusicTracks", column: "NormalizedTitle");
        migrationBuilder.CreateIndex(name: "IX_PlaylistTracks_MusicId", table: "PlaylistTracks", column: "MusicId");
        migrationBuilder.CreateIndex(name: "IX_PlaylistTracks_PlaylistId_MusicId", table: "PlaylistTracks", columns: new[] { "PlaylistId", "MusicId" }, unique: true);
        migrationBuilder.CreateIndex(name: "IX_TransactionNotifications_TransactionId", table: "TransactionNotifications", column: "TransactionId");
        migrationBuilder.CreateIndex(name: "IX_Transactions_UserAccountId_RequestedAtUtc", table: "Transactions", columns: new[] { "UserAccountId", "RequestedAtUtc" });
        migrationBuilder.CreateIndex(name: "IX_UserAccounts_Email", table: "UserAccounts", column: "Email", unique: true);
    }

    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropTable(name: "FavoriteBands");
        migrationBuilder.DropTable(name: "FavoriteMusics");
        migrationBuilder.DropTable(name: "PlaylistTracks");
        migrationBuilder.DropTable(name: "SubscriptionPlans");
        migrationBuilder.DropTable(name: "TransactionNotifications");
        migrationBuilder.DropTable(name: "UserAccounts");
        migrationBuilder.DropTable(name: "MusicTracks");
        migrationBuilder.DropTable(name: "Playlists");
        migrationBuilder.DropTable(name: "Transactions");
        migrationBuilder.DropTable(name: "Albums");
        migrationBuilder.DropTable(name: "Merchants");
        migrationBuilder.DropTable(name: "Artists");
    }
}
