using DocumentStorageService.Entities;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Options;

namespace DocumentStorageService.Services
{
    /// <summary>
    /// Cache service for documents.
    /// </summary>
    public class CacheService : ICacheService
    {
        private readonly IMemoryCache _cache;
        private readonly MemoryCacheEntryOptions _cachingOptions;

        /// <summary>
        /// Ctor.
        /// </summary>
        /// <param name="cache">In-memory cache.</param>
        /// <param name="cachingOptions">Caching options/</param>
        public CacheService(IMemoryCache cache, IOptions<MemoryCacheEntryOptions> cachingOptions)
        {
            _cache = cache ?? throw new ArgumentNullException(nameof(cache));
            _cachingOptions = cachingOptions?.Value ?? throw new ArgumentNullException(nameof(cachingOptions));
        }

        /// <inheritdoc/>
        public void StoreToCache(Document document)
            => _cache.Set(document.Id, document, _cachingOptions);

        /// <inheritdoc/>
        public void RemoveFromCache(string documentId)
            => _cache.Remove(documentId);

        /// <inheritdoc/>
        public Document? TryGetFromCache(string documentId)
            => _cache.Get<Document>(documentId);
    }
}
