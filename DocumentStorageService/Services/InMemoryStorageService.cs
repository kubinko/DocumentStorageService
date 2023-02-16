using DocumentStorageService.Entities;

namespace DocumentStorageService.Services
{
    /// <summary>
    /// Service for storing documents.
    /// </summary>
    public class InMemoryStorageService : IStorageService
    {
        private readonly Dictionary<string, Document> _documents = new();

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
            }

            return Task.CompletedTask;
        }

        /// <inheritdoc/>
        public Task<Document?> GetDocument(string id, CancellationToken cancellationToken = default)
            => Task.FromResult(_documents.TryGetValue(id, out Document? document) ? document : null);
    }
}
