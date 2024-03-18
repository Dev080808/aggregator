using Aggregator.DataAccess.Interfaces.Models;
using Aggregator.Entities.Models;

namespace Aggregator.DataAccess.Interfaces
{
    public interface IRouteRepository
    {
        Task<List<Route>> Search(RouteSearchFilter filter);
    }
}
