using Application.DTOs;
using Application.Use_Cases.PropertyListings.Queries;
using Application.Use_Cases.PropertyListings.QueryHandlers;
using AutoMapper;
using Domain.Common;
using Domain.Entities;
using Domain.Repositories;
using FluentAssertions;
using NSubstitute;

namespace RealEstateManagement.Application.UnitTests
{
    public class GetListingsByUserIdQueryHandlerTests
    {
        private readonly IMapper _mapper;
        private readonly IPropertyListingRepository _repository;
        private readonly GetListingsByUserIdQueryHandler _handler;

        public GetListingsByUserIdQueryHandlerTests()
        {
            _mapper = Substitute.For<IMapper>();
            _repository = Substitute.For<IPropertyListingRepository>();
            _handler = new GetListingsByUserIdQueryHandler(_mapper, _repository);
        }

        [Fact]
        public async Task Handle_ShouldReturnSuccessResult_WhenRepositoryReturnsSuccess()
        {
            // Arrange
            var userId = Guid.NewGuid();
            var propertyListings = new List<PropertyListing>
            {
                new PropertyListing { PropertyId = Guid.NewGuid(), Title = "Test Property 1", UserID = userId },
                new PropertyListing { PropertyId = Guid.NewGuid(), Title = "Test Property 2", UserID = userId }
            };
            var result = Result<IEnumerable<PropertyListing>>.Success(propertyListings);
            _repository.GetListingsByUserId(userId).Returns(result);

            var propertyListingDtos = propertyListings.Select(pl => new PropertyListingDto { PropertyId = pl.PropertyId, Title = pl.Title, UserID = pl.UserID }).ToList();
            _mapper.Map<PropertyListingDto>(Arg.Any<PropertyListing>()).Returns(args => new PropertyListingDto { PropertyId = ((PropertyListing)args[0]).PropertyId, Title = ((PropertyListing)args[0]).Title, UserID = ((PropertyListing)args[0]).UserID });

            var query = new GetListingsByUserIdQuery { UserId = userId };

            // Act
            var response = await _handler.Handle(query, CancellationToken.None);

            // Assert
            response.IsSuccess.Should().BeTrue();
            response.Data.Should().BeEquivalentTo(propertyListingDtos);
        }

        [Fact]
        public async Task Handle_ShouldReturnFailureResult_WhenRepositoryReturnsFailure()
        {
            // Arrange
            var errorMessage = "Error occurred";
            var result = Result<IEnumerable<PropertyListing>>.Failure(errorMessage);
            _repository.GetListingsByUserId(Arg.Any<Guid>()).Returns(result);

            var query = new GetListingsByUserIdQuery { UserId = Guid.NewGuid() };

            // Act
            var response = await _handler.Handle(query, CancellationToken.None);

            // Assert
            response.IsSuccess.Should().BeFalse();
            response.ErrorMessage.Should().Be(errorMessage);
        }
    }
}

