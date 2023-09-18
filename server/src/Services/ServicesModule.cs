using Autofac;
using ChatServer.Database.Entities.Messages;
using ChatServer.Services.Services.Messages;
using MongoDB.Driver;

namespace ChatServer.Services;

public sealed class ServicesModule : Module
{
    private const string MessageCollectionName = "messages";

    protected override void Load(ContainerBuilder builder)
    {
        base.Load(builder);

        builder.RegisterType<MessageGetAllQuery>();
        builder.RegisterType<MessageSaveCommand>();

        builder.Register<IMongoCollection<Message>>(context =>
        {
            var db = context.Resolve<IMongoDatabase>();
            return db.GetCollection<Message>(MessageCollectionName);
        });
    }
}
