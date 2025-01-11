using Application.DTOs;
using Application.Use_Cases.PropertyListings.Queries;
using Application.Use_Cases.Queries;
using Application.Utils;
using Domain.Common;
using FluentAssertions;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using NSubstitute;
using RealEstateManagement.Controllers;

namespace RealEstateManagement.Application.UnitTests
{
    public class PropertyListingsControllerTests
    {
        private readonly IMediator _mediator;
        private readonly PropertyListingsController _controller;

        public PropertyListingsControllerTests()
        {
            _mediator = Substitute.For<IMediator>();
            _controller = new PropertyListingsController(_mediator);
        }


        [Fact]
        public async Task GetAllPropertyListingsAsync_ShouldReturnOkResult_WhenListingsAreFound()
        {
            // Arrange
            var listings = new List<PropertyListingDto> { new PropertyListingDto { PropertyId = Guid.NewGuid() } };
            var result = Result<List<PropertyListingDto>>.Success(listings);
            _mediator.Send(Arg.Any<GetAllPropertyListingQuery>()).Returns(result);

            // Act
            var response = await _controller.GetAllPropertyListingsAsync();

            // Assert
            response.Result.Should().BeOfType<OkObjectResult>();
        }

        [Fact]
        public async Task GetListingByIdAsync_ShouldReturnOkResult_WhenListingIsFound()
        {
            // Arrange
            var listingId = Guid.NewGuid();
            var listing = new PropertyListingDto { PropertyId = listingId };
            var result = Result<PropertyListingDto>.Success(listing);
            _mediator.Send(Arg.Any<GetListingByIdQuery>()).Returns(result);

            // Act
            var response = await _controller.GetListingByIdAsync(listingId);

            // Assert
            response.Result.Should().BeOfType<OkObjectResult>();
        }

        [Fact]
        public async Task GetListingByIdAsync_ShouldReturnNotFound_WhenListingIsNotFound()
        {
            // Arrange
            var listingId = Guid.NewGuid();
            var result = Result<PropertyListingDto>.Success(null);
            _mediator.Send(Arg.Any<GetListingByIdQuery>()).Returns(result);

            // Act
            var response = await _controller.GetListingByIdAsync(listingId);

            // Assert
            response.Result.Should().BeOfType<NotFoundObjectResult>();
        }

        [Fact]
        public async Task GetListingsByUserId_ShouldReturnOkResult_WhenListingsAreFound()
        {
            // Arrange
            var userId = Guid.NewGuid();
            var listings = new List<PropertyListingDto> { new PropertyListingDto { PropertyId = Guid.NewGuid() } };
            var result = Result<List<PropertyListingDto>>.Success(listings);
            _mediator.Send(Arg.Any<GetListingsByUserIdQuery>()).Returns(result);

            // Act
            var response = await _controller.GetListingsByUserId(userId);

            // Assert
            response.Result.Should().BeOfType<OkObjectResult>();
        }

        [Fact]
        public async Task GetFilteredPropertyListings_ShouldReturnOkResult_WhenListingsAreFound()
        {
            // Arrange
            var query = new GetFilteredPropertyListingsQuery
            {
                Page = 1,
                PageSize = 10,
                Type = "House",
                Price = 100000,
                NumberOfBathrooms = 2,
                NumberOfBedrooms = 3,
                Status = "Available",
                SquareFootage = 1500
            };
            var listings = new PagedResult<PropertyListingDto>(new List<PropertyListingDto> { new PropertyListingDto { PropertyId = Guid.NewGuid() } }, 1);
            var result = Result<PagedResult<PropertyListingDto>>.Success(listings);
            _mediator.Send(query).Returns(result);

            // Act
            var response = await _controller.GetFilteredPropertyListings(query.Page, query.PageSize, query.Type, query.Price, query.NumberOfBathrooms, query.NumberOfBedrooms, query.Status, query.SquareFootage);

            // Assert
            response.Result.Should().BeOfType<OkObjectResult>();
        }
    }
}
