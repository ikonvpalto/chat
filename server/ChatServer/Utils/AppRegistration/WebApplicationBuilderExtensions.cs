using System.Reflection;
using Autofac;
using Autofac.Extensions.DependencyInjection;

namespace ChatServer.Utils.AppRegistration;

public static class WebApplicationBuilderExtensions
{
    public static WebApplicationBuilder AddAutofac(this WebApplicationBuilder webApplicationBuilder, params Assembly[] assemblies)
    {
        if (!assemblies.Any())
        {
            assemblies = new[] { Assembly.GetCallingAssembly() };
        }

        webApplicationBuilder.Host.UseServiceProviderFactory(new AutofacServiceProviderFactory());
        webApplicationBuilder.Host.ConfigureContainer<ContainerBuilder>(b =>
        {
            b.RegisterAssemblyModules(assemblies);
        });

        return webApplicationBuilder;
    }

    public static WebApplicationBuilder ConfigureSettings<TSettings>(this WebApplicationBuilder webApplicationBuilder, string section)
        where TSettings : class
    {
        webApplicationBuilder.Services.Configure<TSettings>(
            webApplicationBuilder.Configuration.GetSection(section));

        return webApplicationBuilder;
    }

    public static WebApplicationBuilder AddDefaultAllowAllCors(this WebApplicationBuilder webApplicationBuilder)
    {
        webApplicationBuilder.Services.AddCors(options =>
        {
            options.AddDefaultPolicy(b => b
                .SetIsOriginAllowed(_ => true)
                .AllowAnyHeader()
                .AllowAnyMethod()
                .AllowCredentials());
        });

        return webApplicationBuilder;
    }

    public static WebApplicationBuilder AddControllers(this WebApplicationBuilder webApplicationBuilder)
    {
        webApplicationBuilder.Services.AddControllers();

        return webApplicationBuilder;
    }

    public static WebApplicationBuilder AddSwagger(this WebApplicationBuilder webApplicationBuilder)
    {
        webApplicationBuilder.Services.AddEndpointsApiExplorer();
        webApplicationBuilder.Services.AddSwaggerGen();

        return webApplicationBuilder;
    }

    public static WebApplicationBuilder AddSignalR(this WebApplicationBuilder webApplicationBuilder)
    {
        webApplicationBuilder.Services.AddSignalR();

        return webApplicationBuilder;
    }
}
