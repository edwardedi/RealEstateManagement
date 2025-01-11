using Application.Use_Cases.ClientInquiries.CommandHandlers;
using Application.Use_Cases.ClientInquiries.Commands;
using Domain.Common;
using Domain.Repositories;
using FluentAssertions;
using NSubstitute;

namespace RealEstateManagement.Application.UnitTests
{
    public class DeleteClientInquiryCommandHandlerTests
    {
        private readonly IClientInquiryRepository _repository;
        private readonly DeleteClientInquiryCommandHandler _handler;

        public DeleteClientInquiryCommandHandlerTests()
        {
            _repository = Substitute.For<IClientInquiryRepository>();
            _handler = new DeleteClientInquiryCommandHandler(_repository);
        }

        [Fact]
        public async Task Handle_ShouldReturnSuccessResult_WhenRepositoryReturnsSuccess()
        {
            // Arrange
            var inquiryId = Guid.NewGuid();
            var result = Result<Guid>.Success(inquiryId);
            _repository.DeleteInquiryAsync(inquiryId).Returns(result);

            var command = new DeleteClientInquiryCommand { InquiryId = inquiryId };

            // Act
            var response = await _handler.Handle(command, CancellationToken.None);

            // Assert
            response.IsSuccess.Should().BeTrue();
            response.Data.Should().Be(inquiryId);
        }

        [Fact]
        public async Task Handle_ShouldReturnFailureResult_WhenRepositoryReturnsFailure()
        {
            // Arrange
            var inquiryId = Guid.NewGuid();
            var errorMessage = "Error occurred";
            var result = Result<Guid>.Failure(errorMessage);
            _repository.DeleteInquiryAsync(inquiryId).Returns(result);

            var command = new DeleteClientInquiryCommand { InquiryId = inquiryId };

            // Act
            var response = await _handler.Handle(command, CancellationToken.None);

            // Assert
            response.IsSuccess.Should().BeFalse();
            response.ErrorMessage.Should().Be(errorMessage);
        }
    }
}
