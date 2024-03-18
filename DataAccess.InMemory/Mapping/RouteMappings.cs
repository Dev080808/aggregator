using Aggregator.Entities.Models;

namespace Aggregator.DataAccess.InMemory.Mapping
{
    public static class RouteMappings
    {
        public static Route ToDomainModel(this Entities.Route entity)
        {
            return new Route()
            {
                Origin = entity.Origin,
                Destination = entity.Destination,
                OriginDateTime = entity.OriginDateTime,
                DestinationDateTime = entity.DestinationDateTime,
                Price = entity.Price,
                TimeLimit = entity.TimeLimit
            };
        }
    }
}
