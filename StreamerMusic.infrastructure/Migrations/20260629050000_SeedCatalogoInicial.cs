using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MusicStreamer.infrastructure.Migrations;

public partial class SeedCatalogoInicial : Migration
{
    private static readonly Guid FreePlanId = Guid.Parse("11111111-1111-1111-1111-111111111111");
    private static readonly Guid PremiumPlanId = Guid.Parse("22222222-2222-2222-2222-222222222222");
    private static readonly Guid FamilyPlanId = Guid.Parse("33333333-3333-3333-3333-333333333333");

    private static readonly Guid RevTheoryArtistId = Guid.Parse("44444444-4444-4444-4444-444444444444");
    private static readonly Guid ShinedownArtistId = Guid.Parse("55555555-5555-5555-5555-555555555555");
    private static readonly Guid NirvanaArtistId = Guid.Parse("66666666-6666-6666-6666-666666666666");
    private static readonly Guid MetallicaArtistId = Guid.Parse("77777777-7777-7777-7777-777777777777");
    private static readonly Guid FooFightersArtistId = Guid.Parse("88888888-8888-8888-8888-888888888888");
    private static readonly Guid DisturbedArtistId = Guid.Parse("99999999-9999-9999-9999-999999999999");
    private static readonly Guid BreakingBenjaminArtistId = Guid.Parse("aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaaa");
    private static readonly Guid SeetherArtistId = Guid.Parse("bbbbbbbb-bbbb-bbbb-bbbb-bbbbbbbbbbbb");

    private static readonly Guid RevTheoryAlbumId = Guid.Parse("10000000-0000-0000-0000-000000000001");
    private static readonly Guid ShinedownSoundAlbumId = Guid.Parse("10000000-0000-0000-0000-000000000002");
    private static readonly Guid ShinedownCoversAlbumId = Guid.Parse("10000000-0000-0000-0000-000000000003");
    private static readonly Guid NirvanaAlbumId = Guid.Parse("10000000-0000-0000-0000-000000000004");
    private static readonly Guid MetallicaAlbumId = Guid.Parse("10000000-0000-0000-0000-000000000005");
    private static readonly Guid FooFightersAlbumId = Guid.Parse("10000000-0000-0000-0000-000000000006");
    private static readonly Guid DisturbedAlbumId = Guid.Parse("10000000-0000-0000-0000-000000000007");
    private static readonly Guid BreakingBenjaminAlbumId = Guid.Parse("10000000-0000-0000-0000-000000000008");
    private static readonly Guid SeetherAlbumId = Guid.Parse("10000000-0000-0000-0000-000000000009");

    private static readonly Guid MerchantSubscriptionId = Guid.Parse("50000000-0000-0000-0000-000000000001");
    private static readonly Guid MerchantTicketsId = Guid.Parse("50000000-0000-0000-0000-000000000002");
    private static readonly Guid MerchantStoreId = Guid.Parse("50000000-0000-0000-0000-000000000003");

    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.InsertData(
            table: "SubscriptionPlans",
            columns: new[] { "Id", "Name", "MonthlyPrice", "MaxTransactionAmount", "AllowsNightTransactions", "IsActive" },
            values: new object[,]
            {
                { FreePlanId, "Free", 0m, 30m, false, true },
                { PremiumPlanId, "Premium", 19.90m, 200m, true, true },
                { FamilyPlanId, "Family", 29.90m, 350m, true, true }
            });

        migrationBuilder.InsertData(
            table: "Artists",
            columns: new[] { "Id", "Name", "NormalizedName" },
            values: new object[,]
            {
                { RevTheoryArtistId, "Rev Theory", "REV THEORY" },
                { ShinedownArtistId, "Shinedown", "SHINEDOWN" },
                { NirvanaArtistId, "Nirvana", "NIRVANA" },
                { MetallicaArtistId, "Metallica", "METALLICA" },
                { FooFightersArtistId, "Foo Fighters", "FOO FIGHTERS" },
                { DisturbedArtistId, "Disturbed", "DISTURBED" },
                { BreakingBenjaminArtistId, "Breaking Benjamin", "BREAKING BENJAMIN" },
                { SeetherArtistId, "Seether", "SEETHER" }
            });

