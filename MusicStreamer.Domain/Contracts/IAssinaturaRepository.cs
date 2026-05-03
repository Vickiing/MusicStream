using System;
using System.Collections.Generic;
using System.Text;

namespace MusicStreamer.Domain.Contracts
{
    public interface IAssinaturaRepository
    {
        Task<bool> AtivarAssinaturaUsuarioAsync(int usuarioId);
        Task<bool> CancelarAssinaturaUsuarioAsync(int usuarioId);
    }
}
