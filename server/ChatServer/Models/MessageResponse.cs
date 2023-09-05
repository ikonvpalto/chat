using System.Text.Json.Serialization;

namespace ChatServer.Models;

public sealed class MessageResponse
{
    [JsonPropertyName("text")]
    public required string Text { get; init; }

    [JsonPropertyName("date")]
    public required DateTimeOffset Date { get; init; }
}
