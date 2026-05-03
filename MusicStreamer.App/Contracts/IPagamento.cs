using System;
using System.Collections.Generic;
using System.Text;

namespace MusicStreamer.App.Contracts
{
    public interface IPagamento
    {
        Task<bool> ProcessarPagamentoAsync(Guid usuarioId, decimal valor);
    }
}
