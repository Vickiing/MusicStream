using MusicStreamer.App.Contracts;
using MusicStreamer.App.DTOs;
using MusicStreamer.Domain.Entities;
using MusicStreamer.Domain.Repositories;
using MusicStreamer.Domain.Services;

namespace MusicStreamer.App.Services;

public sealed class ServicoTransacoes(
    IContaUsuarioRepository userAccountRepository,
    IPlanoAssinaturaRepository subscriptionPlanRepository,
    IComercianteRepository merchantRepository,
    ITransacaoRepository transactionRepository,
    IServicoAutorizacaoTransacao authorizationDomainService) : IServicoTransacoes
{
    public async Task<ResultadoTransacaoDto?> AuthorizeAsync(AutorizarTransacaoDto input, CancellationToken cancellationToken = default)
    {
        var user = await userAccountRepository.GetByIdAsync(input.UserId, cancellationToken);
        var merchant = await merchantRepository.GetByIdAsync(input.MerchantId, cancellationToken);
        if (user is null || merchant is null)
        {
            return null;
        }

        var plan = user.SubscriptionPlanId.HasValue
            ? await subscriptionPlanRepository.GetByIdAsync(user.SubscriptionPlanId.Value, cancellationToken)
            : null;

        var lastApproved = await transactionRepository.GetLastApprovedTransactionAsync(user.Id, cancellationToken);
        var decision = authorizationDomainService.Authorize(user, plan, merchant, input.Amount, input.RequestedAtUtc, lastApproved);

        var transaction = Transaction.Create(
            user.Id,
            merchant.Id,
            input.Amount,
            input.RequestedAtUtc,
            decision.IsApproved,
            decision.Reason);

        transaction.AddNotification(NotificacaoTransacao.CreateMerchantNotification(transaction.Id, merchant.Name, decision));
        transaction.AddNotification(NotificacaoTransacao.CreateCardOwnerNotification(transaction.Id, user.Email.Value, decision));

        await transactionRepository.AddAsync(transaction, cancellationToken);
        return Map(transaction);
    }

    public async Task<IReadOnlyList<ResultadoTransacaoDto>> GetByUserAsync(Guid userId, CancellationToken cancellationToken = default)
    {
        var transactions = await transactionRepository.GetByUserAsync(userId, cancellationToken);
        return transactions.Select(Map).ToList();
    }

    private static ResultadoTransacaoDto Map(Transaction transaction)
    {
        return new ResultadoTransacaoDto(
            transaction.Id,
            transaction.UserAccountId,
            transaction.MerchantId,
            transaction.Amount,
            transaction.Status.ToString(),
            transaction.Reason,
            transaction.RequestedAtUtc,
            transaction.Notifications
                .Select(notification => new NotificacaoTransacaoDto(
                    notification.Recipient,
                    notification.Channel,
                    notification.Status.ToString(),
                    notification.Message))
                .ToList());
    }
}


