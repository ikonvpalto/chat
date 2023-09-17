using Autofac;
using ChatServer.Services;

namespace ChatServer;

public sealed class ChatModule : Module
{
    protected override void Load(ContainerBuilder builder)
    {
        base.Load(builder);

        builder.RegisterType<MessageService>();
    }
}
