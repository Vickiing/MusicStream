using MusicStreamer.Domain.Entities;

namespace MusicStreamer.App.Abstractions;

public interface IServicoToken
{
    string Generate(ContaUsuario account);
}

