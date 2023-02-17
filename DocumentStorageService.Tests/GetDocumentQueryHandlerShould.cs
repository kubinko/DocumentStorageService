using DocumentStorageService.Commands;
using DocumentStorageService.Queries;
using DocumentStorageService.Services;

namespace DocumentStorageService.Tests
{
    public class GetDocumentQueryHandlerShould
    {
        [Fact]
        public async Task AttemptToGetDocumentSpecifiedByQuery()
        {
            var query = new GetDocumentQuery("get-document-id");
            var storageService = Substitute.For<IStorageService>();
            var handler = new GetDocumentQueryHandler(storageService);
            var cancellationToken = new CancellationToken();

            await handler.Handle(query, cancellationToken);

            await storageService.Received(1).GetDocument(Arg.Is(query.Id), Arg.Is(cancellationToken));
        }
    }
}
