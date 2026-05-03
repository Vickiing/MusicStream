using MusicStreamer.App.Contracts;
using System;
using System.Collections.Generic;
using System.Text;

namespace MusicStreamer.App.Services
{
    public class AssinaturaApp : IAssinaturaApp
    {
        public Task<bool> AtivarAssinaturaAsync(int usuarioId)
        {
            throw new NotImplementedException();
        }

        public Task<bool> CancelarAssinaturaAsync(int usuarioId)
        {
            throw new NotImplementedException();
        }
    }
}
