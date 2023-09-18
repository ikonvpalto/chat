using Autofac;
using Autofac.Extensions.DependencyInjection;
using ChatServer.Database.Context;
using ChatServer.Database.Entities.Identity;
using Microsoft.AspNetCore.Identity;
using Module = Autofac.Module;

namespace ChatServer.Api.Utils.AppRegistration;

public static class WebApplicationBuilderExtensions
{
    public static WebApplicationBuilder AddConfigFile(this WebApplicationBuilder builder, string path, bool optional = true)
    {
        builder.Configuration.AddJsonFile(path, optional);
        return builder;
    }

    public static WebApplicationBuilder AddAutofac(this WebApplicationBuilder builder, params Module[] modules)
    {
        builder.Host.UseServiceProviderFactory(new AutofacServiceProviderFactory());
        builder.Host.ConfigureContainer<ContainerBuilder>(b =>
        {
            foreach (var module in modules)
            {
                b.RegisterModule(module);
            }
        });

        return builder;
    }

    public static WebApplicationBuilder ConfigureSettings<TSettings>(this WebApplicationBuilder builder, string section)
        where TSettings : class
    {
        builder.Services.Configure<TSettings>(
            builder.Configuration.GetSection(section));

        return builder;
    }

    public static WebApplicationBuilder AddDefaultAllowAllCors(this WebApplicationBuilder builder)
    {
        builder.Services.AddCors(options =>
        {
            options.AddDefaultPolicy(b => b
                .SetIsOriginAllowed(_ => true)
                .AllowAnyHeader()
                .AllowAnyMethod()
                .AllowCredentials());
        });

        return builder;
    }

    public static WebApplicationBuilder AddControllers(this WebApplicationBuilder builder)
    {
        builder.Services.AddControllers(options =>
        {
            options.RespectBrowserAcceptHeader = true;
        });

        return builder;
    }

    public static WebApplicationBuilder AddSwagger(this WebApplicationBuilder builder)
    {
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();

        return builder;
    }

    public static WebApplicationBuilder AddSignalR(this WebApplicationBuilder builder)
    {
        builder.Services.AddSignalR();

        return builder;
    }

    public static WebApplicationBuilder AddIdentity(this WebApplicationBuilder builder)
    {
        builder.Services
            .AddIdentity<User, Role>(options =>
            {
                options.Password.RequiredLength = 8;

                options.Lockout.AllowedForNewUsers = true;
                options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(10);
                options.Lockout.MaxFailedAccessAttempts = 3;
                options.User.RequireUniqueEmail = true;
            })
            .AddEntityFrameworkStores<AppDbContext>()
            .AddDefaultTokenProviders();

        return builder;
    }
}
