# MusicStreamer

AplicaÃ§Ã£o de streaming de mÃºsica desenvolvida em ASP.NET Core com:

- **Backend** em Web API
- **Camada de apresentaÃ§Ã£o visual** em **ASP.NET MVC**
- **Entity Framework Core**
- **Repository pattern**
- **DDD inspirado em Eric Evans**
- **Azure SQL Server** como banco alvo

## Estrutura

- `MusicStreamer.Api`: Web API + MVC
- `MusicStreamer.App`: application services e DTOs
- `MusicStreamer.Domain`: entidades, value objects, repositories e regras de negÃ³cio
- `StreamerMusic.infrastructure`: EF Core, repositories e migrations

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

3. Execute a aplicaÃ§Ã£o:

```powershell
dotnet run --project MusicStreamer.Api/MusicStreamer.Api.csproj
```

4. No startup, a aplicaÃ§Ã£o executa automaticamente as migrations e os seeds. Isso inclui:

- planos, comerciantes, artistas, Ã¡lbuns e mÃºsicas base
- 50 mÃºsicas extras para demonstraÃ§Ã£o da busca

5. Acesse:

- MVC: `https://localhost:7107/app/account/register`
- API: `https://localhost:7107/api/...`

ObservaÃ§Ã£o: o startup faz `Database.Migrate()` e executa os seeds automaticamente. Basta configurar a connection string e subir a API.

## EvidÃªncias das rubricas

