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
    public class CreatePropertyListingCommandHandlerTests
    {
        private readonly IPropertyListingRepository repository;
        private readonly IMapper mapper;

        public CreatePropertyListingCommandHandlerTests()
        {
            repository = Substitute.For<IPropertyListingRepository>();
            mapper = Substitute.For<IMapper>();
        }

        [Fact]
        public async Task Given_CreatePropertyListingCommandHandler_When_HandleIsCalled_Then_SuccessResultShouldBeReturned()
        {
            // Arrange
            var command = GenerateCreatePropertyListingCommand();
            var propertyListing = GeneratePropertyListing(command);
            var propertyId = new Guid("23c15cbb-9fbc-4076-9685-2d691996a323");
            mapper.Map<PropertyListing>(command).Returns(propertyListing);
            repository.AddListingAsync(propertyListing).Returns(Result<Guid>.Success(propertyId));

            // Act
            var handler = new CreatePropertyListingCommandHandler(repository, mapper);
            var result = await handler.Handle(command, CancellationToken.None);

            // Assert
            result.IsSuccess.Should().BeTrue();
            result.Data.Should().Be(propertyId);
        }

        [Fact]
        public async Task Given_CreatePropertyListingCommandHandler_When_HandleIsCalled_Then_FailureResultShouldBeReturned()
        {
            // Arrange
            var command = GenerateCreatePropertyListingCommand();
            var propertyListing = GeneratePropertyListing(command);
            mapper.Map<PropertyListing>(command).Returns(propertyListing);
            repository.AddListingAsync(propertyListing).Returns(Result<Guid>.Failure("Database error"));

            // Act
            var handler = new CreatePropertyListingCommandHandler(repository, mapper);
            var result = await handler.Handle(command, CancellationToken.None);

            // Assert
            result.IsSuccess.Should().BeFalse();
            result.ErrorMessage.Should().Be("Database error");
        }

        private static CreatePropertyListingCommand GenerateCreatePropertyListingCommand()
        {
            return new CreatePropertyListingCommand
            {
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

        private static PropertyListing GeneratePropertyListing(CreatePropertyListingCommand command)
        {
            return new PropertyListing
            {
                PropertyId = new Guid("23c15cbb-9fbc-4076-9685-2d691996a323"),
                Address = command.Address,
                Type = command.Type,
                Price = command.Price,
                SquareFootage = command.SquareFootage,
                NumberOfBedrooms = command.NumberOfBedrooms,
                NumberOfBathrooms = command.NumberOfBathrooms,
                Description = command.Description,
                Status = command.Status,
                ListingDate = command.ListingDate,
                ImageURLs = command.ImageURLs,
                UserID = command.UserID
            };
        }
    }
}
