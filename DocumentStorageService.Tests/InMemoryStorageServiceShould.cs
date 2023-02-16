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
            var storageService = new InMemoryStorageService();
            Document document = CreateDocument(DocumentId, new[] { "Tag1", "Tag3", "Tag42" }, CreateDocumentData1());

            await storageService.AddDocument(document);

            AssertDocument(await storageService.GetDocument(DocumentId), document);
        }

        [Fact]
        public async Task ProperlyModifyExistingDocument()
        {
            var storageService = new InMemoryStorageService();
            Document document = CreateDocument(DocumentId, new[] { "Tag1", "Tag3", "Tag42" }, CreateDocumentData1());
            Document modifiedDocument = CreateDocument(DocumentId, new[] { "AnotherTag" }, CreateDocumentData2());

            await storageService.AddDocument(document);
            await storageService.ModifyDocument(modifiedDocument);

            AssertDocument(await storageService.GetDocument(DocumentId), modifiedDocument);
        }

        [Fact]
        public async Task NotOverrideExistingDocumentDuringAddOperation()
        {
            var storageService = new InMemoryStorageService();
            Document document = CreateDocument(DocumentId, new[] { "Tag1", "Tag3", "Tag42" }, CreateDocumentData1());
            Document duplicateDocument = CreateDocument(DocumentId, new[] { "AnotherTag" }, CreateDocumentData2());

            await storageService.AddDocument(document);
            await storageService.AddDocument(duplicateDocument);

            AssertDocument(await storageService.GetDocument(DocumentId), document);
        }

        [Fact]
        public async Task NotThrowExceptionWhenAttemptingToModifyNonExistingDocument()
        {
            var storageService = new InMemoryStorageService();
            Document document = CreateDocument(DocumentId, new[] { "Tag1", "Tag3", "Tag42" }, CreateDocumentData1());

            var action = async () => await storageService.ModifyDocument(document);

            await action.Should().NotThrowAsync<Exception>();
            (await storageService.GetDocument(DocumentId)).Should().BeNull();
        }

        [Fact]
        public async Task ReturnNullIfSpecifiedDocumentWasNotFound()
        {
            var storageService = new InMemoryStorageService();

            Document? result = await storageService.GetDocument(DocumentId);

            result.Should().BeNull();
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