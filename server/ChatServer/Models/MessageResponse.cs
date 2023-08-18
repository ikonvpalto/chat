namespace ChatServer.Models;

public sealed class MessageResponse
{
    public required string Message { get; init; }

    public required DateTimeOffset Date { get; init; }
}
