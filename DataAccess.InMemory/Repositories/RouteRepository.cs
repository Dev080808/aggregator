using Microsoft.EntityFrameworkCore;

using Aggregator.DataAccess.InMemory.Mapping;
using Aggregator.DataAccess.Interfaces;
using Aggregator.DataAccess.Interfaces.Models;
using Aggregator.Entities.Models;

namespace Aggregator.DataAccess.InMemory.Repositories
{
    public class RouteRepository : IRouteRepository
    {
        private readonly ProviderOneDbContext _context;

        public RouteRepository(ProviderOneDbContext context)
        {
            _context = context;
        }

        public async Task<List<Route>> Search(RouteSearchFilter filter)
        {
            var query = GetQuery(filter);

            var entities = await query.ToListAsync();

            var items = entities.Select(RouteMappings.ToDomainModel).ToList();

            return items;
        }

        private IQueryable<Entities.Route> GetQuery(RouteSearchFilter filter)
        {
            var query = _context.Routes.AsQueryable();

            query = query.Where(x => x.OriginDateTime.Date == filter.OriginDate.Date);

            if (filter.DestinationDate.HasValue)
            {
                query = query.Where(x => x.DestinationDateTime.Date == filter.DestinationDate.Value.Date);
            }

            if (filter.MaxPrice.HasValue)
            {
                query = query.Where(x => x.Price <= filter.MaxPrice.Value);
            }

            if (!string.IsNullOrEmpty(filter.Origin))
            {
                query = query.Where(x => x.Origin.Contains(filter.Origin));
            }

            if (!string.IsNullOrEmpty(filter.Destination))
            {
                query = query.Where(x => x.Destination.Contains(filter.Destination));
            }

            return query;
        }
    }
}
