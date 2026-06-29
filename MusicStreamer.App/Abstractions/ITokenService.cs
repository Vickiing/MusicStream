using MusicStreamer.Domain.Entities;

namespace MusicStreamer.App.Abstractions;

public interface ITokenService
{
    string Generate(UserAccount account);
}
