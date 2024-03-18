using Aggregator.UseCases.Interfaces;

using Asp.Versioning;

using Microsoft.AspNetCore.Mvc;

using Swashbuckle.AspNetCore.Annotations;

using SystemAggregator.Clients.Aggregator.Models;
using SystemAggregator.Site.Exceptions;

namespace Aggregator.Site.Controllers.Api
{
    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/search")]
    [SwaggerTag("Search API")]
    public class SearchController : ControllerBase
    {
        private readonly ISearchService _searchService;

        public SearchController(ISearchService searchService)
        {
            _searchService = searchService;
        }

        [HttpPost]
        public async Task<IActionResult> Post(SearchRequest request, CancellationToken cancellationToken)
        {
            var onlyCached = request.Filters?.OnlyCached ?? false;

            var isAvailable = await _searchService.IsAvailableAsync(cancellationToken);

            if (!onlyCached && !isAvailable)
            {
                return ExceptionGenerator.GetServiceUnavailableResult();
            }

            var result = await _searchService.SearchAsync(request, cancellationToken);

            return Ok(result);
        }
    }
}