using Application.Use_Cases.Authentication.Commands;
using FluentValidation.TestHelper;

namespace RealEstateManagement.Application.UnitTests
{
    public class RegisterUserCommandValidatorTests
    {
        private readonly RegisterUserCommandValidator _validator;

        public RegisterUserCommandValidatorTests()
        {
            _validator = new RegisterUserCommandValidator();
        }

        [Fact]
        public void Should_Have_Error_When_Name_Is_Empty()
        {
            var command = new RegisterUserCommand { Name = string.Empty, Email = "test@example.com", PhoneNumber = "1234567890", Password = "password123" };
            var result = _validator.TestValidate(command);
            result.ShouldHaveValidationErrorFor(x => x.Name);
        }

        [Fact]
        public void Should_Have_Error_When_Name_Exceeds_Maximum_Length()
        {
            var command = new RegisterUserCommand { Name = new string('A', 51), Email = "test@example.com", PhoneNumber = "1234567890", Password = "password123" };
            var result = _validator.TestValidate(command);
            result.ShouldHaveValidationErrorFor(x => x.Name);
        }

        [Fact]
        public void Should_Have_Error_When_Email_Is_Empty()
        {
            var command = new RegisterUserCommand { Name = "Test User", Email = string.Empty, PhoneNumber = "1234567890", Password = "password123" };
            var result = _validator.TestValidate(command);
            result.ShouldHaveValidationErrorFor(x => x.Email);
        }

        [Fact]
        public void Should_Have_Error_When_Email_Is_Invalid()
        {
            var command = new RegisterUserCommand { Name = "Test User", Email = "invalid-email", PhoneNumber = "1234567890", Password = "password123" };
            var result = _validator.TestValidate(command);
            result.ShouldHaveValidationErrorFor(x => x.Email);
        }

        [Fact]
        public void Should_Have_Error_When_PhoneNumber_Is_Empty()
        {
            var command = new RegisterUserCommand { Name = "Test User", Email = "test@example.com", PhoneNumber = string.Empty, Password = "password123" };
            var result = _validator.TestValidate(command);
            result.ShouldHaveValidationErrorFor(x => x.PhoneNumber);
        }

        [Fact]
        public void Should_Have_Error_When_PhoneNumber_Exceeds_Maximum_Length()
        {
            var command = new RegisterUserCommand { Name = "Test User", Email = "test@example.com", PhoneNumber = new string('1', 16), Password = "password123" };
            var result = _validator.TestValidate(command);
            result.ShouldHaveValidationErrorFor(x => x.PhoneNumber);
        }

        [Fact]
        public void Should_Have_Error_When_Password_Is_Empty()
        {
            var command = new RegisterUserCommand { Name = "Test User", Email = "test@example.com", PhoneNumber = "1234567890", Password = string.Empty };
            var result = _validator.TestValidate(command);
            result.ShouldHaveValidationErrorFor(x => x.Password);
        }

        [Fact]
        public void Should_Have_Error_When_Password_Exceeds_Maximum_Length()
        {
            var command = new RegisterUserCommand { Name = "Test User", Email = "test@example.com", PhoneNumber = "1234567890", Password = new string('P', 51) };
            var result = _validator.TestValidate(command);
            result.ShouldHaveValidationErrorFor(x => x.Password);
        }

        [Fact]
        public void Should_Not_Have_Error_When_Command_Is_Valid()
        {
            var command = new RegisterUserCommand
            {
                Name = "Test User",
                Email = "test@example.com",
                PhoneNumber = "1234567890",
                Password = "password123"
            };
            var result = _validator.TestValidate(command);
            result.ShouldNotHaveAnyValidationErrors();
        }
    }
}