| Rubrica | EvidÃªncia |
| --- | --- |
| 1.1 Camada de apresentaÃ§Ã£o | MVC: [PainelController.cs](./MusicStreamer.Api/Controllers/PainelController.cs), [Home.cshtml](./MusicStreamer.Api/Views/Painel/Home.cshtml), [Catalog.cshtml](./MusicStreamer.Api/Views/Painel/Catalog.cshtml), [Plans.cshtml](./MusicStreamer.Api/Views/Painel/Plans.cshtml), [Playlists.cshtml](./MusicStreamer.Api/Views/Painel/Playlists.cshtml), [Search.cshtml](./MusicStreamer.Api/Views/Painel/Search.cshtml), [Favorites.cshtml](./MusicStreamer.Api/Views/Painel/Favorites.cshtml), [Transactions.cshtml](./MusicStreamer.Api/Views/Painel/Transactions.cshtml), [Shared/_Layout.cshtml](./MusicStreamer.Api/Views/Shared/_Layout.cshtml), [Shared/_Sidebar.cshtml](./MusicStreamer.Api/Views/Shared/_Sidebar.cshtml), [Login.cshtml](./MusicStreamer.Api/Views/ContaUsuarioMvc/Login.cshtml), [Register.cshtml](./MusicStreamer.Api/Views/ContaUsuarioMvc/Register.cshtml). API: [AutenticacaoController.cs](./MusicStreamer.Api/Controllers/AutenticacaoController.cs) |
| 1.2 Camada de serviÃ§os | [ServicoAutenticacao.cs](./MusicStreamer.App/Services/ServicoAutenticacao.cs), [ServicoPlaylist.cs](./MusicStreamer.App/Services/ServicoPlaylist.cs), [ServicoTransacoes.cs](./MusicStreamer.App/Services/ServicoTransacoes.cs), [ServicoFavoritos.cs](./MusicStreamer.App/Services/ServicoFavoritos.cs), [ServicoCatalogo.cs](./MusicStreamer.App/Services/ServicoCatalogo.cs), [ServicoPlanosAssinatura.cs](./MusicStreamer.App/Services/ServicoPlanosAssinatura.cs) |
| 1.3 Camada de negÃ³cios | [ContaUsuario.cs](./MusicStreamer.Domain/Entities/ContaUsuario.cs), [Playlist.cs](./MusicStreamer.Domain/Entities/Playlist.cs), [ServicoAutorizacaoTransacao.cs](./MusicStreamer.Domain/Services/ServicoAutorizacaoTransacao.cs), [DecisaoAutorizacaoTransacao.cs](./MusicStreamer.Domain/Services/DecisaoAutorizacaoTransacao.cs) |
| 1.4 Camada de acesso a dados | [MusicStreamerDbContext.cs](./StreamerMusic.infrastructure/Data/MusicStreamerDbContext.cs), [ContaUsuarioRepository.cs](./StreamerMusic.infrastructure/Repositories/ContaUsuarioRepository.cs), [TransacaoRepository.cs](./StreamerMusic.infrastructure/Repositories/TransacaoRepository.cs), [InicializadorBancoDados.cs](./StreamerMusic.infrastructure/Data/InicializadorBancoDados.cs), [CatalogoRepository.cs](./StreamerMusic.infrastructure/Repositories/CatalogoRepository.cs) |
| 2.1 Cadastro e login | API: [AutenticacaoController.cs](./MusicStreamer.Api/Controllers/AutenticacaoController.cs). MVC: [ContaUsuarioMvcController.cs](./MusicStreamer.Api/Controllers/ContaUsuarioMvcController.cs). Hash e token: [Pbkdf2PasswordHasher.cs](./StreamerMusic.infrastructure/Security/Pbkdf2PasswordHasher.cs), [JwtTokenService.cs](./StreamerMusic.infrastructure/Security/JwtTokenService.cs) |
| 2.2 MÃ³dulo de transaÃ§Ã£o | [TransacoesController.cs](./MusicStreamer.Api/Controllers/TransacoesController.cs), [PainelController.cs](./MusicStreamer.Api/Controllers/PainelController.cs), [Transactions.cshtml](./MusicStreamer.Api/Views/Painel/Transactions.cshtml), [ServicoTransacoes.cs](./MusicStreamer.App/Services/ServicoTransacoes.cs), [TransactionNotification.cs](./MusicStreamer.Domain/Entities/TransactionNotification.cs) |
| 2.3 Busca de mÃºsica | [CatalogoController.cs](./MusicStreamer.Api/Controllers/CatalogoController.cs), [PainelController.cs](./MusicStreamer.Api/Controllers/PainelController.cs), [Search.cshtml](./MusicStreamer.Api/Views/Painel/Search.cshtml), [CatalogoRepository.cs](./StreamerMusic.infrastructure/Repositories/CatalogoRepository.cs), [MusicStreamerDbContext.cs](./StreamerMusic.infrastructure/Data/MusicStreamerDbContext.cs) |
| 2.4 Favoritar mÃºsica | API: [FavoritosController.cs](./MusicStreamer.Api/Controllers/FavoritosController.cs). MVC: [PainelController.cs](./MusicStreamer.Api/Controllers/PainelController.cs), [Search.cshtml](./MusicStreamer.Api/Views/Painel/Search.cshtml), [Favorites.cshtml](./MusicStreamer.Api/Views/Painel/Favorites.cshtml). Service: [ServicoFavoritos.cs](./MusicStreamer.App/Services/ServicoFavoritos.cs) |
| 3.1 Modelo EF Core | [MusicStreamerDbContext.cs](./StreamerMusic.infrastructure/Data/MusicStreamerDbContext.cs), [Entidades mapeadas e DbSets](./StreamerMusic.infrastructure/Data/MusicStreamerDbContext.cs) |
| 3.2 Migrations | [20260628090000_InitialCreate.cs](./StreamerMusic.infrastructure/Migrations/20260628090000_InitialCreate.cs), [InicializadorBancoDados.cs](./StreamerMusic.infrastructure/Data/InicializadorBancoDados.cs) |
| 3.3 Repository pattern | Contratos: [IContaUsuarioRepository.cs](./MusicStreamer.Domain/Repositories/IContaUsuarioRepository.cs), [IPlaylistRepository.cs](./MusicStreamer.Domain/Repositories/IPlaylistRepository.cs), [ITransacaoRepository.cs](./MusicStreamer.Domain/Repositories/ITransacaoRepository.cs). ImplementaÃ§Ãµes: [ContaUsuarioRepository.cs](./StreamerMusic.infrastructure/Repositories/ContaUsuarioRepository.cs), [TransacaoRepository.cs](./StreamerMusic.infrastructure/Repositories/TransacaoRepository.cs) |
| 3.4 InjeÃ§Ã£o de dependÃªncia | [Program.cs](./MusicStreamer.Api/Program.cs) |
| 4.1 Microsoft Azure | PreparaÃ§Ã£o para Azure SQL e configuraÃ§Ã£o por ambiente em [Program.cs](./MusicStreamer.Api/Program.cs) e [appsettings.json](./MusicStreamer.Api/appsettings.json) |
| 4.2 Armazenamento de dados no Azure | Justificativa configurada em [appsettings.json](./MusicStreamer.Api/appsettings.json) |
| 4.3 Azure SQL Database | [Program.cs](./MusicStreamer.Api/Program.cs), [appsettings.json](./MusicStreamer.Api/appsettings.json), [20260628090000_InitialCreate.cs](./StreamerMusic.infrastructure/Migrations/20260628090000_InitialCreate.cs), [InicializadorBancoDados.cs](./StreamerMusic.infrastructure/Data/InicializadorBancoDados.cs) |
| 4.4 Azure App Service | AplicaÃ§Ã£o web preparada para publicaÃ§Ã£o com Web API + MVC em [Program.cs](./MusicStreamer.Api/Program.cs), [Shared/_Layout.cshtml](./MusicStreamer.Api/Views/Shared/_Layout.cshtml) e [Shared/_Sidebar.cshtml](./MusicStreamer.Api/Views/Shared/_Sidebar.cshtml) |

