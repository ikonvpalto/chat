using System.Text.Json.Serialization;

namespace ChatServer.Models.Request;

public sealed class MessageRequest
{
    [JsonPropertyName("text")]
    public required string Text { get; init; }
}
