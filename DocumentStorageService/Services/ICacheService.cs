using DocumentStorageService.Entities;

namespace DocumentStorageService.Services
{
    /// <summary>
    /// Interface describing cache service.
    /// </summary>
    public interface ICacheService
    {
        /// <summary>
        /// Stores document into cache.
        /// </summary>
        /// <param name="document">Document.</param>
        void StoreToCache(Document document);

        /// <summary>
        /// Removes document with <paramref name="documentId"/> from cache.
        /// </summary>
        /// <param name="documentId">Document ID.</param>
        void RemoveFromCache(string documentId);

        /// <summary>
        /// Attempts to retrieve document from cache.
        /// </summary>
        /// <param name="documentId">Document ID.</param>
        /// <returns>Document; <c>null</c>, if document was not found.</returns>
        Document? TryGetFromCache(string documentId);
    }
}