## Itens funcionais implementados

- CriaÃ§Ã£o de conta
- Login
- Escolha de plano com tela de pagamento simulada de R$50
- Player visual com botÃµes comuns de reproduÃ§Ã£o e bloqueio visual atÃ© ativaÃ§Ã£o da assinatura
- Listagem de bandas e Ã¡lbuns
- Busca de bandas e mÃºsicas
- CriaÃ§Ã£o de playlist
- AssociaÃ§Ã£o de mÃºsica Ã  playlist
- Favoritar mÃºsicas
- Favoritar bandas
- AutorizaÃ§Ã£o de transaÃ§Ã£o com validaÃ§Ã£o de estado, valor, horÃ¡rio e Ãºltima autorizaÃ§Ã£o
- NotificaÃ§Ã£o para comerciante e dono do cartÃ£o
- PreparaÃ§Ã£o para Azure SQL Server e hospedagem no Azure

## Rubrica 4 - Microsoft Azure

**4.1 Microsoft Azure:** a aplicaÃ§Ã£o foi pensada para rodar como um web app publicado no Azure, separando configuraÃ§Ã£o, camadas e dependÃªncia de ambiente para facilitar implantaÃ§Ã£o e manutenÃ§Ã£o.

**4.2 Armazenamento de dados no Microsoft Azure:** o uso de SQL gerenciado evita administrar servidor local, facilita backup, escala e reduz o trabalho operacional para um sistema com autenticaÃ§Ã£o, catÃ¡logo e transaÃ§Ãµes.

**4.3 Azure SQL Database:** o banco relacional foi escolhido porque o sistema tem entidades conectadas entre si, precisa de integridade entre usuÃ¡rios, planos, playlists, favoritos e transaÃ§Ãµes, e o EF Core conversa naturalmente com esse modelo.

**4.4 Azure App Service:** o App Service atende bem uma aplicaÃ§Ã£o ASP.NET MVC + Web API porque simplifica publicaÃ§Ã£o, deployment e execuÃ§Ã£o da aplicaÃ§Ã£o sem exigir infraestrutura prÃ³pria, mantendo a entrega mais direta para a demonstraÃ§Ã£o.

## ValidaÃ§Ã£o executada

- RevisÃ£o de cÃ³digo e estrutura concluÃ­da no workspace.
- `dotnet build MusicStreamer.Api\MusicStreamer.Api.csproj --no-restore` executado com sucesso.
