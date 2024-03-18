using SystemAggregator.Clients.Aggregator.Models;
using SystemAggregator.Core.Extensions;

namespace Aggregator.UseCases.Models
{
    public class SearchResponseBuilder
    {
        private IEnumerable<Route> _routes;

        public SearchResponseBuilder(IEnumerable<Route> routes)
        {
            _routes = routes;
        }

        public SearchResponseBuilder OnlyActual()
        {
            _routes = _routes.Where(x => x.TimeLimit < DateTime.UtcNow).ToList();

            return this;
        }

        public SearchResponse GetResult()
        {
            var response = new SearchResponse();

            var routeMinutesDurations = _routes
                .Select(x => DateTimeExtension.CalculateDateDiffInMinutes(x.OriginDateTime, x.DestinationDateTime))
                .ToList();

            response.Routes = _routes.ToArray();
            response.MinPrice = _routes.Min(x => x.Price);
            response.MaxPrice = _routes.Max(x => x.Price);
            response.MinMinutesRoute = routeMinutesDurations.Min();
            response.MaxMinutesRoute = routeMinutesDurations.Max();

            return response;
        }
    }
}
