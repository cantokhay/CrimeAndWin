using Microsoft.Extensions.DependencyInjection;
using System.Reflection;
using Shared.Application.Abstractions.Messaging;
using Microsoft.Extensions.Configuration;

namespace Shared.Infrastructure
{
    public static class ServiceRegistration
    {
        public static IServiceCollection AddSharedMail(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<Mail.MailOptions>(configuration.GetSection("Mail"));
            services.AddScoped<Mail.IMailService, Mail.MailService>();
            return services;
        }

        public static IServiceCollection AddRequestHandlers(this IServiceCollection services, Assembly assembly)
        {
            var handlers = assembly.GetTypes()
                .Where(t => !t.IsAbstract && !t.IsInterface)
                .SelectMany(t => t.GetInterfaces()
                    .Where(i => i.IsGenericType &&
                                i.GetGenericTypeDefinition() == typeof(IRequestHandler<,>)),
                    (t, i) => new { Implementation = t, Interface = i });

            foreach (var handler in handlers)
            {
                services.AddScoped(handler.Interface, handler.Implementation);
            }

            return services;
        }
    }
}
