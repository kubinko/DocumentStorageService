using DocumentStorageService.Commands;
using DocumentStorageService.Entities;
using DocumentStorageService.Services;

namespace DocumentStorageService.Tests
{
    public class AddDocumentCommandHandlerShould
    {
        [Fact]
        public async Task AddNewDocumentToStorageWithCorrectValues()
        {
            var command = new AddDocumentCommand()
            {
                Id = "add-document-id",
                Tags = new List<string>(new[] { "TagX", "TagY", "TagZ" }),
                Data = new Random(Environment.TickCount).NextDouble()
            };
            var storageService = Substitute.For<IStorageService>();
            var handler = new AddDocumentCommandHandler(storageService);
            var cancellationToken = new CancellationToken();

            await handler.Handle(command, cancellationToken);

            await storageService.Received(1).AddDocument(
                Arg.Is<Document>(d => d.Id == command.Id && d.Tags.SequenceEqual(command.Tags) && d.Data == command.Data),
                Arg.Is(cancellationToken));
        }
    }
}
