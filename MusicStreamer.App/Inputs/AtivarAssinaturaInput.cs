using System;
using System.Collections.Generic;
using System.Text;

namespace MusicStreamer.App.Inputs
{
    public class AtivarAssinaturaInput
    {
        public int UsuarioId { get; set; }
        public int TipoPagamento { get; set; }
        public int TipoAssinatura { get; set; }
        public bool RenovacaoAutomatica { get; set; }
    }
}
