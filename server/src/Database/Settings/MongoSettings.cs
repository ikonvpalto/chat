namespace ChatServer.Database.Settings;

public sealed class MongoSettings
{
    public const string Section = "Mongo";

    public required string ConnectionString { get; init; }
    public required string DatabaseName { get; init; }
    public required string MessagesCollectionName { get; init; }
}
