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
    public class GetAllTransactionsQueryHandlerTests
    {
        private readonly IMapper _mapper;
        private readonly ITransactionRepository _repository;
        private readonly GetAllTransactionsQueryHandler _handler;

        public GetAllTransactionsQueryHandlerTests()
        {
            _mapper = Substitute.For<IMapper>();
            _repository = Substitute.For<ITransactionRepository>();
            _handler = new GetAllTransactionsQueryHandler(_mapper, _repository);
        }

        [Fact]
        public async Task Handle_ShouldReturnSuccessResult_WhenRepositoryReturnsSuccess()
        {
            // Arrange
            var transactions = new List<Transaction>
            {
                new Transaction { TransactionId = Guid.NewGuid(), PropertyId = Guid.NewGuid(), BuyerId = Guid.NewGuid(), SellerId = Guid.NewGuid(), SalePrice = 250000 },
                new Transaction { TransactionId = Guid.NewGuid(), PropertyId = Guid.NewGuid(), BuyerId = Guid.NewGuid(), SellerId = Guid.NewGuid(), SalePrice = 300000 }
            };
            var result = Result<IEnumerable<Transaction>>.Success(transactions);
            _repository.GetAllTransactionsAsync().Returns(result);

            var transactionDtos = transactions.Select(t => new TransactionDto { TransactionId = t.TransactionId, PropertyId = t.PropertyId, BuyerId = t.BuyerId, SellerId = t.SellerId, SalePrice = t.SalePrice }).ToList();
            _mapper.Map<TransactionDto>(Arg.Any<Transaction>()).Returns(args => new TransactionDto { TransactionId = ((Transaction)args[0]).TransactionId, PropertyId = ((Transaction)args[0]).PropertyId, BuyerId = ((Transaction)args[0]).BuyerId, SellerId = ((Transaction)args[0]).SellerId, SalePrice = ((Transaction)args[0]).SalePrice });

            var query = new GetAllTransactionsQuery();

            // Act
            var response = await _handler.Handle(query, CancellationToken.None);

            // Assert
            response.IsSuccess.Should().BeTrue();
            response.Data.Should().BeEquivalentTo(transactionDtos);
        }

        [Fact]
        public async Task Handle_ShouldReturnFailureResult_WhenRepositoryReturnsFailure()
        {
            // Arrange
            var errorMessage = "Error occurred";
            var result = Result<IEnumerable<Transaction>>.Failure(errorMessage);
            _repository.GetAllTransactionsAsync().Returns(result);

            var query = new GetAllTransactionsQuery();

            // Act
            var response = await _handler.Handle(query, CancellationToken.None);

            // Assert
            response.IsSuccess.Should().BeFalse();
            response.ErrorMessage.Should().Be(errorMessage);
        }
    }
}