        migrationBuilder.InsertData(
            table: "Merchants",
            columns: new[] { "Id", "Name", "Category", "IsActive" },
            values: new object[,]
            {
                { MerchantSubscriptionId, "MusicStreamer Premium", "subscription", true },
                { MerchantTicketsId, "Ingresso de Show", "ticketing", true },
                { MerchantStoreId, "Loja Oficial MusicStreamer", "merchandise", true }
            });

        migrationBuilder.InsertData(
            table: "Albums",
            columns: new[] { "Id", "ArtistId", "Title", "NormalizedTitle", "ReleaseYear" },
            values: new object[,]
            {
                { RevTheoryAlbumId, RevTheoryArtistId, "Justice", "JUSTICE", 2008 },
                { ShinedownSoundAlbumId, ShinedownArtistId, "The Sound of Madness", "THE SOUND OF MADNESS", 2008 },
                { ShinedownCoversAlbumId, ShinedownArtistId, "B-Sides and Covers", "B-SIDES AND COVERS", 2012 },
                { NirvanaAlbumId, NirvanaArtistId, "Nevermind", "NEVERMIND", 1991 },
                { MetallicaAlbumId, MetallicaArtistId, "Metallica", "METALLICA", 1991 },
                { FooFightersAlbumId, FooFightersArtistId, "The Colour and the Shape", "THE COLOUR AND THE SHAPE", 1997 },
                { DisturbedAlbumId, DisturbedArtistId, "Immortalized", "IMMORTALIZED", 2015 },
                { BreakingBenjaminAlbumId, BreakingBenjaminArtistId, "Phobia", "PHOBIA", 2006 },
                { SeetherAlbumId, SeetherArtistId, "Karma and Effect", "KARMA AND EFFECT", 2005 }
            });

