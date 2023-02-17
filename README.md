# Document Storage Service
Sample ASP.NET service for storing documents and their retrieval in various output formats.

Service uses CQRS pattern implemented by [MediatR](https://github.com/jbogard/MediatR).

## Endpoints
### Add new document
``` http
POST /documents
```

**Payload:** JSON with properties `id` (required, string), `tags` (array of strings) and `data` (dictionary)

**Possible status codes:** ` 201 CREATED `

### Modify document
``` http
PUT /documents
```

**Payload:** JSON with properties `id` (required, string), `tags` (array of strings) and `data` (dictionary)

**Possible status codes:** ` 204 NO CONTENT `

### Retrieve document
``` http
GET /documents/{documentId}
```

**Possible status codes:** ` 200 OK `, ` 404 NOT FOUND `

## Output Formatting
Documents can be retrieved in various formats. So far these formats are supported:
- JSON (application/json)
- XML (application/xml)
- MessagePack (application/x-msgpack)

Output format is based on `Accept` header. Output is written by `OutputFormatter`. To support another format, just add new child of `OutputFormatter` in `AddControllers()` method during service startup and add new MIME type to `Produces` attribute of GET endpoint.

## Storage
Documents are stored in in-memory storage. To switch to another type of storage, just implement interface `IStorageService` for required type of storage and switch `IStorageService` implementation type in DI initialization.

In order to demonstrate a way to improve performance and speed up document retrieval, service contains simple in-memory cache, that store recently requested documents. 

*Note: It does not have any real use when using in-memory storage service, but it would be beneficial when using another type of storage.*

## Unit tests
**`DocumentStorageService.Tests`** project contains simple unit tests.
