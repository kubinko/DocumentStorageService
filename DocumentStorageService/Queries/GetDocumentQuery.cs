using DocumentStorageService.Entities;
using MediatR;

namespace DocumentStorageService.Queries
{
    /// <summary>
    /// Query for retrieving document.
    /// </summary>
    public class GetDocumentQuery : IRequest<Document?>
    {
        /// <summary>
        /// Unique document identifier.
        /// </summary>
        public string Id { get; }

        /// <summary>
        /// Ctor.
        /// </summary>
        /// <param name="id">Unique document identifier.</param>
        public GetDocumentQuery(string id)
        {
            Id = id;
        }
    }
}
