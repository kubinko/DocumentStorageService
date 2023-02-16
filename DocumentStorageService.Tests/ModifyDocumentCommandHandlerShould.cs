using DocumentStorageService.Commands;
using DocumentStorageService.Entities;
using DocumentStorageService.Services;

namespace DocumentStorageService.Tests
{
    public class ModifyDocumentCommandHandlerShould
    {
        [Fact]
        public async Task ModifyDocumentInStorageWithCorrectValues()
        {
            var command = new ModifyDocumentCommand()
            {
                Id = "modify-document-id",
                Tags = new List<string>(new[] { "TagA", "TagB", "TagC" }),
                Data = new Dictionary<string, string>() { ["SomeData"] = new Random(Environment.TickCount).NextDouble().ToString() }
            };
            var storageService = Substitute.For<IStorageService>();
            var handler = new ModifyDocumentCommandHandler(storageService);
            var cancellationToken = new CancellationToken();

            await handler.Handle(command, cancellationToken);

            await storageService.Received(1).ModifyDocument(
                Arg.Is<Document>(d =>
                    d.Id == command.Id &&
                    d.Tags.SequenceEqual(command.Tags) &&
                    d.Data!.SequenceEqual(command.Data)),
                Arg.Is(cancellationToken));
        }
    }
}
