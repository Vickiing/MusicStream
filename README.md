# MusicStreamer

Aplicacao de streaming de musica desenvolvida em ASP.NET Core com:

- **Backend** em Web API
- **Camada de apresentacao visual** em **ASP.NET MVC**
- **Entity Framework Core**
- **Repository pattern**
- **DDD inspirado em Eric Evans**
- **Azure SQL Server** como banco alvo

## Estrutura

- `MusicStreamer.Api`: Web API + MVC
- `MusicStreamer.App`: application services e DTOs
- `MusicStreamer.Domain`: entidades, value objects, repositories e regras de negocio
- `StreamerMusic.infrastructure`: EF Core, repositories, seed e migrations

## Como configurar

Defina a connection string do Azure SQL Server em `ConnectionStrings:AzureSqlConnection`.

Exemplo em [appsettings.json linhas 2-4](./MusicStreamer.Api/appsettings.json#L2-L4):

```json
"ConnectionStrings": {
  "AzureSqlConnection": "Server=tcp:SEU-SERVIDOR.database.windows.net,1433;Initial Catalog=MusicStreamerDb;Persist Security Info=False;User ID=USUARIO;Password=SENHA;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;"
}
```

## Como executar

1. Configure a sua `AzureSqlConnection`.
2. Compile:

```powershell
dotnet build MusicStreamer.slnx --no-restore
```

3. Se quiser aplicar a migration manualmente:

```powershell
dotnet ef database update --project StreamerMusic.infrastructure --startup-project MusicStreamer.Api
```

4. Rode a aplicacao:

```powershell
dotnet run --project MusicStreamer.Api/MusicStreamer.Api.csproj
```

5. Acesse:

- MVC: `https://localhost:7107/app/account/register`
- API: `https://localhost:7107/api/...`

Observacao: a aplicacao nao aplica migrations nem seed automaticamente no startup. O banco deve ser atualizado manualmente com `dotnet ef database update` antes de subir a API. A migration `SeedCatalogoInicial` popula planos, artistas, albuns, musicas e comerciantes de exemplo.

## Evidencias das rubricas

| Rubrica | Status | Evidencia |
| --- | --- | --- |
| 1.1 Camada de apresentacao | Demonstrado | MVC: [DashboardController.cs linhas 9-132](./MusicStreamer.Api/Controllers/DashboardController.cs#L9-L132), [Index.cshtml linhas 11-202](./MusicStreamer.Api/Views/Dashboard/Index.cshtml#L11-L202), [Login.cshtml linhas 6-31](./MusicStreamer.Api/Views/AccountMvc/Login.cshtml#L6-L31). API: [AuthController.cs linhas 8-27](./MusicStreamer.Api/Controllers/AuthController.cs#L8-L27) |
| 1.2 Camada de servicos | Demonstrado | [AuthService.cs linhas 9-40](./MusicStreamer.App/Services/AuthService.cs#L9-L40), [PlaylistService.cs linhas 8-63](./MusicStreamer.App/Services/PlaylistService.cs#L8-L63), [TransactionService.cs linhas 9-71](./MusicStreamer.App/Services/TransactionService.cs#L9-L71) |
| 1.3 Camada de negocios | Demonstrado | [UserAccount.cs linhas 6-42](./MusicStreamer.Domain/Entities/UserAccount.cs#L6-L42), [Playlist.cs linhas 3-34](./MusicStreamer.Domain/Entities/Playlist.cs#L3-L34), [TransactionAuthorizationDomainService.cs linhas 6-55](./MusicStreamer.Domain/Services/TransactionAuthorizationDomainService.cs#L6-L55) |
| 1.4 Camada de acesso a dados | Demonstrado | [MusicStreamerDbContext.cs linhas 8-134](./StreamerMusic.infrastructure/Data/MusicStreamerDbContext.cs#L8-L134), [UserAccountRepository.cs linhas 8-31](./StreamerMusic.infrastructure/Repositories/UserAccountRepository.cs#L8-L31) |
| 2.1 Cadastro e login | Demonstrado | API: [AuthController.cs linhas 12-26](./MusicStreamer.Api/Controllers/AuthController.cs#L12-L26). MVC: [AccountMvcController.cs linhas 12-91](./MusicStreamer.Api/Controllers/AccountMvcController.cs#L12-L91). Hash e token: [Pbkdf2PasswordHasher.cs linhas 6-31](./StreamerMusic.infrastructure/Security/Pbkdf2PasswordHasher.cs#L6-L31), [JwtTokenService.cs linhas 9-25](./StreamerMusic.infrastructure/Security/JwtTokenService.cs#L9-L25) |
| 2.2 Modulo de transacao | Demonstrado | [TransactionsController.cs linhas 14-30](./MusicStreamer.Api/Controllers/TransactionsController.cs#L14-L30), [DashboardController.cs linhas 110-119](./MusicStreamer.Api/Controllers/DashboardController.cs#L110-L119), [TransactionService.cs linhas 16-44](./MusicStreamer.App/Services/TransactionService.cs#L16-L44), [TransactionNotification.cs linhas 20-41](./MusicStreamer.Domain/Entities/TransactionNotification.cs#L20-L41) |
| 2.3 Busca de musica | Demonstrado | [CatalogController.cs linhas 23-31](./MusicStreamer.Api/Controllers/CatalogController.cs#L23-L31), [DashboardController.cs linhas 24-57](./MusicStreamer.Api/Controllers/DashboardController.cs#L24-L57), [CatalogRepository.cs linhas 40-73](./StreamerMusic.infrastructure/Repositories/CatalogRepository.cs#L40-L73), [MusicStreamerDbContext.cs linhas 47-75](./StreamerMusic.infrastructure/Data/MusicStreamerDbContext.cs#L47-L75) |
| 2.4 Favoritar musica | Demonstrado | API: [FavoritesController.cs linhas 14-35](./MusicStreamer.Api/Controllers/FavoritesController.cs#L14-L35). MVC: [DashboardController.cs linhas 92-107](./MusicStreamer.Api/Controllers/DashboardController.cs#L92-L107), [Index.cshtml linhas 98-128](./MusicStreamer.Api/Views/Dashboard/Index.cshtml#L98-L128). Service: [FavoritesService.cs linhas 13-68](./MusicStreamer.App/Services/FavoritesService.cs#L13-L68) |
| 3.1 Modelo EF Core | Demonstrado | [MusicStreamerDbContext.cs linhas 10-21](./StreamerMusic.infrastructure/Data/MusicStreamerDbContext.cs#L10-L21), [MusicStreamerDbContext.cs linhas 23-134](./StreamerMusic.infrastructure/Data/MusicStreamerDbContext.cs#L23-L134) |
| 3.2 Migrations | Demonstrado | [20260628090000_InitialCreate.cs linhas 8-239](./StreamerMusic.infrastructure/Migrations/20260628090000_InitialCreate.cs#L8-L239), [SeedCatalogoInicial.cs linhas 7-169](./StreamerMusic.infrastructure/Migrations/20260629050000_SeedCatalogoInicial.cs#L7-L169) |
| 3.3 Repository pattern | Demonstrado | Contratos: [IUserAccountRepository.cs linhas 5-10](./MusicStreamer.Domain/Repositories/IUserAccountRepository.cs#L5-L10), [IPlaylistRepository.cs linhas 5-10](./MusicStreamer.Domain/Repositories/IPlaylistRepository.cs#L5-L10), [ITransactionRepository.cs linhas 5-9](./MusicStreamer.Domain/Repositories/ITransactionRepository.cs#L5-L9). Implementacoes: [UserAccountRepository.cs linhas 8-31](./StreamerMusic.infrastructure/Repositories/UserAccountRepository.cs#L8-L31), [TransactionRepository.cs linhas 9-34](./StreamerMusic.infrastructure/Repositories/TransactionRepository.cs#L9-L34) |
| 3.4 Injecao de dependencia | Demonstrado | [Program.cs linhas 18-58](./MusicStreamer.Api/Program.cs#L18-L58) |
| 4.1 Microsoft Azure | Demonstrado | Preparacao para Azure SQL e configuracao por ambiente em [Program.cs linhas 28-32](./MusicStreamer.Api/Program.cs#L28-L32) e [appsettings.json linhas 10-15](./MusicStreamer.Api/appsettings.json#L10-L15) |
| 4.2 Armazenamento de dados no Azure | Demonstrado | Justificativa configurada em [appsettings.json linhas 10-15](./MusicStreamer.Api/appsettings.json#L10-L15) |
| 4.3 Azure SQL Database | Demonstrado | [Program.cs linhas 28-32](./MusicStreamer.Api/Program.cs#L28-L32), [appsettings.json linhas 2-4](./MusicStreamer.Api/appsettings.json#L2-L4), [20260628090000_InitialCreate.cs linhas 12-221](./StreamerMusic.infrastructure/Migrations/20260628090000_InitialCreate.cs#L12-L221) |
| 4.4 Azure App Service | Demonstrado | Aplicacao web preparada para publicacao com Web API + MVC em [Program.cs linhas 69-78](./MusicStreamer.Api/Program.cs#L69-L78) e justificativa em [appsettings.json linhas 10-15](./MusicStreamer.Api/appsettings.json#L10-L15) |

## Itens funcionais implementados

- Criacao de conta
- Login
- Escolha de plano
- Listagem de bandas e albuns
- Busca de bandas e musicas
- Criacao de playlist
- Associacao de musica a playlist
- Favoritar musicas
- Favoritar bandas
- Autorizacao de transacao com validacao de estado, valor, horario e ultima autorizacao
- Notificacao para comerciante e dono do cartao
- Preparacao para Azure SQL Server e hospedagem no Azure

## Validacao executada

- Build realizado com sucesso: `dotnet build MusicStreamer.slnx --no-restore`
