using Application.DTOs;
using Application.Use_Cases.Transactions.Queries;
using Application.Use_Cases.Transactions.QueriesHandlers;
using AutoMapper;
using Domain.Common;
using Domain.Entities;
using Domain.Repositories;
using FluentAssertions;
using NSubstitute;

namespace RealEstateManagement.Application.UnitTests
{
    public class GetTransactionByIdQueryHandlerTests
    {
        private readonly ITransactionRepository _repository;
        private readonly IMapper _mapper;
        private readonly GetTransactionByIdQueryHandler _handler;

        public GetTransactionByIdQueryHandlerTests()
        {
            _repository = Substitute.For<ITransactionRepository>();
            _mapper = Substitute.For<IMapper>();
            _handler = new GetTransactionByIdQueryHandler(_repository, _mapper);
        }

        [Fact]
        public async Task Handle_ShouldReturnSuccessResult_WhenRepositoryReturnsSuccess()
        {
            // Arrange
            var transactionId = Guid.NewGuid();
            var transaction = new Transaction
            {
                TransactionId = transactionId,
                PropertyId = Guid.NewGuid(),
                BuyerId = Guid.NewGuid(),
                SellerId = Guid.NewGuid(),
                SalePrice = 250000
            };
            var result = Result<Transaction>.Success(transaction);
            _repository.GetTransactionByIdAsync(transactionId).Returns(result);

            var transactionDto = new TransactionDto
            {
                TransactionId = transaction.TransactionId,
                PropertyId = transaction.PropertyId,
                BuyerId = transaction.BuyerId,
                SellerId = transaction.SellerId,
                SalePrice = transaction.SalePrice
            };
            _mapper.Map<TransactionDto>(transaction).Returns(transactionDto);

            var query = new GetTransactionByIdQuery { TransactionId = transactionId };

            // Act
            var response = await _handler.Handle(query, CancellationToken.None);

            // Assert
            response.IsSuccess.Should().BeTrue();
            response.Data.Should().BeEquivalentTo(transactionDto);
        }

        [Fact]
        public async Task Handle_ShouldReturnFailureResult_WhenRepositoryReturnsFailure()
        {
            // Arrange
            var transactionId = Guid.NewGuid();
            var errorMessage = "Transaction with ID not found.";
            var result = Result<Transaction>.Failure(errorMessage);
            _repository.GetTransactionByIdAsync(transactionId).Returns(result);

            var query = new GetTransactionByIdQuery { TransactionId = transactionId };

            // Act
            var response = await _handler.Handle(query, CancellationToken.None);

            // Assert
            response.IsSuccess.Should().BeFalse();
            response.ErrorMessage.Should().Be($"Transaction with ID {transactionId} not found.");
        }
    }
}
