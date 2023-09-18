using System.Text.Json.Serialization;

namespace ChatServer.Services.Models.Request;

public sealed class MessageRequest
{
    [JsonPropertyName("text")]
    public required string Text { get; init; }
}
