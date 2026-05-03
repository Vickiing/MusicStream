using System;
using System.Collections.Generic;
using System.Text;

namespace MusicStreamer.App.Contracts
{
    public interface IAssinaturaApp
    {
        Task<bool> AtivarAssinaturaAsync(int usuarioId);
        Task<bool> CancelarAssinaturaAsync(int usuarioId);
    }
}
