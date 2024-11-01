using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace HistoryBlazor.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddHistoryBlazor(
        this IServiceCollection services,
        ServiceLifetime lifeTime = ServiceLifetime.Scoped,
        object? key = null)
    {
        services.TryAdd(new ServiceDescriptor(
            typeof(IHistoryBlazor), key, typeof(HistoryBlazor), lifeTime));
        return services;
    }
}
