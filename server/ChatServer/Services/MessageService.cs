using ChatServer.Models.Entity;
using ChatServer.Models.Request;
using ChatServer.Models.Response;
using MongoDB.Driver;

namespace ChatServer.Services;

public sealed class MessageService
{
    private const string CollectionName = "messages";

    private readonly IMongoDatabase _database;

    public MessageService(IMongoDatabase database)
    {
        _database = database;
    }

    private IMongoCollection<Message> Collection => _database.GetCollection<Message>(CollectionName);

    public async Task SaveAsync(MessageRequest messageRequest, CancellationToken cancellationToken = default)
    {
        var entity = new Message()
        {
            Text = messageRequest.Text
        };

        await Collection.InsertOneAsync(entity, null, cancellationToken);
    }

    public async Task<IReadOnlyCollection<MessageResponse>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        var cursor = await Collection.FindAsync(_ => true, null, cancellationToken);
        var messages = await cursor.ToListAsync(cancellationToken);

        return messages
            .Select(m => new MessageResponse
            {
                Text = m.Text
            })
            .ToArray();
    }
}
