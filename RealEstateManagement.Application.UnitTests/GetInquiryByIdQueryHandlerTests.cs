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
    public class GetInquiryByIdQueryHandlerTests
    {
        private readonly IClientInquiryRepository _repository;
        private readonly IMapper _mapper;
        private readonly GetInquiryByIdQueryHandler _handler;

        public GetInquiryByIdQueryHandlerTests()
        {
            _repository = Substitute.For<IClientInquiryRepository>();
            _mapper = Substitute.For<IMapper>();
            _handler = new GetInquiryByIdQueryHandler(_mapper, _repository);
        }

        [Fact]
        public async Task Handle_ShouldReturnSuccessResult_WhenRepositoryReturnsSuccess()
        {
            // Arrange
            var inquiryId = Guid.NewGuid();
            var clientInquiry = new ClientInquiry
            {
                InquiryId = inquiryId,
                ClientId = Guid.NewGuid(),
                MinPrice = 100000,
                MaxPrice = 500000,
                MinSquareFootage = 1000,
                MaxSquareFootage = 3000,
                NumberOfBedrooms = 3,
                NumberOfBathrooms = 2
            };
            var result = Result<ClientInquiry>.Success(clientInquiry);
            _repository.GetInquiryByIdAsync(inquiryId).Returns(result);

            var clientInquiryDto = new ClientInquiryDto
            {
                InquiryId = clientInquiry.InquiryId,
                ClientId = clientInquiry.ClientId,
                MinPrice = clientInquiry.MinPrice,
                MaxPrice = clientInquiry.MaxPrice,
                MinSquareFootage = clientInquiry.MinSquareFootage,
                MaxSquareFootage = clientInquiry.MaxSquareFootage,
                NumberOfBedrooms = clientInquiry.NumberOfBedrooms,
                NumberOfBathrooms = clientInquiry.NumberOfBathrooms
            };
            _mapper.Map<ClientInquiryDto>(clientInquiry).Returns(clientInquiryDto);

            var query = new GetInquiryByIdQuery { InquiryId = inquiryId };

            // Act
            var response = await _handler.Handle(query, CancellationToken.None);

            // Assert
            response.IsSuccess.Should().BeTrue();
            response.Data.Should().BeEquivalentTo(clientInquiryDto);
        }

        [Fact]
        public async Task Handle_ShouldReturnFailureResult_WhenRepositoryReturnsFailure()
        {
            // Arrange
            var inquiryId = Guid.NewGuid();
            var errorMessage = "Client inquiry with Id not found";
            var result = Result<ClientInquiry>.Failure(errorMessage);
            _repository.GetInquiryByIdAsync(inquiryId).Returns(result);

            var query = new GetInquiryByIdQuery { InquiryId = inquiryId };

            // Act
            var response = await _handler.Handle(query, CancellationToken.None);

            // Assert
            response.IsSuccess.Should().BeFalse();
            response.ErrorMessage.Should().Be($"Client inquiry with Id {inquiryId} not found");
        }
    }
}


