# MusicStreamer

Aplicação de streaming de música desenvolvida em ASP.NET Core com arquitetura em camadas.

## Estrutura

- `MusicStreamer.Api`: camada de apresentação, MVC e Web API.
- `MusicStreamer.App`: camada de serviços e DTOs.
- `MusicStreamer.Domain`: entidades, value objects, regras de negócio e contratos.
- `StreamerMusic.infrastructure`: acesso a dados, EF Core, repositórios, migrations e seed.

#### Modelo do dominio: 
<img width="1408" height="768" alt="modelo_dominio" src="https://github.com/user-attachments/assets/37053b60-1e11-48d5-ae83-d0c6c0f8a46d" />

## Como configurar

Defina a connection string do Azure SQL Server em `ConnectionStrings:AzureSqlConnection`.

Exemplo em [appsettings.json](./MusicStreamer.Api/appsettings.json):

```json
{
  "ConnectionStrings": {
    "AzureSqlConnection": "Server=tcp:SEU-SERVIDOR.database.windows.net,1433;Initial Catalog=MusicStreamerDb;Persist Security Info=False;User ID=USUARIO;Password=SENHA;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;"
  }
}
```

## Como executar

1. Configure a connection string do banco.
2. Compile o projeto:

```powershell
dotnet build MusicStreamer.slnx --no-restore
```

3. Execute a aplicação:

```powershell
dotnet run --project MusicStreamer.Api/MusicStreamer.Api.csproj
```

4. No startup, a aplicação executa automaticamente as migrations e o seed inicial.

## Evidências das rubricas

Os links abaixo apontam diretamente para os arquivos usados como evidência.

### 1. Desenvolvimento de sistemas web e utilização de arquiteturas em camadas

| Item | Evidência |
| --- | --- |
| Camada de apresentação | [PainelController.cs](./MusicStreamer.Api/Controllers/PainelController.cs), [Home.cshtml](./MusicStreamer.Api/Views/Painel/Home.cshtml), [Catalog.cshtml](./MusicStreamer.Api/Views/Painel/Catalog.cshtml), [Plans.cshtml](./MusicStreamer.Api/Views/Painel/Plans.cshtml), [Playlists.cshtml](./MusicStreamer.Api/Views/Painel/Playlists.cshtml), [Search.cshtml](./MusicStreamer.Api/Views/Painel/Search.cshtml), [Favorites.cshtml](./MusicStreamer.Api/Views/Painel/Favorites.cshtml), [Transactions.cshtml](./MusicStreamer.Api/Views/Painel/Transactions.cshtml), [Shared/_Layout.cshtml](./MusicStreamer.Api/Views/Shared/_Layout.cshtml), [Shared/_Sidebar.cshtml](./MusicStreamer.Api/Views/Shared/_Sidebar.cshtml), [Login.cshtml](./MusicStreamer.Api/Views/ContaUsuarioMvc/Login.cshtml), [Register.cshtml](./MusicStreamer.Api/Views/ContaUsuarioMvc/Register.cshtml) |
| Camada de serviços | [ServicoAutenticacao.cs](./MusicStreamer.App/Services/ServicoAutenticacao.cs), [ServicoPlaylist.cs](./MusicStreamer.App/Services/ServicoPlaylist.cs), [ServicoTransacoes.cs](./MusicStreamer.App/Services/ServicoTransacoes.cs), [ServicoFavoritos.cs](./MusicStreamer.App/Services/ServicoFavoritos.cs), [ServicoCatalogo.cs](./MusicStreamer.App/Services/ServicoCatalogo.cs), [ServicoPlanosAssinatura.cs](./MusicStreamer.App/Services/ServicoPlanosAssinatura.cs) |
| Camada de negócios | [ContaUsuario.cs](./MusicStreamer.Domain/Entities/ContaUsuario.cs), [Playlist.cs](./MusicStreamer.Domain/Entities/Playlist.cs), [ServicoAutorizacaoTransacao.cs](./MusicStreamer.Domain/Services/ServicoAutorizacaoTransacao.cs), [DecisaoAutorizacaoTransacao.cs](./MusicStreamer.Domain/Services/DecisaoAutorizacaoTransacao.cs) |
| Camada de acesso a dados | [MusicStreamerDbContext.cs](./StreamerMusic.infrastructure/Data/MusicStreamerDbContext.cs), [ContaUsuarioRepository.cs](./StreamerMusic.infrastructure/Repositories/ContaUsuarioRepository.cs), [TransacaoRepository.cs](./StreamerMusic.infrastructure/Repositories/TransacaoRepository.cs), [InicializadorBancoDados.cs](./StreamerMusic.infrastructure/Data/InicializadorBancoDados.cs), [CatalogoRepository.cs](./StreamerMusic.infrastructure/Repositories/CatalogoRepository.cs) |

### 2. Projetar aplicativos Web com ASP.NET MVC e Web API

