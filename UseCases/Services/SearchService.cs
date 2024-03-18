using Aggregator.UseCases.Interfaces;
using Aggregator.UseCases.Mapping;
using Aggregator.UseCases.Models;

using Caching.Interfaces;

using Microsoft.Extensions.Caching.Memory;

using SystemAggregator.Clients;
using SystemAggregator.Clients.Aggregator.Models;
using SystemAggregator.Clients.ProviderOne;
using SystemAggregator.Clients.ProviderTwo;

namespace Aggregator.UseCases.Services
{
    public class SearchService : ISearchService
    {
        private readonly IProviderOneClient _providerOneClient;
        private readonly IProviderTwoClient _providerTwoClient;

        private readonly IMemoryCache _cache;

        public SearchService(
            IProviderOneClient providerOneClient,
            IProviderTwoClient providerTwoClient,
            ICustomMemoryCache cache)
        {
            _providerOneClient = providerOneClient;
            _providerTwoClient = providerTwoClient;

            _cache = cache.MemoryCache;
        }

        public async Task<bool> IsAvailableAsync(CancellationToken cancellationToken)
        {
            bool isProviderOneAvailable;
            bool isProviderTwoAvailable;

            try
            {
                await _providerOneClient.PingAsync();

                isProviderOneAvailable = true;
            }
            catch (ApiClientException)
            {
                isProviderOneAvailable = false;
            }

            try
            {
                await _providerTwoClient.PingAsync();

                isProviderTwoAvailable = true;
            }
            catch (ApiClientException)
            {
                isProviderTwoAvailable = false;
            }

            return isProviderOneAvailable || isProviderTwoAvailable;
        }

        public async Task<SearchResponse> SearchAsync(SearchRequest request, CancellationToken cancellationToken)
        {
            var onlyCached = request.Filters?.OnlyCached ?? false;

            var providerOneResponse = await _providerOneClient.SearchAsync(request.ToProviderOneFormat());
            var providerOneRoutes = providerOneResponse?.Routes?.Select(SearchMapping.ToStandardFormat) ?? Enumerable.Empty<Route>();

            var providerTwoResponse = await _providerTwoClient.SearchAsync(request.ToProviderTwoFormat());
            var providerTwoRoutes = providerTwoResponse?.Routes?.Select(SearchMapping.ToStandardFormat) ?? Enumerable.Empty<Route>();

            var routes = providerOneRoutes.Union(providerTwoRoutes);

            var response = new SearchResponseBuilder(routes)
                .OnlyActual()
                .GetResult();

            SetToCache(response.Routes);

            return response;
        }

        private void SetToCache(IEnumerable<Route> routes)
        {
            foreach (var route in routes)
            {
                _cache.Set($"route_{route.Id}", route, new MemoryCacheEntryOptions()
                {
                    SlidingExpiration = TimeSpan.FromHours(1),
                    Size = 1024 * 1024
                });
            }
        }
    }
}
