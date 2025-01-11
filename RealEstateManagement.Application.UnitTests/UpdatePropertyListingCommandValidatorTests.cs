using Application.Use_Cases.Commands;
using FluentValidation.TestHelper;

namespace RealEstateManagement.Application.UnitTests
{
    public class UpdatePropertyListingCommandValidatorTests
    {
        private readonly UpdatePropertyListingCommandValidator _validator;

        public UpdatePropertyListingCommandValidatorTests()
        {
            _validator = new UpdatePropertyListingCommandValidator();
        }

        [Fact]
        public void Should_Have_Error_When_PropertyId_Is_Empty()
        {
            var command = new UpdatePropertyListingCommand { PropertyId = Guid.Empty };
            var result = _validator.TestValidate(command);
            result.ShouldHaveValidationErrorFor(x => x.PropertyId);
        }

        [Fact]
        public void Should_Have_Error_When_Title_Exceeds_Maximum_Length()
        {
            var command = new UpdatePropertyListingCommand { Title = new string('T', 101) };
            var result = _validator.TestValidate(command);
            result.ShouldHaveValidationErrorFor(x => x.Title);
        }

        [Fact]
        public void Should_Have_Error_When_Address_Exceeds_Maximum_Length()
        {
            var command = new UpdatePropertyListingCommand { Address = new string('A', 201) };
            var result = _validator.TestValidate(command);
            result.ShouldHaveValidationErrorFor(x => x.Address);
        }

        [Fact]
        public void Should_Have_Error_When_Price_Is_Less_Than_Or_Equal_To_Zero()
        {
            var command = new UpdatePropertyListingCommand { Price = 0 };
            var result = _validator.TestValidate(command);
            result.ShouldHaveValidationErrorFor(x => x.Price);
        }

        [Fact]
        public void Should_Have_Error_When_SquareFootage_Is_Less_Than_Or_Equal_To_Zero()
        {
            var command = new UpdatePropertyListingCommand { SquareFootage = 0 };
            var result = _validator.TestValidate(command);
            result.ShouldHaveValidationErrorFor(x => x.SquareFootage);
        }

        [Fact]
        public void Should_Have_Error_When_NumberOfBedrooms_Is_Less_Than_Zero()
        {
            var command = new UpdatePropertyListingCommand { NumberOfBedrooms = -1 };
            var result = _validator.TestValidate(command);
            result.ShouldHaveValidationErrorFor(x => x.NumberOfBedrooms);
        }

        [Fact]
        public void Should_Have_Error_When_NumberOfBathrooms_Is_Less_Than_Zero()
        {
            var command = new UpdatePropertyListingCommand { NumberOfBathrooms = -1 };
            var result = _validator.TestValidate(command);
            result.ShouldHaveValidationErrorFor(x => x.NumberOfBathrooms);
        }

        [Fact]
        public void Should_Have_Error_When_Description_Is_Empty()
        {
            var command = new UpdatePropertyListingCommand { Description = string.Empty };
            var result = _validator.TestValidate(command);
            result.ShouldHaveValidationErrorFor(x => x.Description);
        }

        [Fact]
        public void Should_Have_Error_When_Description_Exceeds_Maximum_Length()
        {
            var command = new UpdatePropertyListingCommand { Description = new string('D', 401) };
            var result = _validator.TestValidate(command);
            result.ShouldHaveValidationErrorFor(x => x.Description);
        }

        [Fact]
        public void Should_Have_Error_When_Status_Is_Empty()
        {
            var command = new UpdatePropertyListingCommand { Status = string.Empty };
            var result = _validator.TestValidate(command);
            result.ShouldHaveValidationErrorFor(x => x.Status);
        }

        [Fact]
        public void Should_Have_Error_When_Status_Is_Invalid()
        {
            var command = new UpdatePropertyListingCommand { Status = "invalid" };
            var result = _validator.TestValidate(command);
            result.ShouldHaveValidationErrorFor(x => x.Status)
                  .WithErrorMessage("Status must be either 'available' or 'sold'.");
        }

        [Fact]
        public void Should_Have_Error_When_ListingDate_Is_Empty()
        {
            var command = new UpdatePropertyListingCommand { ListingDate = default };
            var result = _validator.TestValidate(command);
            result.ShouldHaveValidationErrorFor(x => x.ListingDate);
        }

        [Fact]
        public void Should_Have_Error_When_ImageURLs_Is_Empty()
        {
            var command = new UpdatePropertyListingCommand { ImageURLs = string.Empty };
            var result = _validator.TestValidate(command);
            result.ShouldHaveValidationErrorFor(x => x.ImageURLs);
        }

        [Fact]
        public void Should_Have_Error_When_UserID_Is_Empty()
        {
            var command = new UpdatePropertyListingCommand { UserID = Guid.Empty };
            var result = _validator.TestValidate(command);
            result.ShouldHaveValidationErrorFor(x => x.UserID);
        }

        [Fact]
        public void Should_Not_Have_Error_When_Command_Is_Valid()
        {
            var command = new UpdatePropertyListingCommand
            {
                PropertyId = Guid.NewGuid(),
                Title = "Beautiful Home",
                Address = "123 Main St",
                Price = 250000,
                SquareFootage = 1500,
                NumberOfBedrooms = 3,
                NumberOfBathrooms = 2,
                Description = "A lovely home in a great neighborhood.",
                Status = "available",
                ListingDate = DateTime.Now,
                ImageURLs = "http://example.com/image.jpg",
                UserID = Guid.NewGuid()
            };
            var result = _validator.TestValidate(command);
            result.ShouldNotHaveAnyValidationErrors();
        }
    }
}
