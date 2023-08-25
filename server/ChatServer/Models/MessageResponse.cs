using System.Text.Json.Serialization;

namespace ChatServer.Models;

public sealed class MessageResponse
{
    [JsonPropertyName("message")]
    public required string Message { get; init; }

    [JsonPropertyName("date")]
    public required DateTimeOffset Date { get; init; }
}
