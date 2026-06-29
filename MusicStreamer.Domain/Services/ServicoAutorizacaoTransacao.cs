using MusicStreamer.Domain.Entities;
using MusicStreamer.Domain.Enums;

namespace MusicStreamer.Domain.Services;

public sealed class ServicoAutorizacaoTransacao : IServicoAutorizacaoTransacao
{
    public DecisaoAutorizacaoTransacao Authorize(
        ContaUsuario account,
        PlanoAssinatura? plan,
        Comerciante merchant,
        decimal amount,
        DateTimeOffset requestedAtUtc,
        Transaction? lastApprovedTransaction)
    {
        if (account.Status != StatusConta.Active)
        {
            return new DecisaoAutorizacaoTransacao(false, "negada: conta inativa");
        }

        if (plan is null || !plan.IsActive)
        {
            return new DecisaoAutorizacaoTransacao(false, "negada: assinatura inexistente");
        }

        if (!merchant.IsActive)
        {
            return new DecisaoAutorizacaoTransacao(false, "negada: comerciante inativo");
        }

        if (amount <= 0)
        {
            return new DecisaoAutorizacaoTransacao(false, "negada: valor invalido");
        }

        if (amount > plan.MaxTransactionAmount)
        {
            return new DecisaoAutorizacaoTransacao(false, "negada: valor acima do limite do plano");
        }

        if (requestedAtUtc.Hour < 6 && !plan.AllowsNightTransactions)
        {
            return new DecisaoAutorizacaoTransacao(false, "negada: horario fora da politica do plano");
        }

        if (lastApprovedTransaction is not null
            && lastApprovedTransaction.MerchantId == merchant.Id
            && lastApprovedTransaction.Amount == amount
            && requestedAtUtc - lastApprovedTransaction.RequestedAtUtc <= TimeSpan.FromMinutes(2))
        {
            return new DecisaoAutorizacaoTransacao(false, "negada: tentativa duplicada apos ultima autorizacao");
        }

        return new DecisaoAutorizacaoTransacao(true, "aprovada");
    }
}

