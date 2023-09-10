using MongoDB.Bson.Serialization.Attributes;

namespace ChatServer.Models.Entity;

public sealed class Message
{
    [BsonId]
    public Guid Id { get; set; }

    [BsonElement("text")]
    public required string Text { get; init; }
}
