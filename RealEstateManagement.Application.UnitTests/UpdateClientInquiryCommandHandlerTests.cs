using Application.Use_Cases.ClientInquiries.Commands;
using Application.Use_Cases.ClientInquiries.CommandHandlers;
using AutoMapper;
using Domain.Common;
using Domain.Entities;
using Domain.Repositories;
using FluentAssertions;
using NSubstitute;

namespace RealEstateManagement.Application.UnitTests
{
    public class UpdateClientInquiryCommandHandlerTests
    {
        private readonly IClientInquiryRepository _repository;
        private readonly IMapper _mapper;
        private readonly UpdateClientInquiryCommandHandler _handler;

        public UpdateClientInquiryCommandHandlerTests()
        {
            _repository = Substitute.For<IClientInquiryRepository>();
            _mapper = Substitute.For<IMapper>();
            _handler = new UpdateClientInquiryCommandHandler(_repository, _mapper);
        }

        [Fact]
        public async Task Handle_ShouldReturnSuccessResult_WhenRepositoryReturnsSuccess()
        {
            // Arrange
            var command = new UpdateClientInquiryCommand
            {
                InquiryId = Guid.NewGuid(),
                Types = new List<string> { "House", "Apartment" },
                MinPrice = 100000,
                MaxPrice = 500000,
                MinSquareFootage = 1000,
                MaxSquareFootage = 3000,
                NumberOfBedrooms = 3,
                NumberOfBathrooms = 2
            };
            var clientInquiry = new ClientInquiry
            {
                InquiryId = command.InquiryId,
                ClientId = Guid.NewGuid(),
                Types = command.Types,
                MinPrice = command.MinPrice,
                MaxPrice = command.MaxPrice,
                MinSquareFootage = command.MinSquareFootage,
                MaxSquareFootage = command.MaxSquareFootage,
                NumberOfBedrooms = command.NumberOfBedrooms,
                NumberOfBathrooms = command.NumberOfBathrooms
            };
            _mapper.Map<ClientInquiry>(command).Returns(clientInquiry);

            var result = Result<Guid>.Success(clientInquiry.InquiryId);
            _repository.UpdateInquiryAsync(clientInquiry).Returns(result);

            // Act
            var response = await _handler.Handle(command, CancellationToken.None);

            // Assert
            response.IsSuccess.Should().BeTrue();
            response.Data.Should().Be(clientInquiry.InquiryId);
        }

        [Fact]
        public async Task Handle_ShouldReturnFailureResult_WhenRepositoryReturnsFailure()
        {
            // Arrange
            var command = new UpdateClientInquiryCommand
            {
                InquiryId = Guid.NewGuid(),
                Types = new List<string> { "House", "Apartment" },
                MinPrice = 100000,
                MaxPrice = 500000,
                MinSquareFootage = 1000,
                MaxSquareFootage = 3000,
                NumberOfBedrooms = 3,
                NumberOfBathrooms = 2
            };
            var clientInquiry = new ClientInquiry
            {
                InquiryId = command.InquiryId,
                ClientId = Guid.NewGuid(),
                Types = command.Types,
                MinPrice = command.MinPrice,
                MaxPrice = command.MaxPrice,
                MinSquareFootage = command.MinSquareFootage,
                MaxSquareFootage = command.MaxSquareFootage,
                NumberOfBedrooms = command.NumberOfBedrooms,
                NumberOfBathrooms = command.NumberOfBathrooms
            };
            _mapper.Map<ClientInquiry>(command).Returns(clientInquiry);

            var errorMessage = "Error occurred";
            var result = Result<Guid>.Failure(errorMessage);
            _repository.UpdateInquiryAsync(clientInquiry).Returns(result);

            // Act
            var response = await _handler.Handle(command, CancellationToken.None);

            // Assert
            response.IsSuccess.Should().BeFalse();
            response.ErrorMessage.Should().Be(errorMessage);
        }
    }
}
