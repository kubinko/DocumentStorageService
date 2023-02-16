using MediatR;

namespace DocumentStorageService.Commands
{
    /// <summary>
    /// Command for adding document.
    /// </summary>
    public class AddDocumentCommand : IRequest
    {
        /// <summary>
        /// Unique document identifier.
        /// </summary>
        public string Id { get; set; } = "";

        /// <summary>
        /// Collection of document tags.
        /// </summary>
        public List<string> Tags { get; set; } = new List<string>();

        /// <summary>
        /// Document data.
        /// </summary>
        public object? Data { get; set; }
    }
}
