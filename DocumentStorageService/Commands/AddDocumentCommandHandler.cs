using DocumentStorageService.Entities;
using DocumentStorageService.Services;
using Mapster;
using MediatR;

namespace DocumentStorageService.Commands
{
    /// <summary>
    /// Handler for <see cref="AddDocumentCommand"/>.
    /// </summary>
    public class AddDocumentCommandHandler : IRequestHandler<AddDocumentCommand>
    {
        private readonly IStorageService _storageService;

        /// <summary>
        /// Ctor.
        /// </summary>
        /// <param name="storageService">Service for storing documents.</param>
        public AddDocumentCommandHandler(IStorageService storageService)
        {
            _storageService = storageService;
        }

        /// <inheritdoc/>
        public Task Handle(AddDocumentCommand request, CancellationToken cancellationToken)
            => _storageService.AddDocument(request.Adapt<Document>(), cancellationToken);
    }
}
