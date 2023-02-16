using DocumentStorageService.Commands;
using DocumentStorageService.Entities;
using DocumentStorageService.Queries;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net.Mime;

namespace DocumentStorageService.Controllers
{
    [Route("[controller]")]
    [ApiController]
    [AllowAnonymous]
    public class DocumentsController : ControllerBase
    {
        private readonly IMediator _mediator;

        /// <summary>
        /// Ctor.
        /// </summary>
        /// <param name="mediator">MediatR.</param>
        public DocumentsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        /// <summary>
        /// Adds new document to storage.
        /// </summary>
        /// <param name="payload">Document payload.</param>
        [HttpPost]
        public async Task<IActionResult> AddDocument(AddDocumentCommand payload, CancellationToken cancellationToken)
        {
            await _mediator.Send(payload, cancellationToken);
            return Created(nameof(GetDocument), new { payload.Id });
        }

        /// <summary>
        /// Modifies document to storage.
        /// </summary>
        /// <param name="payload">Document payload.</param>
        [HttpPut]
        public async Task<IActionResult> ModifyDocument(ModifyDocumentCommand payload, CancellationToken cancellationToken)
        {
            await _mediator.Send(payload, cancellationToken);
            return NoContent();
        }


        /// <summary>
        /// Attempts to retrieve document from storage.
        /// </summary>
        /// <param name="id">Document ID.</param>
        [HttpGet("{id}")]
        [Produces(MediaTypeNames.Application.Json, MediaTypeNames.Application.Xml, "application/x-msgpack")]
        public async Task<IActionResult> GetDocument(string id, CancellationToken cancellationToken)
        {
            Document? document = await _mediator.Send(new GetDocumentQuery(id), cancellationToken);
            return document == null ? NotFound() : Ok(document);
        }
    }
}
