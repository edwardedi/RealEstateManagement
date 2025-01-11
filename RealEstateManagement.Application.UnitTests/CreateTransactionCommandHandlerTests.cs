using Application.Use_Cases.Transactions.CommandHandlers;
using Application.Use_Cases.Transactions.Commands;
using AutoMapper;
using Domain.Common;
using Domain.Entities;
using Domain.Repositories;
using FluentAssertions;
using NSubstitute;

namespace RealEstateManagement.Application.UnitTests
{
    public class CreateTransactionCommandHandlerTests
    {
        private readonly ITransactionRepository _repository;
        private readonly IMapper _mapper;
        private readonly CreateTransactionCommandHandler _handler;

        public CreateTransactionCommandHandlerTests()
        {
            _repository = Substitute.For<ITransactionRepository>();
            _mapper = Substitute.For<IMapper>();
            _handler = new CreateTransactionCommandHandler(_repository, _mapper);
        }

        [Fact]
        public async Task Handle_ShouldReturnSuccessResult_WhenRepositoryReturnsSuccess()
        {
            // Arrange
            var command = new CreateTransactionCommand
            {
                PropertyId = Guid.NewGuid(),
                BuyerId = Guid.NewGuid(),
                SellerId = Guid.NewGuid(),
                SalePrice = 250000
            };
            var transaction = new Transaction
            {
                TransactionId = Guid.NewGuid(),
                PropertyId = command.PropertyId,
                BuyerId = command.BuyerId,
                SellerId = command.SellerId,
                SalePrice = command.SalePrice
            };
            _mapper.Map<Transaction>(command).Returns(transaction);

            var result = Result<Guid>.Success(transaction.TransactionId);
            _repository.AddTransactionAsync(transaction).Returns(result);

            // Act
            var response = await _handler.Handle(command, CancellationToken.None);

            // Assert
            response.IsSuccess.Should().BeTrue();
            response.Data.Should().Be(transaction.TransactionId);
        }

        [Fact]
        public async Task Handle_ShouldReturnFailureResult_WhenRepositoryReturnsFailure()
        {
            // Arrange
            var command = new CreateTransactionCommand
            {
                PropertyId = Guid.NewGuid(),
                BuyerId = Guid.NewGuid(),
                SellerId = Guid.NewGuid(),
                SalePrice = 250000
            };
            var transaction = new Transaction
            {
                TransactionId = Guid.NewGuid(),
                PropertyId = command.PropertyId,
                BuyerId = command.BuyerId,
                SellerId = command.SellerId,
                SalePrice = command.SalePrice
            };
            _mapper.Map<Transaction>(command).Returns(transaction);

            var errorMessage = "Error occurred";
            var result = Result<Guid>.Failure(errorMessage);
            _repository.AddTransactionAsync(transaction).Returns(result);

            // Act
            var response = await _handler.Handle(command, CancellationToken.None);

            // Assert
            response.IsSuccess.Should().BeFalse();
            response.ErrorMessage.Should().Be(errorMessage);
        }
    }
}
