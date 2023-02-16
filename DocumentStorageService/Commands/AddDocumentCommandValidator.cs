using FluentValidation;

namespace DocumentStorageService.Commands
{
    /// <summary>
    /// Validator for <see cref="AddDocumentCommand"/>.
    /// </summary>
    public class AddDocumentCommandValidator : AbstractValidator<AddDocumentCommand>
    {
        /// <summary>
        /// Ctor.
        /// </summary>
        public AddDocumentCommandValidator()
        {
            RuleFor(x => x.Id).NotEmpty();
        }
    }
}
