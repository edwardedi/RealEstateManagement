using Application.Use_Cases.Transactions.Commands;
using FluentValidation.TestHelper;

namespace RealEstateManagement.Application.UnitTests
{
    public class CreateTransactionCommandValidatorTests
    {
        private readonly CreateTransactionCommandValidator _validator;

        public CreateTransactionCommandValidatorTests()
        {
            _validator = new CreateTransactionCommandValidator();
        }

        [Fact]
        public void Should_Have_Error_When_PropertyId_Is_Empty()
        {
            var command = new CreateTransactionCommand { PropertyId = Guid.Empty };
            var result = _validator.TestValidate(command);
            result.ShouldHaveValidationErrorFor(x => x.PropertyId);
        }

        [Fact]
        public void Should_Have_Error_When_BuyerId_Is_Empty()
        {
            var command = new CreateTransactionCommand { BuyerId = Guid.Empty };
            var result = _validator.TestValidate(command);
            result.ShouldHaveValidationErrorFor(x => x.BuyerId);
        }

        [Fact]
        public void Should_Have_Error_When_SellerId_Is_Empty()
        {
            var command = new CreateTransactionCommand { SellerId = Guid.Empty };
            var result = _validator.TestValidate(command);
            result.ShouldHaveValidationErrorFor(x => x.SellerId);
        }

        [Fact]
        public void Should_Have_Error_When_SalePrice_Is_Less_Than_Or_Equal_To_Zero()
        {
            var command = new CreateTransactionCommand { SalePrice = 0 };
            var result = _validator.TestValidate(command);
            result.ShouldHaveValidationErrorFor(x => x.SalePrice);
        }

        [Fact]
        public void Should_Not_Have_Error_When_Command_Is_Valid()
        {
            var command = new CreateTransactionCommand
            {
                PropertyId = Guid.NewGuid(),
                BuyerId = Guid.NewGuid(),
                SellerId = Guid.NewGuid(),
                SalePrice = 250000
            };
            var result = _validator.TestValidate(command);
            result.ShouldNotHaveAnyValidationErrors();
        }
    }
}

