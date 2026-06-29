namespace MusicStreamer.App.DTOs;

public sealed record CadastrarUsuarioDto(string DisplayName, string Email, string Password);

public sealed record EntrarDto(string Email, string Password);

public sealed record RespostaAutenticacaoDto(Guid UserId, string DisplayName, string Email, string Token, Guid? SubscriptionPlanId);