| Item | Evidência |
| --- | --- |
| Cadastro e login | [AutenticacaoController.cs](./MusicStreamer.Api/Controllers/AutenticacaoController.cs), [ContaUsuarioMvcController.cs](./MusicStreamer.Api/Controllers/ContaUsuarioMvcController.cs), [Pbkdf2PasswordHasher.cs](./StreamerMusic.infrastructure/Security/Pbkdf2PasswordHasher.cs), [JwtTokenService.cs](./StreamerMusic.infrastructure/Security/JwtTokenService.cs) |
| Transação | [TransacoesController.cs](./MusicStreamer.Api/Controllers/TransacoesController.cs), [PainelController.cs](./MusicStreamer.Api/Controllers/PainelController.cs), [Transactions.cshtml](./MusicStreamer.Api/Views/Painel/Transactions.cshtml), [ServicoTransacoes.cs](./MusicStreamer.App/Services/ServicoTransacoes.cs), [TransactionNotification.cs](./MusicStreamer.Domain/Entities/TransactionNotification.cs) |
| Busca de música | [CatalogoController.cs](./MusicStreamer.Api/Controllers/CatalogoController.cs), [PainelController.cs](./MusicStreamer.Api/Controllers/PainelController.cs), [Search.cshtml](./MusicStreamer.Api/Views/Painel/Search.cshtml), [CatalogoRepository.cs](./StreamerMusic.infrastructure/Repositories/CatalogoRepository.cs), [MusicStreamerDbContext.cs](./StreamerMusic.infrastructure/Data/MusicStreamerDbContext.cs) |
| Favoritar música | [FavoritosController.cs](./MusicStreamer.Api/Controllers/FavoritosController.cs), [PainelController.cs](./MusicStreamer.Api/Controllers/PainelController.cs), [Search.cshtml](./MusicStreamer.Api/Views/Painel/Search.cshtml), [Favorites.cshtml](./MusicStreamer.Api/Views/Painel/Favorites.cshtml), [ServicoFavoritos.cs](./MusicStreamer.App/Services/ServicoFavoritos.cs) |

### 3. Implementar acesso a dados utilizando Entity Framework

| Item | Evidência |
| --- | --- |
| Modelo EF | [MusicStreamerDbContext.cs](./StreamerMusic.infrastructure/Data/MusicStreamerDbContext.cs) |
| Migrations | [20260629032353_CriacaoInicial.cs](./StreamerMusic.infrastructure/Migrations/20260629032353_CriacaoInicial.cs), [InicializadorBancoDados.cs](./StreamerMusic.infrastructure/Data/InicializadorBancoDados.cs) |
| Repository pattern | [IUserAccountRepository.cs](./MusicStreamer.Domain/Repositories/IUserAccountRepository.cs), [IPlaylistRepository.cs](./MusicStreamer.Domain/Repositories/IPlaylistRepository.cs), [ITransactionRepository.cs](./MusicStreamer.Domain/Repositories/ITransactionRepository.cs), [ContaUsuarioRepository.cs](./StreamerMusic.infrastructure/Repositories/ContaUsuarioRepository.cs), [TransacaoRepository.cs](./StreamerMusic.infrastructure/Repositories/TransacaoRepository.cs) |
| Injeção de dependência | [Program.cs](./MusicStreamer.Api/Program.cs) |

### 4. Disponibilizar aplicativos Web no Microsoft Azure

#### 4.1 Demonstração do Microsoft Azure

A aplicação foi preparada para rodar com serviços gerenciados da Azure, porque isso reduz o trabalho de infraestrutura, facilita a publicação e mantém o foco na aplicação. O ponto principal aqui é usar um ambiente de hospedagem simples de manter e fácil de revisar, com separação clara entre aplicação e banco.

Arquivos de apoio: [Program.cs](./MusicStreamer.Api/Program.cs) e [appsettings.json](./MusicStreamer.Api/appsettings.json).

#### 4.2 Serviços de armazenamento de dados do Microsoft Azure

A escolha do Azure SQL Database atende ao cenário de dados relacionais da aplicação, que precisa de integridade, consultas consistentes e fácil manutenção. Além disso, por ser um serviço gerenciado, ele reduz a responsabilidade operacional com backup, disponibilidade e manutenção do servidor.

Arquivo de apoio: [appsettings.json](./MusicStreamer.Api/appsettings.json).

#### 4.3 Serviço de SQL do Microsoft Azure

O Azure SQL foi usado porque o domínio da aplicação é relacional: usuários, planos, transações, playlists, músicas e favoritos possuem vínculos fortes entre si. Isso combina bem com SQL, migrations e EF Core, permitindo evoluir o banco sem refazer a aplicação manualmente.

Arquivos de apoio: [Program.cs](./MusicStreamer.Api/Program.cs), [appsettings.json](./MusicStreamer.Api/appsettings.json), [20260629032353_CriacaoInicial.cs](./StreamerMusic.infrastructure/Migrations/20260629032353_CriacaoInicial.cs), [InicializadorBancoDados.cs](./StreamerMusic.infrastructure/Data/InicializadorBancoDados.cs).

#### 4.4 Serviço de aplicativos Web do Microsoft Azure

O App Service é a opção mais adequada para publicar a aplicação web porque simplifica o deploy, a execução contínua e a configuração de ambiente. Como o projeto possui MVC e Web API na mesma solução, esse modelo facilita a exposição da aplicação sem exigir gerenciamento manual de servidor.

Arquivos de apoio: [Program.cs](./MusicStreamer.Api/Program.cs), [Shared/_Layout.cshtml](./MusicStreamer.Api/Views/Shared/_Layout.cshtml) e [Shared/_Sidebar.cshtml](./MusicStreamer.Api/Views/Shared/_Sidebar.cshtml).

## Observações funcionais

- O seed inicial é executado no startup.
- O projeto deixa a base pronta para cadastro, login, assinatura, busca, favoritos, playlists e transações.
