using Application.DTOs;
using Application.Use_Cases.PropertyListings.Filtering;
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
    public class GetFilteredPropertyListingsQueryHandlerTests
    {
        private readonly IPropertyListingRepository _repository;
        private readonly IMapper _mapper;
        private IEnumerable<IPropertyListingFilterStrategy> _filterStrategies;
        private readonly GetFilteredPropertyListingsQueryHandler _handler;

        public GetFilteredPropertyListingsQueryHandlerTests()
        {
            _repository = Substitute.For<IPropertyListingRepository>();
            _mapper = Substitute.For<IMapper>();
            _filterStrategies = Substitute.For<IEnumerable<IPropertyListingFilterStrategy>>();
            _handler = new GetFilteredPropertyListingsQueryHandler(_repository, _mapper, _filterStrategies);
        }

        [Fact]
        public async Task Handle_ShouldReturnSuccessResult_WhenRepositoryReturnsSuccess()
        {
            // Arrange
            var propertyListings = new List<PropertyListing>
            {
                new PropertyListing { PropertyId = Guid.NewGuid(), Title = "Test Property 1" },
                new PropertyListing { PropertyId = Guid.NewGuid(), Title = "Test Property 2" }
            };
            var result = Result<IEnumerable<PropertyListing>>.Success(propertyListings);
            _repository.GetAllListingsAsync().Returns(result);

            var propertyListingDtos = propertyListings.Select(pl => new PropertyListingDto { PropertyId = pl.PropertyId, Title = pl.Title }).ToList();
            _mapper.Map<List<PropertyListingDto>>(Arg.Any<IEnumerable<PropertyListing>>()).Returns(propertyListingDtos);

            var query = new GetFilteredPropertyListingsQuery { Page = 1, PageSize = 10 };

            // Act
            var response = await _handler.Handle(query, CancellationToken.None);

            // Assert
            response.IsSuccess.Should().BeTrue();
            response.Data.Data.Should().BeEquivalentTo(propertyListingDtos);
            response.Data.TotalCount.Should().Be(propertyListings.Count);
        }

        [Fact]
        public async Task Handle_ShouldReturnFailureResult_WhenRepositoryReturnsFailure()
        {
            // Arrange
            var errorMessage = "Error occurred";
            var result = Result<IEnumerable<PropertyListing>>.Failure(errorMessage);
            _repository.GetAllListingsAsync().Returns(result);

            var query = new GetFilteredPropertyListingsQuery { Page = 1, PageSize = 10 };

            // Act
            var response = await _handler.Handle(query, CancellationToken.None);

            // Assert
            response.IsSuccess.Should().BeFalse();
            response.ErrorMessage.Should().Be(errorMessage);
        }

        [Fact]
        public async Task Handle_ShouldApplyFiltersAndReturnPagedResult()
        {
            // Arrange
            var propertyListings = new List<PropertyListing>
            {
                new PropertyListing { PropertyId = Guid.NewGuid(), Title = "Test Property 1" },
                new PropertyListing { PropertyId = Guid.NewGuid(), Title = "Test Property 2" }
            };
            var result = Result<IEnumerable<PropertyListing>>.Success(propertyListings);
            _repository.GetAllListingsAsync().Returns(result);

            var propertyListingDtos = propertyListings.Select(pl => new PropertyListingDto { PropertyId = pl.PropertyId, Title = pl.Title }).ToList();
            _mapper.Map<List<PropertyListingDto>>(Arg.Any<IEnumerable<PropertyListing>>()).Returns(propertyListingDtos);

            var filterStrategy = Substitute.For<IPropertyListingFilterStrategy>();
            filterStrategy.ApplyFilter(Arg.Any<IQueryable<PropertyListing>>(), Arg.Any<GetFilteredPropertyListingsQuery>())
                .Returns(args => args.ArgAt<IQueryable<PropertyListing>>(0));
            _filterStrategies = new List<IPropertyListingFilterStrategy> { filterStrategy };

            var query = new GetFilteredPropertyListingsQuery { Page = 1, PageSize = 10 };

            // Act
            var response = await _handler.Handle(query, CancellationToken.None);

            // Assert
            response.IsSuccess.Should().BeTrue();
            response.Data.Data.Should().BeEquivalentTo(propertyListingDtos);
            response.Data.TotalCount.Should().Be(propertyListings.Count);
        }
    }
}
