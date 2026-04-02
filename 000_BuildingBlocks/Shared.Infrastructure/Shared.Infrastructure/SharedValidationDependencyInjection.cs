using FluentValidation;
using Mediator;
using Microsoft.Extensions.DependencyInjection;
using Shared.Infrastructure.Validation;
using System.Reflection;

namespace Shared.Infrastructure;

public static class SharedValidationDependencyInjection
{
    public static IServiceCollection AddSharedValidation(this IServiceCollection services, params Assembly[] assemblies)
    {
        // 1. FluentValidation’ı parametre olarak gelen assembly’lerden tarayıp kaydet
        services.AddValidatorsFromAssemblies(assemblies);

        // 2. Mediator Pipeline Behavior olarak ValidationBehavior’ı kaydet
        // Mediator Source Generator’da Registration IPipelineBehavior<,> üzerinden yapılır.
        services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));

        return services;
    }
}
