using MongoDB.Bson.Serialization.Attributes;

namespace ChatServer.Database.Entities.Messages;

public sealed class MessageSender
{
    [BsonElement("userId")]
    public required Guid UserId { get; init; }

    [BsonElement("username")]
    public required string Username { get; init; }
}
