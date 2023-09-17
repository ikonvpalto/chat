using Autofac;
using ChatServer.Database.Context;
using ChatServer.Database.Settings;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace ChatServer.Database;

public sealed class DatabaseModule : Module
{
    private readonly IConfiguration _configuration;

    public DatabaseModule(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    protected override void Load(ContainerBuilder builder)
    {
        base.Load(builder);

        builder.Register<IMongoDatabase>(context =>
        {
            var settings = context.Resolve<IOptions<MongoSettings>>();

            var client = new MongoClient(settings.Value.ConnectionString);
            return client.GetDatabase(settings.Value.DatabaseName);
        });

        var dbCtxBuilder = new AppDbContextFactory();

        builder.Register(_ => dbCtxBuilder.CreateDbContext(_configuration));
        builder.Register(_ => dbCtxBuilder.CreateDbContextOptions(_configuration));
    }
}
