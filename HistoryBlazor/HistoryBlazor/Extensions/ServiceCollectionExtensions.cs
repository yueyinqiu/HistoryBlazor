using Microsoft.Extensions.DependencyInjection;

namespace HistoryBlazor.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddHistoryBlazor(
        this IServiceCollection services,
        ServiceLifetime lifeTime = ServiceLifetime.Scoped,
        object? key = null)
    {
        services.Add(new ServiceDescriptor(
            typeof(ISyncHistoryBlazor), key, typeof(HistoryBlazor), lifeTime));
        return services;
    }
}
