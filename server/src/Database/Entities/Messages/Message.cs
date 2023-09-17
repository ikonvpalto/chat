using MongoDB.Bson.Serialization.Attributes;

namespace ChatServer.Database.Entities.Messages;

public sealed class Message
{
    [BsonId]
    public Guid Id { get; set; }

    [BsonElement("text")]
    public required string Text { get; init; }

    [BsonElement("sender")]
    public required MessageSender Sender { get; init; }
}
