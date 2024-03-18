using Caching.Interfaces;

using Microsoft.Extensions.Caching.Memory;

namespace Aggregator.Caching.InMemory
{
    public class CustomMemoryCache : ICustomMemoryCache
    {
        public IMemoryCache MemoryCache { get; private set; }

        public CustomMemoryCache()
        {
            MemoryCache = new MemoryCache(new MemoryCacheOptions
            {
                SizeLimit = 1024 * 1024 * 100
            });
        }
    }
}
