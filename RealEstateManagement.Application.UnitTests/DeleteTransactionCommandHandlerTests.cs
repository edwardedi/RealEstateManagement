using Domain.Common;
using Domain.Repositories;
using FluentAssertions;
using NSubstitute;
using RealEstateManagement.Application.Transactions.CommandHandlers;
using RealEstateManagement.Application.Transactions.Commands;

namespace RealEstateManagement.Application.UnitTests
{
    public class DeleteTransactionCommandHandlerTests
    {
        private readonly ITransactionRepository _repository;
        private readonly DeleteTransactionCommandHandler _handler;

        public DeleteTransactionCommandHandlerTests()
        {
            _repository = Substitute.For<ITransactionRepository>();
            _handler = new DeleteTransactionCommandHandler(_repository);
        }

        [Fact]
        public async Task Handle_ShouldReturnSuccessResult_WhenRepositoryReturnsSuccess()
        {
            // Arrange
            var transactionId = Guid.NewGuid();
            var result = Result<Guid>.Success(transactionId);
            _repository.DeleteTransactionAsync(transactionId).Returns(result);

            var command = new DeleteTransactionCommand { TransactionId = transactionId };

            // Act
            var response = await _handler.Handle(command, CancellationToken.None);

            // Assert
            response.IsSuccess.Should().BeTrue();
            response.Data.Should().Be(transactionId);
        }

        [Fact]
        public async Task Handle_ShouldReturnFailureResult_WhenRepositoryReturnsFailure()
        {
            // Arrange
            var transactionId = Guid.NewGuid();
            var errorMessage = "Error occurred";
            var result = Result<Guid>.Failure(errorMessage);
            _repository.DeleteTransactionAsync(transactionId).Returns(result);

            var command = new DeleteTransactionCommand { TransactionId = transactionId };

            // Act
            var response = await _handler.Handle(command, CancellationToken.None);

            // Assert
            response.IsSuccess.Should().BeFalse();
            response.ErrorMessage.Should().Be(errorMessage);
        }
    }
}
