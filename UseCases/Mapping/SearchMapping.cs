using SystemAggregator.Clients.Aggregator.Models;
using SystemAggregator.Clients.ProviderOne.Models;
using SystemAggregator.Clients.ProviderTwo.Models;

namespace Aggregator.UseCases.Mapping
{
    public static class SearchMapping
    {
        public static ProviderOneSearchRequest ToProviderOneFormat(this SearchRequest request)
        {
            return new ProviderOneSearchRequest
            {
                From = request.Origin,
                To = request.Destination,
                DateFrom = request.OriginDateTime,
                DateTo = request.Filters?.DestinationDateTime,
                MaxPrice = request.Filters?.MaxPrice
            };
        }

        public static ProviderTwoSearchRequest ToProviderTwoFormat(this SearchRequest request)
        {
            return new ProviderTwoSearchRequest
            {
                Departure = request.Origin,
                Arrival = request.Destination,
                DepartureDate = request.OriginDateTime,
                MinTimeLimit = request.Filters?.MinTimeLimit
            };
        }

        public static Route ToStandardFormat(this ProviderOneRoute route)
        {
            return new Route
            {
                Id = route.Id,
                Origin = route.From,
                Destination = route.To,
                OriginDateTime = route.DateFrom,
                DestinationDateTime = route.DateTo,
                Price = route.Price,
                TimeLimit = route.TimeLimit
            };
        }

        public static Route ToStandardFormat(this ProviderTwoRoute route)
        {
            return new Route
            {
                Id = route.Id,
                Origin = route.Departure.Point,
                Destination = route.Arrival.Point,
                OriginDateTime = route.Departure.Date,
                DestinationDateTime = route.Arrival.Date,
                Price = route.Price,
                TimeLimit = route.TimeLimit
            };
        }
    }
}
