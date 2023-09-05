using System.Text.Json.Serialization;

namespace ChatServer.Models;

public sealed class MessageRequest
{
    [JsonPropertyName("text")]
    public required string Text { get; init; }
}
