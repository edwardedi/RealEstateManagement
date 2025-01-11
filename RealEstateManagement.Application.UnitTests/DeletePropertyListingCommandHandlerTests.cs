using Application.Use_Cases.CommandHandlers;
using Application.Use_Cases.Commands;
using Domain.Common;
using Domain.Repositories;
using FluentAssertions;
using NSubstitute;

namespace RealEstateManagement.Application.UnitTests
{
    public class DeletePropertyListingCommandHandlerTests
    {
        private readonly IPropertyListingRepository repository;

        public DeletePropertyListingCommandHandlerTests()
        {
            repository = Substitute.For<IPropertyListingRepository>();
        }

        [Fact]
        public async Task Given_DeletePropertyListingCommandHandler_When_HandleIsCalled_Then_ListingShouldBeDeleted()
        {
            // Arrange
            var propertyId = new Guid("7b2ed928-a903-47f5-8bcd-c93216154f29");
            var result = Result<Guid>.Success(propertyId);
            repository.DeleteListingAsync(propertyId).Returns(result);
            var command = new DeletePropertyListingCommand { PropertyId = propertyId };

            // Act
            var handler = new DeletePropertyListingCommandHandler(repository);
            var response = await handler.Handle(command, CancellationToken.None);

            // Assert
            response.Should().NotBeNull();
            response.IsSuccess.Should().BeTrue();
            response.Data.Should().Be(propertyId);
        }

        [Fact]
        public async Task Given_DeletePropertyListingCommandHandler_When_HandleFails_Then_ErrorShouldBeReturned()
        {
            // Arrange
            var propertyId = new Guid("7b2ed928-a903-47f5-8bcd-c93216154f29");
            var result = Result<Guid>.Failure("Failed to delete property listing.");
            repository.DeleteListingAsync(propertyId).Returns(result);
            var command = new DeletePropertyListingCommand { PropertyId = propertyId };

            // Act
            var handler = new DeletePropertyListingCommandHandler(repository);
            var response = await handler.Handle(command, CancellationToken.None);

            // Assert
            response.Should().NotBeNull();
            response.IsSuccess.Should().BeFalse();
            response.ErrorMessage.Should().Be("Failed to delete property listing.");
        }
    }
}
