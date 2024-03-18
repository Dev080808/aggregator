using Microsoft.Extensions.Caching.Memory;

namespace Caching.Interfaces
{
    public interface ICustomMemoryCache
    {
        IMemoryCache MemoryCache { get; }
    }
}
