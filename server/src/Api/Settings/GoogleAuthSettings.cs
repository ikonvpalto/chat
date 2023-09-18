namespace ChatServer.Api.Settings;

public sealed class GoogleAuthSettings
{
    public const string Section = "Auth:Google";

    public required string ClientId { get; init; }
    public required string ClientSecret { get; init; }
}
