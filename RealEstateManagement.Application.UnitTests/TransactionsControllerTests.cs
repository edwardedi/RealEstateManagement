using Application.DTOs;
using Application.Use_Cases.Transactions.Queries;
using Application.Utils;
using AutoMapper;
using Domain.Common;
using Domain.Repositories;
using FluentAssertions;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using NSubstitute;
using RealEstateManagement.Application.Transactions.Commands;
using RealEstateManagement.Controllers;

namespace RealEstateManagement.Application.UnitTests
{
    public class TransactionsControllerTests
    {
        private readonly IMediator _mediator;
        private readonly IPropertyListingRepository _propertyListingRepository;
        private readonly IMapper _mapper;
        private readonly TransactionsController _controller;

        public TransactionsControllerTests()
        {
            _mediator = Substitute.For<IMediator>();
            _propertyListingRepository = Substitute.For<IPropertyListingRepository>();
            _mapper = Substitute.For<IMapper>();
            _controller = new TransactionsController(_mediator, _propertyListingRepository, _mapper);
        }

        [Fact]
        public async Task UpdateTransaction_ShouldReturnNoContent_WhenUpdateIsSuccessful()
        {
            // Arrange
            var command = new UpdateTransactionCommand
            {
                TransactionId = Guid.NewGuid(),
                PropertyId = Guid.NewGuid(),
                BuyerId = Guid.NewGuid(),
                SellerId = Guid.NewGuid(),
                SalePrice = 300000
            };
            _mediator.Send(command).Returns(Result<Guid>.Success(command.TransactionId));

            // Act
            var response = await _controller.UpdateTransaction(command.TransactionId, command);

            // Assert
            response.Should().BeOfType<NoContentResult>();
        }

        [Fact]
        public async Task UpdateTransaction_ShouldReturnBadRequest_WhenIdsDoNotMatch()
        {
            // Arrange
            var command = new UpdateTransactionCommand
            {
                TransactionId = Guid.NewGuid(),
                PropertyId = Guid.NewGuid(),
                BuyerId = Guid.NewGuid(),
                SellerId = Guid.NewGuid(),
                SalePrice = 300000
            };

            // Act
            var response = await _controller.UpdateTransaction(Guid.NewGuid(), command);

            // Assert
            response.Should().BeOfType<BadRequestObjectResult>();
        }

        [Fact]
        public async Task DeleteTransaction_ShouldReturnNoContent_WhenDeleteIsSuccessful()
        {
            // Arrange
            var transactionId = Guid.NewGuid();
            _mediator.Send(Arg.Any<DeleteTransactionCommand>()).Returns(Result<Guid>.Success(transactionId));

            // Act
            var response = await _controller.DeleteTransaction(transactionId);

            // Assert
            response.Should().BeOfType<NoContentResult>();
        }

        [Fact]
        public async Task DeleteTransaction_ShouldReturnBadRequest_WhenDeleteFails()
        {
            // Arrange
            var transactionId = Guid.NewGuid();
            var errorMessage = "Error occurred";
            _mediator.Send(Arg.Any<DeleteTransactionCommand>()).Returns(Result<Guid>.Failure(errorMessage));

            // Act
            var response = await _controller.DeleteTransaction(transactionId);

            // Assert
            response.Should().BeOfType<BadRequestObjectResult>();
        }

        [Fact]
        public async Task GetAllTransactions_ShouldReturnOkResult_WhenTransactionsAreFound()
        {
            // Arrange
            var transactions = new List<TransactionDto> { new TransactionDto { TransactionId = Guid.NewGuid() } };
            var result = Result<List<TransactionDto>>.Success(transactions);
            _mediator.Send(Arg.Any<GetAllTransactionsQuery>()).Returns(result);

            // Act
            var response = await _controller.GetAllTransactions();

            // Assert
            response.Result.Should().BeOfType<OkObjectResult>();
        }

        [Fact]
        public async Task GetTransactionById_ShouldReturnOkResult_WhenTransactionIsFound()
        {
            // Arrange
            var transactionId = Guid.NewGuid();
            var transaction = new TransactionDto { TransactionId = transactionId };
            var result = Result<TransactionDto>.Success(transaction);
            _mediator.Send(Arg.Any<GetTransactionByIdQuery>()).Returns(result);

            // Act
            var response = await _controller.GetTransactionById(transactionId);

            // Assert
            response.Result.Should().BeOfType<OkObjectResult>();
        }

        [Fact]
        public async Task GetTransactionById_ShouldReturnNotFound_WhenTransactionIsNotFound()
        {
            // Arrange
            var transactionId = Guid.NewGuid();
            var result = Result<TransactionDto>.Success(null);
            _mediator.Send(Arg.Any<GetTransactionByIdQuery>()).Returns(result);

            // Act
            var response = await _controller.GetTransactionById(transactionId);

            // Assert
            response.Result.Should().BeOfType<NotFoundObjectResult>();
        }

        [Fact]
        public async Task GetTransactionByPropertyId_ShouldReturnOkResult_WhenTransactionIsFound()
        {
            // Arrange
            var propertyId = Guid.NewGuid();
            var transaction = new TransactionDto { TransactionId = Guid.NewGuid() };
            var result = Result<TransactionDto>.Success(transaction);
            _mediator.Send(Arg.Any<GetTransactionByPropertyIdQuery>()).Returns(result);

            // Act
            var response = await _controller.GetTransactionByPropertyId(propertyId);

            // Assert
            response.Result.Should().BeOfType<OkObjectResult>();
        }

        [Fact]
        public async Task GetTransactionByPropertyId_ShouldReturnNotFound_WhenTransactionIsNotFound()
        {
            // Arrange
            var propertyId = Guid.NewGuid();
            var result = Result<TransactionDto>.Success(null);
            _mediator.Send(Arg.Any<GetTransactionByPropertyIdQuery>()).Returns(result);

            // Act
            var response = await _controller.GetTransactionByPropertyId(propertyId);

            // Assert
            response.Result.Should().BeOfType<NotFoundObjectResult>();
        }

        [Fact]
        public async Task GetTransactionsByBuyerId_ShouldReturnOkResult_WhenTransactionsAreFound()
        {
            // Arrange
            var buyerId = Guid.NewGuid();
            var transactions = new PagedResult<TransactionDto>(new List<TransactionDto> { new TransactionDto { TransactionId = Guid.NewGuid() } }, 1);
            var result = Result<PagedResult<TransactionDto>>.Success(transactions);
            _mediator.Send(Arg.Any<GetTransactionsByBuyerIdQuery>()).Returns(result);

            // Act
            var response = await _controller.GetTransactionsByBuyerId(buyerId);

            // Assert
            response.Result.Should().BeOfType<OkObjectResult>();
        }

        [Fact]
        public async Task GetTransactionsBySellerId_ShouldReturnOkResult_WhenTransactionsAreFound()
        {
            // Arrange
            var sellerId = Guid.NewGuid();
            var transactions = new PagedResult<TransactionDto>(new List<TransactionDto> { new TransactionDto { TransactionId = Guid.NewGuid() } }, 1);
            var result = Result<PagedResult<TransactionDto>>.Success(transactions);
            _mediator.Send(Arg.Any<GetTransactionsBySellerIdQuery>()).Returns(result);

            // Act
            var response = await _controller.GetTransactionsBySellerId(sellerId);

            // Assert
            response.Result.Should().BeOfType<OkObjectResult>();
        }
    }
}
