using DocumentStorageService.Commands;
using FluentValidation.Results;

namespace DocumentStorageService.Tests
{
    public class AddDocumentCommandValidatorShould
    {
        [Theory]
        [InlineData("")]
        [InlineData(null)]
        public void MarkCommandWithoutDocumentIdAsInvalid(string id)
        {
            var command = new AddDocumentCommand() { Id = id };
            var validator = new AddDocumentCommandValidator();

            ValidationResult result = validator.Validate(command);
            result.IsValid.Should().BeFalse();
            result.Errors.Should().Contain(err => err.PropertyName == nameof(command.Id));
        }

        [Fact]
        public void MarkCorrectCommandAsValid()
        {
            var command = new AddDocumentCommand() { Id = "valid-id" };
            var validator = new AddDocumentCommandValidator();

            ValidationResult result = validator.Validate(command);
            result.IsValid.Should().BeTrue();
            result.Errors.Should().BeEmpty();
        }
    }
}
