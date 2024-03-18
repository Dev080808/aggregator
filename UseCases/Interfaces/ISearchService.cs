using SystemAggregator.Clients.Aggregator.Models;

namespace Aggregator.UseCases.Interfaces
{
    public interface ISearchService
    {
        Task<SearchResponse> SearchAsync(SearchRequest request, CancellationToken cancellationToken);

        Task<bool> IsAvailableAsync(CancellationToken cancellationToken);
    }
}
