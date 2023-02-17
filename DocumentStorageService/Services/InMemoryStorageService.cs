using DocumentStorageService.Entities;

namespace DocumentStorageService.Services
{
    /// <summary>
    /// Service for storing documents in memory.
    /// </summary>
    public class InMemoryStorageService : IStorageService
    {
        private readonly Dictionary<string, Document> _documents = new();
        private readonly ICacheService _cache;

        /// <summary>
        /// Ctor.
        /// </summary>
        /// <param name="cache">Document cache.</param>
        public InMemoryStorageService(ICacheService cache)
        {
            _cache = cache ?? throw new ArgumentOutOfRangeException(nameof(cache));
        }

        /// <inheritdoc/>
        public Task AddDocument(Document document, CancellationToken cancellationToken = default)
        {
            if (!_documents.ContainsKey(document.Id))
            {
                _documents.Add(document.Id, document);
            }

            return Task.CompletedTask;
        }

        /// <inheritdoc/>
        public Task ModifyDocument(Document document, CancellationToken cancellationToken = default)
        {
            if (_documents.ContainsKey(document.Id))
            {
                _documents[document.Id] = document;
                _cache.RemoveFromCache(document.Id);
            }

            return Task.CompletedTask;
        }

        /// <inheritdoc/>
        public Task<Document?> GetDocument(string id, CancellationToken cancellationToken = default)
        {
            Document? document = _cache.TryGetFromCache(id);
            if (document == null && _documents.TryGetValue(id, out document))
            {
                _cache.StoreToCache(document);
            }

            return Task.FromResult(document);
        }
    }
}
