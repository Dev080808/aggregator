using Aggregator.ApplicationServices.Implementation;
using Aggregator.ApplicationServices.Interfaces;
using Aggregator.Caching.InMemory;
using Aggregator.UseCases.Interfaces;
using Aggregator.UseCases.Services;

using Caching.Interfaces;

using SystemAggregator.Clients.ProviderOne;
using SystemAggregator.Clients.ProviderTwo;

namespace Aggregator.Site.Extensions
{
    public static class HostBuilderExtensions
    {
        public static void AddDomainServices(this IHostBuilder builder)
        {
            builder.ConfigureServices(services =>
            {
            });
        }

        public static void AddApplicationServices(this IHostBuilder builder)
        {
            builder.ConfigureServices(services =>
            {
                services.AddTransient<IHealthCheckService, HealthCheckService>();
            });
        }

        public static void AddUseCases(this IHostBuilder builder)
        {
            builder.AddProviderOneClient();
            builder.AddProviderTwoClient();

            builder.ConfigureServices((context, services) =>
            {
                services.AddCache();

                services.AddTransient<ISearchService, SearchService>();
            });
        }

        private static IServiceCollection AddCache(this IServiceCollection services)
        {
            services.AddSingleton<ICustomMemoryCache, CustomMemoryCache>();

            return services;
        }
    }
}
