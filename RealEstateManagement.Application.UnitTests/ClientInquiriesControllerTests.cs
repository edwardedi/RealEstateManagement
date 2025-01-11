using Application.DTOs;
using Application.Use_Cases.ClientInquiries.Commands;
using Application.Use_Cases.ClientInquiries.Queries;
using Application.Utils;
using Domain.Common;
using FluentAssertions;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using NSubstitute;
using RealEstateManagement.Controllers;
using Xunit;

namespace RealEstateManagement.Application.UnitTests
{
    public class ClientInquiriesControllerTests
    {
        private readonly IMediator _mediator;
        private readonly ClientInquiriesController _controller;

        public ClientInquiriesControllerTests()
        {
            _mediator = Substitute.For<IMediator>();
            _controller = new ClientInquiriesController(_mediator);
        }

        [Fact]
        public async Task CreateInquiry_ShouldReturnOk_WhenCreationIsSuccessful()
        {
            // Arrange
            var command = new CreateClientInquiryCommand
            {
                ClientId = Guid.NewGuid(),
                MinPrice = 100000,
                MaxPrice = 500000
            };
            _mediator.Send(command).Returns(Result<Guid>.Success(Guid.NewGuid()));

            // Act
            var response = await _controller.CreateInquiry(command);

            // Assert
            response.Should().BeOfType<OkObjectResult>();
        }

        [Fact]
        public async Task CreateInquiry_ShouldReturnBadRequest_WhenCreationFails()
        {
            // Arrange
            var command = new CreateClientInquiryCommand
            {
                ClientId = Guid.NewGuid(),
                MinPrice = 100000,
                MaxPrice = 500000
            };
            var errorMessage = "Error occurred";
            _mediator.Send(command).Returns(Result<Guid>.Failure(errorMessage));

            // Act
            var response = await _controller.CreateInquiry(command);

            // Assert
            response.Should().BeOfType<BadRequestObjectResult>();
        }

        [Fact]
        public async Task GetInquiryById_ShouldReturnOkResult_WhenInquiryIsFound()
        {
            // Arrange
            var inquiryId = Guid.NewGuid();
            var inquiry = new ClientInquiryDto { InquiryId = inquiryId };
            var result = Result<ClientInquiryDto>.Success(inquiry);
            _mediator.Send(Arg.Any<GetInquiryByIdQuery>()).Returns(result);

            // Act
            var response = await _controller.GetInquiryById(inquiryId);

            // Assert
            response.Should().BeOfType<OkObjectResult>();
        }


        [Fact]
        public async Task GetAllInquiriesAsync_ShouldReturnOkResult_WhenInquiriesAreFound()
        {
            // Arrange
            var inquiries = new List<ClientInquiryDto> { new ClientInquiryDto { InquiryId = Guid.NewGuid() } };
            var result = Result<List<ClientInquiryDto>>.Success(inquiries);
            _mediator.Send(Arg.Any<GetAllInquiriesQuery>()).Returns(result);

            // Act
            var response = await _controller.GetAllInquiriesAsync();

            // Assert
            response.Result.Should().BeOfType<OkObjectResult>();
        }

        [Fact]
        public async Task GetInquiriesByClientId_ShouldReturnOkResult_WhenInquiriesAreFound()
        {
            // Arrange
            var clientId = Guid.NewGuid();
            var inquiries = new List<ClientInquiryDto> { new ClientInquiryDto { InquiryId = Guid.NewGuid() } };
            var result = Result<List<ClientInquiryDto>>.Success(inquiries);
            _mediator.Send(Arg.Any<GetInquiryByClientIdQuery>()).Returns(result);

            // Act
            var response = await _controller.GetInquiriesByClientId(clientId);

            // Assert
            response.Result.Should().BeOfType<OkObjectResult>();
        }

        [Fact]
        public async Task UpdateInquiry_ShouldReturnOk_WhenUpdateIsSuccessful()
        {
            // Arrange
            var command = new UpdateClientInquiryCommand
            {
                InquiryId = Guid.NewGuid(),
                MinPrice = 100000,
                MaxPrice = 500000
            };
            _mediator.Send(command).Returns(Result<Guid>.Success(command.InquiryId));

            // Act
            var response = await _controller.UpdateInquiry(command.InquiryId, command);

            // Assert
            response.Should().BeOfType<OkObjectResult>();
        }

        [Fact]
        public async Task UpdateInquiry_ShouldReturnBadRequest_WhenIdsDoNotMatch()
        {
            // Arrange
            var command = new UpdateClientInquiryCommand
            {
                InquiryId = Guid.NewGuid(),
                MinPrice = 100000,
                MaxPrice = 500000
            };

            // Act
            var response = await _controller.UpdateInquiry(Guid.NewGuid(), command);

            // Assert
            response.Should().BeOfType<BadRequestObjectResult>();
        }

        [Fact]
        public async Task DeleteInquiry_ShouldReturnOk_WhenDeleteIsSuccessful()
        {
            // Arrange
            var inquiryId = Guid.NewGuid();
            _mediator.Send(Arg.Any<DeleteClientInquiryCommand>()).Returns(Result<Guid>.Success(inquiryId));

            // Act
            var response = await _controller.DeleteInquiry(inquiryId);

            // Assert
            response.Should().BeOfType<OkObjectResult>();
        }

        [Fact]
        public async Task DeleteInquiry_ShouldReturnBadRequest_WhenDeleteFails()
        {
            // Arrange
            var inquiryId = Guid.NewGuid();
            var errorMessage = "Error occurred";
            _mediator.Send(Arg.Any<DeleteClientInquiryCommand>()).Returns(Result<Guid>.Failure(errorMessage));

            // Act
            var response = await _controller.DeleteInquiry(inquiryId);

            // Assert
            response.Should().BeOfType<BadRequestObjectResult>();
        }

        [Fact]
        public async Task SearchAllPropertiesAsync_ShouldReturnOkResult_WhenPropertiesAreFound()
        {
            // Arrange
            var searchQuery = "test";
            var properties = new List<PropertyListingDto> { new PropertyListingDto { PropertyId = Guid.NewGuid() } };
            var result = Result<List<PropertyListingDto>>.Success(properties);
            _mediator.Send(Arg.Any<SearchAllPropertiesQuery>()).Returns(result);

            // Act
            var response = await _controller.SearchAllPropertiesAsync(searchQuery);

            // Assert
            response.Result.Should().BeOfType<OkObjectResult>();
        }
    }
}
