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
        [ProducesResponseType(StatusCodes.Status201Created)]
        public async Task<IActionResult> AddDocument(AddDocumentCommand payload, CancellationToken cancellationToken)
        {
            await _mediator.Send(payload, cancellationToken);
            return Created(nameof(GetDocument), new { payload.Id });
        }

        /// <summary>
        /// Modifies document in storage.
        /// </summary>
        /// <param name="payload">Document payload.</param>
        [HttpPut]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
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
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Document))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetDocument(string id, CancellationToken cancellationToken)
        {
            Document? document = await _mediator.Send(new GetDocumentQuery(id), cancellationToken);
            return document == null ? NotFound() : Ok(document);
        }
    }
}