        migrationBuilder.InsertData(
            table: "MusicTracks",
            columns: new[] { "Id", "ArtistId", "AlbumId", "Title", "NormalizedTitle", "DurationSeconds" },
            values: new object[,]
            {
                { Guid.Parse("20000000-0000-0000-0000-000000000001"), RevTheoryArtistId, RevTheoryAlbumId, "Hell Yeah", "HELL YEAH", 202 },
                { Guid.Parse("20000000-0000-0000-0000-000000000002"), RevTheoryArtistId, RevTheoryAlbumId, "Over the Line", "OVER THE LINE", 214 },
                { Guid.Parse("20000000-0000-0000-0000-000000000003"), RevTheoryArtistId, RevTheoryAlbumId, "Broken Bones", "BROKEN BONES", 211 },
                { Guid.Parse("20000000-0000-0000-0000-000000000004"), ShinedownArtistId, ShinedownSoundAlbumId, "Sound of Madness", "SOUND OF MADNESS", 234 },
                { Guid.Parse("20000000-0000-0000-0000-000000000005"), ShinedownArtistId, ShinedownSoundAlbumId, "Second Chance", "SECOND CHANCE", 218 },
                { Guid.Parse("20000000-0000-0000-0000-000000000006"), ShinedownArtistId, ShinedownCoversAlbumId, "Simple Man", "SIMPLE MAN", 321 },
                { Guid.Parse("20000000-0000-0000-0000-000000000007"), ShinedownArtistId, ShinedownCoversAlbumId, "Her Name Is Alice", "HER NAME IS ALICE", 198 },
                { Guid.Parse("20000000-0000-0000-0000-000000000008"), NirvanaArtistId, NirvanaAlbumId, "Smells Like Teen Spirit", "SMELLS LIKE TEEN SPIRIT", 301 },
                { Guid.Parse("20000000-0000-0000-0000-000000000009"), NirvanaArtistId, NirvanaAlbumId, "Come As You Are", "COME AS YOU ARE", 219 },
                { Guid.Parse("20000000-0000-0000-0000-000000000010"), NirvanaArtistId, NirvanaAlbumId, "Lithium", "LITHIUM", 257 },
                { Guid.Parse("20000000-0000-0000-0000-000000000011"), MetallicaArtistId, MetallicaAlbumId, "Enter Sandman", "ENTER SANDMAN", 331 },
                { Guid.Parse("20000000-0000-0000-0000-000000000012"), MetallicaArtistId, MetallicaAlbumId, "Nothing Else Matters", "NOTHING ELSE MATTERS", 388 },
                { Guid.Parse("20000000-0000-0000-0000-000000000013"), MetallicaArtistId, MetallicaAlbumId, "Sad But True", "SAD BUT TRUE", 324 },
                { Guid.Parse("20000000-0000-0000-0000-000000000014"), FooFightersArtistId, FooFightersAlbumId, "The Pretender", "THE PRETENDER", 269 },
                { Guid.Parse("20000000-0000-0000-0000-000000000015"), FooFightersArtistId, FooFightersAlbumId, "Everlong", "EVERLONG", 250 },
                { Guid.Parse("20000000-0000-0000-0000-000000000016"), DisturbedArtistId, DisturbedAlbumId, "Down with the Sickness", "DOWN WITH THE SICKNESS", 289 },
                { Guid.Parse("20000000-0000-0000-0000-000000000017"), DisturbedArtistId, DisturbedAlbumId, "The Sound of Silence", "THE SOUND OF SILENCE", 244 },
                { Guid.Parse("20000000-0000-0000-0000-000000000018"), BreakingBenjaminArtistId, BreakingBenjaminAlbumId, "The Diary of Jane", "THE DIARY OF JANE", 201 },
                { Guid.Parse("20000000-0000-0000-0000-000000000019"), BreakingBenjaminArtistId, BreakingBenjaminAlbumId, "I Will Not Bow", "I WILL NOT BOW", 217 },
                { Guid.Parse("20000000-0000-0000-0000-000000000020"), SeetherArtistId, SeetherAlbumId, "Fake It", "FAKE IT", 194 },
                { Guid.Parse("20000000-0000-0000-0000-000000000021"), SeetherArtistId, SeetherAlbumId, "Remedy", "REMEDY", 204 }
            });
    }

    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DeleteData(table: "MusicTracks", keyColumn: "Id", keyValue: Guid.Parse("20000000-0000-0000-0000-000000000021"));
        migrationBuilder.DeleteData(table: "MusicTracks", keyColumn: "Id", keyValue: Guid.Parse("20000000-0000-0000-0000-000000000020"));
        migrationBuilder.DeleteData(table: "MusicTracks", keyColumn: "Id", keyValue: Guid.Parse("20000000-0000-0000-0000-000000000019"));
        migrationBuilder.DeleteData(table: "MusicTracks", keyColumn: "Id", keyValue: Guid.Parse("20000000-0000-0000-0000-000000000018"));
        migrationBuilder.DeleteData(table: "MusicTracks", keyColumn: "Id", keyValue: Guid.Parse("20000000-0000-0000-0000-000000000017"));
        migrationBuilder.DeleteData(table: "MusicTracks", keyColumn: "Id", keyValue: Guid.Parse("20000000-0000-0000-0000-000000000016"));
        migrationBuilder.DeleteData(table: "MusicTracks", keyColumn: "Id", keyValue: Guid.Parse("20000000-0000-0000-0000-000000000015"));
        migrationBuilder.DeleteData(table: "MusicTracks", keyColumn: "Id", keyValue: Guid.Parse("20000000-0000-0000-0000-000000000014"));
        migrationBuilder.DeleteData(table: "MusicTracks", keyColumn: "Id", keyValue: Guid.Parse("20000000-0000-0000-0000-000000000013"));
        migrationBuilder.DeleteData(table: "MusicTracks", keyColumn: "Id", keyValue: Guid.Parse("20000000-0000-0000-0000-000000000012"));
        migrationBuilder.DeleteData(table: "MusicTracks", keyColumn: "Id", keyValue: Guid.Parse("20000000-0000-0000-0000-000000000011"));
        migrationBuilder.DeleteData(table: "MusicTracks", keyColumn: "Id", keyValue: Guid.Parse("20000000-0000-0000-0000-000000000010"));
        migrationBuilder.DeleteData(table: "MusicTracks", keyColumn: "Id", keyValue: Guid.Parse("20000000-0000-0000-0000-000000000009"));
        migrationBuilder.DeleteData(table: "MusicTracks", keyColumn: "Id", keyValue: Guid.Parse("20000000-0000-0000-0000-000000000008"));
        migrationBuilder.DeleteData(table: "MusicTracks", keyColumn: "Id", keyValue: Guid.Parse("20000000-0000-0000-0000-000000000007"));
        migrationBuilder.DeleteData(table: "MusicTracks", keyColumn: "Id", keyValue: Guid.Parse("20000000-0000-0000-0000-000000000006"));
        migrationBuilder.DeleteData(table: "MusicTracks", keyColumn: "Id", keyValue: Guid.Parse("20000000-0000-0000-0000-000000000005"));
        migrationBuilder.DeleteData(table: "MusicTracks", keyColumn: "Id", keyValue: Guid.Parse("20000000-0000-0000-0000-000000000004"));
        migrationBuilder.DeleteData(table: "MusicTracks", keyColumn: "Id", keyValue: Guid.Parse("20000000-0000-0000-0000-000000000003"));
        migrationBuilder.DeleteData(table: "MusicTracks", keyColumn: "Id", keyValue: Guid.Parse("20000000-0000-0000-0000-000000000002"));
        migrationBuilder.DeleteData(table: "MusicTracks", keyColumn: "Id", keyValue: Guid.Parse("20000000-0000-0000-0000-000000000001"));

        migrationBuilder.DeleteData(table: "Albums", keyColumn: "Id", keyValue: SeetherAlbumId);
        migrationBuilder.DeleteData(table: "Albums", keyColumn: "Id", keyValue: BreakingBenjaminAlbumId);
        migrationBuilder.DeleteData(table: "Albums", keyColumn: "Id", keyValue: DisturbedAlbumId);
        migrationBuilder.DeleteData(table: "Albums", keyColumn: "Id", keyValue: FooFightersAlbumId);
        migrationBuilder.DeleteData(table: "Albums", keyColumn: "Id", keyValue: MetallicaAlbumId);
        migrationBuilder.DeleteData(table: "Albums", keyColumn: "Id", keyValue: NirvanaAlbumId);
        migrationBuilder.DeleteData(table: "Albums", keyColumn: "Id", keyValue: ShinedownCoversAlbumId);
        migrationBuilder.DeleteData(table: "Albums", keyColumn: "Id", keyValue: ShinedownSoundAlbumId);
        migrationBuilder.DeleteData(table: "Albums", keyColumn: "Id", keyValue: RevTheoryAlbumId);

        migrationBuilder.DeleteData(table: "Merchants", keyColumn: "Id", keyValue: MerchantStoreId);
        migrationBuilder.DeleteData(table: "Merchants", keyColumn: "Id", keyValue: MerchantTicketsId);
        migrationBuilder.DeleteData(table: "Merchants", keyColumn: "Id", keyValue: MerchantSubscriptionId);

        migrationBuilder.DeleteData(table: "Artists", keyColumn: "Id", keyValue: SeetherArtistId);
        migrationBuilder.DeleteData(table: "Artists", keyColumn: "Id", keyValue: BreakingBenjaminArtistId);
        migrationBuilder.DeleteData(table: "Artists", keyColumn: "Id", keyValue: DisturbedArtistId);
        migrationBuilder.DeleteData(table: "Artists", keyColumn: "Id", keyValue: FooFightersArtistId);
        migrationBuilder.DeleteData(table: "Artists", keyColumn: "Id", keyValue: MetallicaArtistId);
        migrationBuilder.DeleteData(table: "Artists", keyColumn: "Id", keyValue: NirvanaArtistId);
        migrationBuilder.DeleteData(table: "Artists", keyColumn: "Id", keyValue: ShinedownArtistId);
        migrationBuilder.DeleteData(table: "Artists", keyColumn: "Id", keyValue: RevTheoryArtistId);

        migrationBuilder.DeleteData(table: "SubscriptionPlans", keyColumn: "Id", keyValue: FamilyPlanId);
        migrationBuilder.DeleteData(table: "SubscriptionPlans", keyColumn: "Id", keyValue: PremiumPlanId);
        migrationBuilder.DeleteData(table: "SubscriptionPlans", keyColumn: "Id", keyValue: FreePlanId);
    }
}
