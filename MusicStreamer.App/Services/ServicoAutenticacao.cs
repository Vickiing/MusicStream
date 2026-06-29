using MusicStreamer.App.Abstractions;
using MusicStreamer.App.Contracts;
using MusicStreamer.App.DTOs;
using MusicStreamer.Domain.Entities;
using MusicStreamer.Domain.Repositories;

namespace MusicStreamer.App.Services;

public sealed class ServicoAutenticacao(
    IContaUsuarioRepository userAccountRepository,
    IHashSenha passwordHasher,
    IServicoToken tokenService) : IServicoAutenticacao
{
    public async Task<RespostaAutenticacaoDto> RegisterAsync(CadastrarUsuarioDto input, CancellationToken cancellationToken = default)
    {
        var existing = await userAccountRepository.GetByEmailAsync(input.Email, cancellationToken);
        if (existing is not null)
        {
            throw new InvalidOperationException("Ja existe uma conta cadastrada com este email.");
        }

        var account = ContaUsuario.Register(input.DisplayName, input.Email, passwordHasher.Hash(input.Password));
        await userAccountRepository.AddAsync(account, cancellationToken);

        return new RespostaAutenticacaoDto(account.Id, account.DisplayName, account.Email.Value, tokenService.Generate(account), account.SubscriptionPlanId);
    }

    public async Task<RespostaAutenticacaoDto?> LoginAsync(EntrarDto input, CancellationToken cancellationToken = default)
    {
        var account = await userAccountRepository.GetByEmailAsync(input.Email, cancellationToken);
        if (account is null || !passwordHasher.Verify(account.PasswordHash, input.Password))
        {
            return null;
        }

        account.RegisterLogin(DateTimeOffset.UtcNow);

        using var loginUpdateCts = CancellationTokenSource.CreateLinkedTokenSource(cancellationToken);
        loginUpdateCts.CancelAfter(TimeSpan.FromSeconds(3));

        try
        {
            await userAccountRepository.UpdateAsync(account, loginUpdateCts.Token);
        }
        catch (OperationCanceledException) when (!cancellationToken.IsCancellationRequested)
        {
            // Atualizacao de ultimo login nao deve impedir o acesso do usuario.
        }
        catch (Exception) when (!cancellationToken.IsCancellationRequested)
        {
            // Mantem o login funcional mesmo se a escrita auxiliar falhar.
        }

        return new RespostaAutenticacaoDto(account.Id, account.DisplayName, account.Email.Value, tokenService.Generate(account), account.SubscriptionPlanId);
    }
}


