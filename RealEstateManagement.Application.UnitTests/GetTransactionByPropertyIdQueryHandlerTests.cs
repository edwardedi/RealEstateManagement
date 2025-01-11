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
    public class GetTransactionByPropertyIdQueryHandlerTests
    {
        private readonly ITransactionRepository _repository;
        private readonly IMapper _mapper;
        private readonly GetTransactionByPropertyIdQueryHandler _handler;

        public GetTransactionByPropertyIdQueryHandlerTests()
        {
            _repository = Substitute.For<ITransactionRepository>();
            _mapper = Substitute.For<IMapper>();
            _handler = new GetTransactionByPropertyIdQueryHandler(_mapper, _repository);
        }

        [Fact]
        public async Task Handle_ShouldReturnSuccessResult_WhenRepositoryReturnsSuccess()
        {
            // Arrange
            var propertyId = Guid.NewGuid();
            var transaction = new Transaction
            {
                TransactionId = Guid.NewGuid(),
                PropertyId = propertyId,
                BuyerId = Guid.NewGuid(),
                SellerId = Guid.NewGuid(),
                SalePrice = 250000
            };
            var result = Result<Transaction>.Success(transaction);
            _repository.GetTransactionByPropertyIdAsync(propertyId).Returns(result);

            var transactionDto = new TransactionDto
            {
                TransactionId = transaction.TransactionId,
                PropertyId = transaction.PropertyId,
                BuyerId = transaction.BuyerId,
                SellerId = transaction.SellerId,
                SalePrice = transaction.SalePrice
            };
            _mapper.Map<TransactionDto>(transaction).Returns(transactionDto);

            var query = new GetTransactionByPropertyIdQuery { PropertyId = propertyId };

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
            var propertyId = Guid.NewGuid();
            var errorMessage = "Transaction not found.";
            var result = Result<Transaction>.Failure(errorMessage);
            _repository.GetTransactionByPropertyIdAsync(propertyId).Returns(result);

            var query = new GetTransactionByPropertyIdQuery { PropertyId = propertyId };

            // Act
            var response = await _handler.Handle(query, CancellationToken.None);

            // Assert
            response.IsSuccess.Should().BeFalse();
            response.ErrorMessage.Should().Be(errorMessage);
        }
    }
}
