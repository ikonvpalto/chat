using Autofac;
using ChatServer.Services;
using ChatServer.Settings;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace ChatServer;

public sealed class ChatModule : Module
{
    protected override void Load(ContainerBuilder builder)
    {
        base.Load(builder);

        builder.Register<IMongoDatabase>(context =>
        {
            var settings = context.Resolve<IOptions<MongoSettings>>();

            var client = new MongoClient(settings.Value.ConnectionString);
            return client.GetDatabase(settings.Value.DatabaseName);
        });

        builder.RegisterType<MessageService>();
    }
}
