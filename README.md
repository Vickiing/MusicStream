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

3. Rode a aplicacao:

```powershell
dotnet run --project MusicStreamer.Api/MusicStreamer.Api.csproj
```

4. A aplicacao executa automaticamente as migrations e os seeds no startup. Isso inclui:

- planos, comerciantes, artistas, albuns e musicas base
- 50 musicas extras para demonstracao da busca

5. Acesse:

- MVC: `https://localhost:7107/app/account/register`
- API: `https://localhost:7107/api/...`

Observacao: o startup da aplicacao faz `Database.Migrate()` e executa os seeds automaticamente. Basta configurar a connection string e subir a API.

## Evidencias das rubricas

| Rubrica | Evidencia |
| --- | --- |
| 1.1 Camada de apresentacao | MVC: [PainelController.cs linhas 1-186](./MusicStreamer.Api/Controllers/PainelController.cs#L1-L186), [Home.cshtml linhas 1-28](./MusicStreamer.Api/Views/Painel/Home.cshtml#L1-L28), [Catalog.cshtml linhas 1-53](./MusicStreamer.Api/Views/Painel/Catalog.cshtml#L1-L53), [Plans.cshtml linhas 1-25](./MusicStreamer.Api/Views/Painel/Plans.cshtml#L1-L25), [Playlists.cshtml linhas 1-68](./MusicStreamer.Api/Views/Painel/Playlists.cshtml#L1-L68), [Search.cshtml linhas 1-74](./MusicStreamer.Api/Views/Painel/Search.cshtml#L1-L74), [Favorites.cshtml linhas 1-61](./MusicStreamer.Api/Views/Painel/Favorites.cshtml#L1-L61), [Transactions.cshtml linhas 1-52](./MusicStreamer.Api/Views/Painel/Transactions.cshtml#L1-L52), [Shared/_Layout.cshtml linhas 1-288](./MusicStreamer.Api/Views/Shared/_Layout.cshtml#L1-L288), [Shared/_Sidebar.cshtml linhas 1-25](./MusicStreamer.Api/Views/Shared/_Sidebar.cshtml#L1-L25), [Login.cshtml linhas 1-25](./MusicStreamer.Api/Views/ContaUsuarioMvc/Login.cshtml#L1-L25), [Register.cshtml linhas 1-25](./MusicStreamer.Api/Views/ContaUsuarioMvc/Register.cshtml#L1-L25). API: [AutenticacaoController.cs linhas 8-27](./MusicStreamer.Api/Controllers/AutenticacaoController.cs#L8-L27) |
| 1.2 Camada de servicos | [ServicoAutenticacao.cs linhas 9-40](./MusicStreamer.App/Services/ServicoAutenticacao.cs#L9-L40), [ServicoPlaylist.cs linhas 8-63](./MusicStreamer.App/Services/ServicoPlaylist.cs#L8-L63), [ServicoTransacoes.cs linhas 9-71](./MusicStreamer.App/Services/ServicoTransacoes.cs#L9-L71) |
| 1.3 Camada de negocios | [ContaUsuario.cs linhas 6-42](./MusicStreamer.Domain/Entities/ContaUsuario.cs#L6-L42), [Playlist.cs linhas 3-34](./MusicStreamer.Domain/Entities/Playlist.cs#L3-L34), [TransactionAuthorizationDomainService.cs linhas 6-55](./MusicStreamer.Domain/Services/TransactionAuthorizationDomainService.cs#L6-L55) |
| 1.4 Camada de acesso a dados | [MusicStreamerDbContext.cs linhas 8-134](./StreamerMusic.infrastructure/Data/MusicStreamerDbContext.cs#L8-L134), [ContaUsuarioRepository.cs linhas 8-31](./StreamerMusic.infrastructure/Repositories/ContaUsuarioRepository.cs#L8-L31), [InicializadorBancoDados.cs](./StreamerMusic.infrastructure/Data/InicializadorBancoDados.cs) |
| 2.1 Cadastro e login | API: [AutenticacaoController.cs linhas 12-26](./MusicStreamer.Api/Controllers/AutenticacaoController.cs#L12-L26). MVC: [ContaUsuarioMvcController.cs linhas 12-91](./MusicStreamer.Api/Controllers/ContaUsuarioMvcController.cs#L12-L91). Hash e token: [Pbkdf2PasswordHasher.cs linhas 6-31](./StreamerMusic.infrastructure/Security/Pbkdf2PasswordHasher.cs#L6-L31), [JwtTokenService.cs linhas 9-25](./StreamerMusic.infrastructure/Security/JwtTokenService.cs#L9-L25) |
| 2.2 Modulo de transacao | [TransacoesController.cs linhas 14-30](./MusicStreamer.Api/Controllers/TransacoesController.cs#L14-L30), [PainelController.cs linhas 150-159](./MusicStreamer.Api/Controllers/PainelController.cs#L150-L159), [Transactions.cshtml linhas 1-52](./MusicStreamer.Api/Views/Painel/Transactions.cshtml#L1-L52), [ServicoTransacoes.cs linhas 16-44](./MusicStreamer.App/Services/ServicoTransacoes.cs#L16-L44), [TransactionNotification.cs linhas 20-41](./MusicStreamer.Domain/Entities/TransactionNotification.cs#L20-L41) |
| 2.3 Busca de musica | [CatalogoController.cs linhas 23-31](./MusicStreamer.Api/Controllers/CatalogoController.cs#L23-L31), [PainelController.cs linhas 41-46](./MusicStreamer.Api/Controllers/PainelController.cs#L41-L46), [Search.cshtml linhas 1-74](./MusicStreamer.Api/Views/Painel/Search.cshtml#L1-L74), [CatalogoRepository.cs linhas 40-73](./StreamerMusic.infrastructure/Repositories/CatalogoRepository.cs#L40-L73), [MusicStreamerDbContext.cs linhas 47-75](./StreamerMusic.infrastructure/Data/MusicStreamerDbContext.cs#L47-L75) |
| 2.4 Favoritar musica | API: [FavoritosController.cs linhas 14-35](./MusicStreamer.Api/Controllers/FavoritosController.cs#L14-L35). MVC: [PainelController.cs linhas 114-141](./MusicStreamer.Api/Controllers/PainelController.cs#L114-L141), [Search.cshtml linhas 1-74](./MusicStreamer.Api/Views/Painel/Search.cshtml#L1-L74), [Favorites.cshtml linhas 1-61](./MusicStreamer.Api/Views/Painel/Favorites.cshtml#L1-L61). Service: [ServicoFavoritos.cs linhas 13-68](./MusicStreamer.App/Services/ServicoFavoritos.cs#L13-L68) |
| 3.1 Modelo EF Core | [MusicStreamerDbContext.cs linhas 10-21](./StreamerMusic.infrastructure/Data/MusicStreamerDbContext.cs#L10-L21), [MusicStreamerDbContext.cs linhas 23-134](./StreamerMusic.infrastructure/Data/MusicStreamerDbContext.cs#L23-L134) |
| 3.2 Migrations | [20260628090000_InitialCreate.cs linhas 8-239](./StreamerMusic.infrastructure/Migrations/20260628090000_InitialCreate.cs#L8-L239), [InicializadorBancoDados.cs](./StreamerMusic.infrastructure/Data/InicializadorBancoDados.cs) |
| 3.3 Repository pattern | Contratos: [IContaUsuarioRepository.cs linhas 5-10](./MusicStreamer.Domain/Repositories/IContaUsuarioRepository.cs#L5-L10), [IPlaylistRepository.cs linhas 5-10](./MusicStreamer.Domain/Repositories/IPlaylistRepository.cs#L5-L10), [ITransacaoRepository.cs linhas 5-9](./MusicStreamer.Domain/Repositories/ITransacaoRepository.cs#L5-L9). Implementacoes: [ContaUsuarioRepository.cs linhas 8-31](./StreamerMusic.infrastructure/Repositories/ContaUsuarioRepository.cs#L8-L31), [TransacaoRepository.cs linhas 9-34](./StreamerMusic.infrastructure/Repositories/TransacaoRepository.cs#L9-L34) |
| 3.4 Injecao de dependencia | [Program.cs linhas 18-58](./MusicStreamer.Api/Program.cs#L18-L58) |
| 4.1 Microsoft Azure | Preparacao para Azure SQL e configuracao por ambiente em [Program.cs linhas 28-32](./MusicStreamer.Api/Program.cs#L28-L32) e [appsettings.json linhas 10-15](./MusicStreamer.Api/appsettings.json#L10-L15) |
| 4.2 Armazenamento de dados no Azure | Justificativa configurada em [appsettings.json linhas 10-15](./MusicStreamer.Api/appsettings.json#L10-L15) |
| 4.3 Azure SQL Database | [Program.cs linhas 28-32](./MusicStreamer.Api/Program.cs#L28-L32), [appsettings.json linhas 2-4](./MusicStreamer.Api/appsettings.json#L2-L4), [20260628090000_InitialCreate.cs linhas 12-221](./StreamerMusic.infrastructure/Migrations/20260628090000_InitialCreate.cs#L12-L221), [InicializadorBancoDados.cs](./StreamerMusic.infrastructure/Data/InicializadorBancoDados.cs) |
| 4.4 Azure App Service | Aplicacao web preparada para publicacao com Web API + MVC em [Program.cs linhas 69-78](./MusicStreamer.Api/Program.cs#L69-L78), [Shared/_Layout.cshtml linhas 1-288](./MusicStreamer.Api/Views/Shared/_Layout.cshtml#L1-L288) e [Shared/_Sidebar.cshtml linhas 1-25](./MusicStreamer.Api/Views/Shared/_Sidebar.cshtml#L1-L25) |

## Itens funcionais implementados

- Criacao de conta
- Login
- Escolha de plano com tela de pagamento simulada de R$50
- Player visual com botoes comuns de reproducao e bloqueio visual ate ativacao da assinatura
- Listagem de bandas e albuns
- Busca de bandas e musicas
- Criacao de playlist
- Associacao de musica a playlist
- Favoritar musicas
- Favoritar bandas
- Autorizacao de transacao com validacao de estado, valor, horario e ultima autorizacao
- Notificacao para comerciante e dono do cartao
- Preparacao para Azure SQL Server e hospedagem no Azure

## Rubrica 4 - Microsoft Azure

**4.1 Microsoft Azure:** a aplicacao foi pensada para rodar como um web app publicado no Azure, separando configuracao, camadas e dependencia de ambiente para facilitar implantacao e manutencao.

**4.2 Armazenamento de dados no Microsoft Azure:** o uso de SQL gerenciado evita administrar servidor local, facilita backup, escala e reduz o trabalho operacional para um sistema com autenticacao, catalogo e transacoes.

**4.3 Azure SQL Database:** o banco relacional foi escolhido porque o sistema tem entidades conectadas entre si, precisa de integridade entre usuarios, planos, playlists, favoritos e transacoes, e o EF Core conversa naturalmente com esse modelo.

**4.4 Azure App Service:** o App Service atende bem uma aplicacao ASP.NET MVC + Web API porque simplifica publicacao, deployment e execucao da aplicacao sem exigir infraestrutura propria, mantendo a entrega mais direta para a demonstracao.

## Validacao executada

- Revisao de codigo e estrutura concluida no workspace.
- A compilacao nao foi fechada localmente porque o ambiente bloqueou acesso ao feed nuget.org.

