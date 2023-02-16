using DocumentStorageService.Entities;

namespace DocumentStorageService.Services
{
    /// <summary>
    /// Interface describing service for storing documents.
    /// </summary>
    public interface IStorageService
    {
        /// <summary>
        /// Adds new document.
        /// </summary>
        /// <param name="document">Document.</param>
        /// <param name="cancellationToken">Cancellation token.</param>
        Task AddDocument(Document document, CancellationToken cancellationToken = default);

        /// <summary>
        /// Modifies existing document.
        /// </summary>
        /// <param name="document">Document to be modified.</param>
        /// <param name="cancellationToken">Cancellation token.</param>
        Task ModifyDocument(Document document, CancellationToken cancellationToken = default);

        /// <summary>
        /// Retrieves document with <paramref name="id"/>.
        /// </summary>
        /// <param name="id">Document ID.</param>
        /// <param name="cancellationToken">Cancellation token.</param>
        /// <returns>Document; <c>null</c>, if document with specified ID could not be found.</returns>
        Task<Document?> GetDocument(string id, CancellationToken cancellationToken = default);
    }
}
