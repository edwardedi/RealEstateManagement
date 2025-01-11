using Application.DTOs;
using Application.Use_Cases.Queries;
using Application.Use_Cases.QueryHandlers;
using AutoMapper;
using Domain.Common;
using Domain.Entities;
using Domain.Repositories;
using FluentAssertions;
using NSubstitute;

namespace RealEstateManagement.Application.UnitTests
{
    public class GetListingByIdQueryHandlerTests
    {
        private readonly IMapper _mapper;
        private readonly IPropertyListingRepository _repository;
        private readonly GetListingByIdQueryHandler _handler;

        public GetListingByIdQueryHandlerTests()
        {
            _mapper = Substitute.For<IMapper>();
            _repository = Substitute.For<IPropertyListingRepository>();
            _handler = new GetListingByIdQueryHandler(_mapper, _repository);
        }

        [Fact]
        public async Task Handle_ShouldReturnSuccessResult_WhenRepositoryReturnsSuccess()
        {
            // Arrange
            var propertyListing = new PropertyListing { PropertyId = Guid.NewGuid(), Title = "Test Property" };
            var result = Result<PropertyListing>.Success(propertyListing);
            _repository.GetListingByIdAsync(propertyListing.PropertyId).Returns(result);

            var propertyListingDto = new PropertyListingDto { PropertyId = propertyListing.PropertyId, Title = propertyListing.Title };
            _mapper.Map<PropertyListingDto>(propertyListing).Returns(propertyListingDto);

            var query = new GetListingByIdQuery { PropertyId = propertyListing.PropertyId };

            // Act
            var response = await _handler.Handle(query, CancellationToken.None);

            // Assert
            response.IsSuccess.Should().BeTrue();
            response.Data.Should().BeEquivalentTo(propertyListingDto);
        }

        [Fact]
        public async Task Handle_ShouldReturnFailureResult_WhenRepositoryReturnsFailure()
        {
            // Arrange
            var errorMessage = "Error occurred";
            var result = Result<PropertyListing>.Failure(errorMessage);
            _repository.GetListingByIdAsync(Arg.Any<Guid>()).Returns(result);

            var query = new GetListingByIdQuery { PropertyId = Guid.NewGuid() };

            // Act
            var response = await _handler.Handle(query, CancellationToken.None);

            // Assert
            response.IsSuccess.Should().BeFalse();
            response.ErrorMessage.Should().Be(errorMessage);
        }
    }
}
