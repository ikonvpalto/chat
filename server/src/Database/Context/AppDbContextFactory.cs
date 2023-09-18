using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace ChatServer.Database.Context;

public sealed class AppDbContextFactory : IDesignTimeDbContextFactory<AppDbContext>
{
    private const string ConnectionStringName = "Default";

    public AppDbContext CreateDbContext(string[] args)
    {
        var appsettingsPath = Path.Join(Directory.GetCurrentDirectory(), "..", "Api", "appsettings.json");
        var localAppsettingsPath = Path.Join(Directory.GetCurrentDirectory(), "..", "Api", "appsettings.local.json");

        var configuration = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile(appsettingsPath)
            .AddJsonFile(localAppsettingsPath)
            .AddCommandLine(args)
            .Build();

        return CreateDbContext(configuration);
    }

    public AppDbContext CreateDbContext(IConfiguration configuration)
    {
        var options = CreateDbContextOptions(configuration);

        return new (options);
    }

    public DbContextOptions<AppDbContext> CreateDbContextOptions(IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString(ConnectionStringName);

        var options = new DbContextOptionsBuilder<AppDbContext>();
        options.UseNpgsql(connectionString);

        return options.Options;
    }
}
