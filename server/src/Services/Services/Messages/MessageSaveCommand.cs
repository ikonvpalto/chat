using ChatServer.Database.Entities.Messages;
using ChatServer.Services.Models.Request;
using MongoDB.Driver;

namespace ChatServer.Services.Services.Messages;

public sealed class MessageSaveCommand : ICommand<MessageRequest>
{
    private readonly IMongoCollection<Message> _collection;

    public MessageSaveCommand(IMongoCollection<Message> collection)
    {
        _collection = collection;
    }

    public async Task DoAsync(MessageRequest param, CancellationToken cancellationToken)
    {
        var entity = new Message
        {
            Text = param.Text,
            Sender = null
        };

        await _collection.InsertOneAsync(entity, null, cancellationToken);
    }
}
