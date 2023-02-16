using DocumentStorageService.Entities;
using DocumentStorageService.Queries;
using DocumentStorageService.Services;
using MediatR;

namespace DocumentStorageService.Commands
{
    /// <summary>
    /// Handler for <see cref="GetDocumentQuery"/>.
    /// </summary>
    public class GetDocumentQueryHandler : IRequestHandler<GetDocumentQuery, Document?>
    {
        private readonly IStorageService _storageService;

        /// <summary>
        /// Ctor.
        /// </summary>
        /// <param name="storageService">Service for storing documents.</param>
        public GetDocumentQueryHandler(IStorageService storageService)
        {
            _storageService = storageService ?? throw new ArgumentNullException(nameof(storageService));
        }

        /// <inheritdoc/>
        public Task<Document?> Handle(GetDocumentQuery request, CancellationToken cancellationToken)
            => _storageService.GetDocument(request.Id, cancellationToken);
    }
}
