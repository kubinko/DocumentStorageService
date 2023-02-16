namespace DocumentStorageService.Entities
{
    /// <summary>
    /// Document.
    /// </summary>
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
        public object? Data { get; set; }
    }
}
