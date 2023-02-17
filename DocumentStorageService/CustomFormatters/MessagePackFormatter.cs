using DocumentStorageService.Entities;
using MessagePack;
using Microsoft.AspNetCore.Mvc.Formatters;

namespace DocumentStorageService.CustomFormatters
{
    /// <summary>
    /// Custom output formatter for MessagePack format.
    /// </summary>
    public class MessagePackFormatter : OutputFormatter
    {
        private const string MessagePackMimeType = "application/x-msgpack";

        /// <summary>
        /// Ctor.
        /// </summary>
        public MessagePackFormatter()
        {
            SupportedMediaTypes.Add(MessagePackMimeType);
        }

        /// <inheritdoc/>
        protected override bool CanWriteType(Type? type)
        {
            return type == typeof(Document);
        }

        /// <inheritdoc/>
        public override async Task WriteResponseBodyAsync(OutputFormatterWriteContext context)
        {
            using var ms = new MemoryStream();

            await MessagePackSerializer.SerializeAsync(context.ObjectType, ms, context.Object);
            await context.HttpContext.Response.BodyWriter.WriteAsync(ms.ToArray());
        }
    }
}
