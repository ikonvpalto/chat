using ChatServer.Database.Entities.Messages;
using ChatServer.Services.Models.Response;
using MongoDB.Driver;

namespace ChatServer.Services.Services.Messages;

public sealed class MessageGetAllQuery : IQuery<IReadOnlyCollection<MessageResponse>>
{
    private readonly IMongoCollection<Message> _collection;

    public MessageGetAllQuery(IMongoCollection<Message> collection)
    {
        _collection = collection;
    }

    public async Task<IReadOnlyCollection<MessageResponse>> QueryAsync(CancellationToken cancellationToken)
    {
        var cursor = await _collection.FindAsync(_ => true, null, cancellationToken);
        var messages = await cursor.ToListAsync(cancellationToken);

        return messages
            .Select(m => new MessageResponse
            {
                Text = m.Text
            })
            .ToArray();
    }
}
