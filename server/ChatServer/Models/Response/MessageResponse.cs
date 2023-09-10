using System.Text.Json.Serialization;

namespace ChatServer.Models.Response;

public sealed class MessageResponse
{
    [JsonPropertyName("text")]
    public required string Text { get; init; }
}
