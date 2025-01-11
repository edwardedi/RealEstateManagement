using Application.Use_Cases.CommandHandlers;
using Application.Use_Cases.Commands;
using AutoMapper;
using Domain.Common;
using Domain.Entities;
using Domain.Repositories;
using FluentAssertions;
using NSubstitute;

namespace RealEstateManagement.Application.UnitTests
{
    public class UpdatePropertyListingCommandHandlerTests
    {
        private readonly IPropertyListingRepository repository;
        private readonly IMapper mapper;

        public UpdatePropertyListingCommandHandlerTests()
        {
            repository = Substitute.For<IPropertyListingRepository>();
            mapper = Substitute.For<IMapper>();
        }

        [Fact]
        public async Task Given_UpdatePropertyListingCommandHandler_When_HandleIsCalled_Then_SuccessResultShouldBeReturned()
        {
            // Arrange
            var command = GenerateUpdatePropertyListingCommand();
            var propertyListing = mapper.Map<PropertyListing>(command);
            repository.UpdateListingAsync(propertyListing).Returns(Result<Guid>.Success(command.PropertyId));

            // Act
            var handler = new UpdatePropertyListingCommandHandler(repository, mapper);
            var result = await handler.Handle(command, CancellationToken.None);

            // Assert
            result.IsSuccess.Should().BeTrue();
            result.Data.Should().Be(command.PropertyId);
        }

        [Fact]
        public async Task Given_UpdatePropertyListingCommandHandler_When_HandleIsCalled_Then_FailureResultShouldBeReturned()
        {
            // Arrange
            var command = GenerateUpdatePropertyListingCommand();
            var propertyListing = mapper.Map<PropertyListing>(command);
            repository.UpdateListingAsync(propertyListing).Returns(Result<Guid>.Failure("Database error"));

            // Act
            var handler = new UpdatePropertyListingCommandHandler(repository, mapper);
            var result = await handler.Handle(command, CancellationToken.None);

            // Assert
            result.IsSuccess.Should().BeFalse();
            result.ErrorMessage.Should().Be("Database error");
        }

        [Fact]
        public async Task Given_UpdatePropertyListingCommandHandler_When_PropertyListingNotFound_Then_FailureResultShouldBeReturned()
        {
            // Arrange
            var command = GenerateUpdatePropertyListingCommand();
            var propertyListing = mapper.Map<PropertyListing>(command);
            repository.UpdateListingAsync(propertyListing).Returns(Result<Guid>.Failure("Listing not found."));

            // Act
            var handler = new UpdatePropertyListingCommandHandler(repository, mapper);
            var result = await handler.Handle(command, CancellationToken.None);

            // Assert
            result.IsSuccess.Should().BeFalse();
            result.ErrorMessage.Should().Be("Listing not found.");
        }

        private  static UpdatePropertyListingCommand GenerateUpdatePropertyListingCommand()
        {
            return new UpdatePropertyListingCommand
            {
                PropertyId = new Guid("57632f09-f390-4be3-9259-eb468e096388"),
                Address = "123 Main St",
                Type = "House",
                Price = 100000,
                SquareFootage = 1500,
                NumberOfBedrooms = 3,
                NumberOfBathrooms = 2,
                Description = "Description 1",
                Status = "Available",
                ListingDate = DateTime.Now,
                ImageURLs = "http://example.com/image1.jpg",
                UserID = Guid.NewGuid()
            };
        }
    }
}
