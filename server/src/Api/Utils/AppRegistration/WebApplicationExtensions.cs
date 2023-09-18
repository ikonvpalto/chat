using System.Reflection;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.SignalR;

namespace ChatServer.Api.Utils.AppRegistration;

public static class WebApplicationExtensions
{
    public static WebApplication UseSwaggerDoc(this WebApplication webApplication)
    {
        webApplication.UseSwagger();
        webApplication.UseSwaggerUI();

        return webApplication;
    }

    public static WebApplication MapSignalRHubs(this WebApplication webApplication)
    {
        var assembly = Assembly.GetCallingAssembly();

        foreach (var hub in assembly.GetTypes().Where(t => t.IsAssignableTo(typeof(Hub))))
        {
            var routeAttribute = hub.GetCustomAttribute<RouteAttribute>(true);

            if (string.IsNullOrWhiteSpace(routeAttribute?.Template))
            {
                continue;
            }

            // call app.MapHub<YourHub>("/your/hub/url") with reflection;
            typeof(HubEndpointRouteBuilderExtensions)
                .GetMethod(
                    nameof(HubEndpointRouteBuilderExtensions.MapHub),
                    new [] { typeof(IEndpointRouteBuilder), typeof(string) })
                ?.MakeGenericMethod(hub)
                .Invoke(null, new object[] { webApplication, routeAttribute.Template });
        }

        return webApplication;
    }
}
