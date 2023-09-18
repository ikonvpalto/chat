namespace ChatServer.Api.Settings;

public sealed class JwtSettings
{
    public const string Section = "Jwt";

    public required string Secret { get; init; }
    public required string ValidIssuer { get; init; }
    public required string ValidAudience { get; init; }
    public required string DurationInMinutes { get; init; }
    public required string RefreshTokenExpiration { get; init; }
}
