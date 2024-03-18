using SystemAggregator.Clients.Aggregator.Models;

namespace Aggregator.UseCases.Models
{
    internal class RouteCacheEntry
    {
        public RouteCacheEntry(Route route)
        {
            Route = route;
        }

        public Route Route { get; private set; }
    }
}
