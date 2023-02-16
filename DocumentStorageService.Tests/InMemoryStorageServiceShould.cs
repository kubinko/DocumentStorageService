using DocumentStorageService.Entities;
using DocumentStorageService.Services;

namespace DocumentStorageService.Tests
{
    public class InMemoryStorageServiceShould
    {
        private const string DocumentId = "some-document-id";

        [Fact]
        public async Task ProperlyAddNewDocument()
        {
            var storageService = CreateService(out _);
            Document document = CreateDocument(DocumentId, new[] { "Tag1", "Tag3", "Tag42" }, CreateDocumentData1());

            await storageService.AddDocument(document);

            AssertDocument(await storageService.GetDocument(DocumentId), document);
        }

        [Fact]
        public async Task ProperlyModifyExistingDocument()
        {
            var storageService = CreateService(out ICacheService cache);
            Document document = CreateDocument(DocumentId, new[] { "Tag1", "Tag3", "Tag42" }, CreateDocumentData1());
            Document modifiedDocument = CreateDocument(DocumentId, new[] { "AnotherTag" }, CreateDocumentData2());

            await storageService.AddDocument(document);
            await storageService.ModifyDocument(modifiedDocument);

            AssertDocument(await storageService.GetDocument(DocumentId), modifiedDocument);
            cache.Received(1).RemoveFromCache(Arg.Is(DocumentId));
        }

        [Fact]
        public async Task NotOverrideExistingDocumentDuringAddOperation()
        {
            var storageService = CreateService(out _);
            Document document = CreateDocument(DocumentId, new[] { "Tag1", "Tag3", "Tag42" }, CreateDocumentData1());
            Document duplicateDocument = CreateDocument(DocumentId, new[] { "AnotherTag" }, CreateDocumentData2());

            await storageService.AddDocument(document);
            await storageService.AddDocument(duplicateDocument);

            AssertDocument(await storageService.GetDocument(DocumentId), document);
        }

        [Fact]
        public async Task NotThrowExceptionWhenAttemptingToModifyNonExistingDocument()
        {
            var storageService = CreateService(out ICacheService cache);
            Document document = CreateDocument(DocumentId, new[] { "Tag1", "Tag3", "Tag42" }, CreateDocumentData1());

            var action = async () => await storageService.ModifyDocument(document);

            await action.Should().NotThrowAsync<Exception>();
            (await storageService.GetDocument(DocumentId)).Should().BeNull();
            cache.DidNotReceive().RemoveFromCache(Arg.Is(DocumentId));
        }

        [Fact]
        public async Task ReturnNullIfSpecifiedDocumentWasNotFound()
        {
            var storageService = CreateService(out ICacheService cache);

            Document? result = await storageService.GetDocument(DocumentId);

            result.Should().BeNull();
            cache.Received(1).TryGetFromCache(Arg.Is(DocumentId));
        }

        [Fact]
        public async Task CacheRetrievedDocumentIfNotAlreadyInCache()
        {
            var storageService = CreateService(out ICacheService cache);
            Document document = CreateDocument(DocumentId, new[] { "Tag1", "Tag3", "Tag42" }, CreateDocumentData1());

            await storageService.AddDocument(document);
            await storageService.GetDocument(DocumentId);

            cache.Received(1).StoreToCache(Arg.Is<Document>(d =>
                d.Id == document.Id &&
                d.Tags.SequenceEqual(document.Tags) &&
                d.Data!.SequenceEqual(document.Data!)));
        }

        [Fact]
        public async Task PreferCacheOverStorage()
        {
            Document cachedDocument = CreateDocument(DocumentId, new[] { "TagX", "TagY", "TagZ" }, CreateDocumentData2());
            var storageService = CreateService(out ICacheService cache, cachedDocument);
            Document document = CreateDocument(DocumentId, new[] { "Tag1", "Tag3", "Tag42" }, CreateDocumentData1());

            await storageService.AddDocument(document);
            Document? result = await storageService.GetDocument(DocumentId);

            cache.Received(1).TryGetFromCache(Arg.Is(DocumentId));
            AssertDocument(result, cachedDocument);
        }

        private static InMemoryStorageService CreateService(out ICacheService cache, Document? cachedDocument = null)
        {
            cache = Substitute.For<ICacheService>();
            if (cachedDocument != null)
            {
                cache.TryGetFromCache(Arg.Is(cachedDocument.Id)).Returns(cachedDocument);
            }

            return new InMemoryStorageService(cache);
        }

        private static Document CreateDocument(
            string documentId,
            IEnumerable<string> tags,
            Dictionary<string, string>? data = null)
            => new()
            {
                Id = documentId,
                Tags = new List<string>(tags),
                Data = data
            };

        private static Dictionary<string, string> CreateDocumentData1()
            => new()
            {
                ["PI"] = Math.PI.ToString()
            };

        private static Dictionary<string, string> CreateDocumentData2()
            => new()
            {
                ["Date"] = new DateTime(2023, 1, 6).ToString()
            };

        private static void AssertDocument(Document? resultDocument, Document expectedDocument)
        {
            resultDocument.Should().NotBeNull();
            resultDocument!.Id.Should().Be(expectedDocument.Id);
            resultDocument!.Tags.Should().BeEquivalentTo(expectedDocument.Tags);
            resultDocument!.Data.Should().BeEquivalentTo(expectedDocument.Data);
        }
    }
}