using DocumentStorageService.Entities;
using DocumentStorageService.Services;
using Mapster;
using MediatR;

namespace DocumentStorageService.Commands
{
    /// <summary>
    /// Handler for <see cref="ModifyDocumentCommand"/>.
    /// </summary>
    public class ModifyDocumentCommandHandler : IRequestHandler<ModifyDocumentCommand>
    {
        private readonly IStorageService _storageService;

        /// <summary>
        /// Ctor.
        /// </summary>
        /// <param name="storageService">Service for storing documents.</param>
        public ModifyDocumentCommandHandler(IStorageService storageService)
        {
            _storageService = storageService;
        }

        /// <inheritdoc/>
        public Task Handle(ModifyDocumentCommand request, CancellationToken cancellationToken)
            => _storageService.ModifyDocument(request.Adapt<Document>(), cancellationToken);
    }
}
