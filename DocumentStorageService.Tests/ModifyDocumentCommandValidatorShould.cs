using DocumentStorageService.Commands;
using FluentValidation.Results;

namespace DocumentStorageService.Tests
{
    public class ModifyDocumentCommandValidatorShould
    {
        [Theory]
        [InlineData("")]
        [InlineData(null)]
        public void MarkCommandWithoutDocumentIdAsInvalid(string id)
        {
            var command = new ModifyDocumentCommand() { Id = id };
            var validator = new ModifyDocumentCommandValidator();

            ValidationResult result = validator.Validate(command);
            result.IsValid.Should().BeFalse();
            result.Errors.Should().Contain(err => err.PropertyName == nameof(command.Id));
        }

        [Fact]
        public void MarkCorrectCommandAsValid()
        {
            var command = new ModifyDocumentCommand() { Id = "valid-id" };
            var validator = new ModifyDocumentCommandValidator();

            ValidationResult result = validator.Validate(command);
            result.IsValid.Should().BeTrue();
            result.Errors.Should().BeEmpty();
        }
    }
}
