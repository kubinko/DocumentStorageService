using MessagePack;

namespace DocumentStorageService.Entities
{
    /// <summary>
    /// Document.
    /// </summary>
    [MessagePackObject(keyAsPropertyName: true)]
    public class Document
    {
        /// <summary>
        /// Unique identifier.
        /// </summary>
        public string Id { get; set; } = "";

        /// <summary>
        /// Collection of document tags.
        /// </summary>
        public List<string> Tags { get; set; } = new List<string>();

        /// <summary>
        /// Document data.
        /// </summary>        
        public Dictionary<string, string>? Data { get; set; }
    }
}
