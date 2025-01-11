using Application.DTOs;
using Application.Use_Cases.ClientInquiries.Queries;
using Application.Use_Cases.ClientInquiries.QueryHandler;
using AutoMapper;
using Domain.Common;
using Domain.Entities;
using Domain.Repositories;
using FluentAssertions;
using NSubstitute;

namespace RealEstateManagement.Application.UnitTests
{
    public class SearchAllPropertiesQueryHandlerTests
    {
        private readonly IMapper _mapper;
        private readonly IClientInquiryRepository _repository;
        private readonly SearchAllPropertiesQueryHandler _handler;

        public SearchAllPropertiesQueryHandlerTests()
        {
            _mapper = Substitute.For<IMapper>();
            _repository = Substitute.For<IClientInquiryRepository>();
            _handler = new SearchAllPropertiesQueryHandler(_mapper, _repository);
        }

        [Fact]
        public async Task Handle_ShouldReturnSuccessResult_WhenRepositoryReturnsSuccess()
        {
            // Arrange
            var searchQuery = "Test";
            var propertyListings = new List<PropertyListing>
            {
                new PropertyListing { PropertyId = Guid.NewGuid(), Title = "Test Property 1" },
                new PropertyListing { PropertyId = Guid.NewGuid(), Title = "Test Property 2" }
            };
            var result = Result<IEnumerable<PropertyListing>>.Success(propertyListings);
            _repository.SearchAllPropertiesAsync(searchQuery).Returns(result);

            var propertyListingDtos = propertyListings.Select(pl => new PropertyListingDto { PropertyId = pl.PropertyId, Title = pl.Title }).ToList();
            _mapper.Map<PropertyListingDto>(Arg.Any<PropertyListing>()).Returns(args => new PropertyListingDto { PropertyId = ((PropertyListing)args[0]).PropertyId, Title = ((PropertyListing)args[0]).Title });

            var query = new SearchAllPropertiesQuery { SearchQuery = searchQuery };

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
            var searchQuery = "Test";
            var errorMessage = "Error occurred";
            var result = Result<IEnumerable<PropertyListing>>.Failure(errorMessage);
            _repository.SearchAllPropertiesAsync(searchQuery).Returns(result);

            var query = new SearchAllPropertiesQuery { SearchQuery = searchQuery };

            // Act
            var response = await _handler.Handle(query, CancellationToken.None);

            // Assert
            response.IsSuccess.Should().BeFalse();
            response.ErrorMessage.Should().Be(errorMessage);
        }
    }
}
