using System.Text.Json.Serialization;

namespace ChatServer.Models;

public sealed class MessageRequest
{
    [JsonPropertyName("message")]
    public required string Message { get; init; }
}
