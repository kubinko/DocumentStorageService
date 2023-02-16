using FluentValidation;

namespace DocumentStorageService.Commands
{
    /// <summary>
    /// Validator for <see cref="ModifyDocumentCommand"/>.
    /// </summary>
    public class ModifyDocumentCommandValidator : AbstractValidator<ModifyDocumentCommand>
    {
        /// <summary>
        /// Ctor.
        /// </summary>
        public ModifyDocumentCommandValidator()
        {
            RuleFor(x => x.Id).NotEmpty();
        }
    }
}
