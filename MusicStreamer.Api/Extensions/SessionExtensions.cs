using System.Text.Json;
using MusicStreamer.Api.Models;

namespace MusicStreamer.Api.Extensions;

public static class SessionExtensions
{
    private const string SessionKey = "CurrentUser";

    public static void SetCurrentUser(this ISession session, UsuarioSessaoViewModel user)
    {
        session.SetString(SessionKey, JsonSerializer.Serialize(user));
    }

    public static UsuarioSessaoViewModel? GetCurrentUser(this ISession session)
    {
        var payload = session.GetString(SessionKey);
        return payload is null ? null : JsonSerializer.Deserialize<UsuarioSessaoViewModel>(payload);
    }

    public static void ClearCurrentUser(this ISession session)
    {
        session.Remove(SessionKey);
    }
}
