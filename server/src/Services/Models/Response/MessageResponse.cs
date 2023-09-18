using System.Text.Json.Serialization;

namespace ChatServer.Services.Models.Response;

public sealed class MessageResponse
{
    [JsonPropertyName("text")]
    public required string Text { get; init; }
}
