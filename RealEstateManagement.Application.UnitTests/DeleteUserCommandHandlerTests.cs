using Application.Use_Cases.Users.CommandHandlers;
using Application.Use_Cases.Users.Commands;
using Domain.Common;
using Domain.Repositories;
using FluentAssertions;
using NSubstitute;

namespace RealEstateManagement.Application.UnitTests
{
    public class DeleteUserCommandHandlerTests
    {
        private readonly IUserRepository _repository;
        private readonly DeleteUserCommandHandler _handler;

        public DeleteUserCommandHandlerTests()
        {
            _repository = Substitute.For<IUserRepository>();
            _handler = new DeleteUserCommandHandler(_repository);
        }

        [Fact]
        public async Task Handle_ShouldReturnSuccessResult_WhenRepositoryReturnsSuccess()
        {
            // Arrange
            var userId = Guid.NewGuid();
            var result = Result<Guid>.Success(userId);
            _repository.DeleteUserAsync(userId).Returns(result);

            var command = new DeleteUserCommand { UserId = userId };

            // Act
            var response = await _handler.Handle(command, CancellationToken.None);

            // Assert
            response.IsSuccess.Should().BeTrue();
            response.Data.Should().Be(userId);
        }

        [Fact]
        public async Task Handle_ShouldReturnFailureResult_WhenRepositoryReturnsFailure()
        {
            // Arrange
            var userId = Guid.NewGuid();
            var errorMessage = "Error occurred";
            var result = Result<Guid>.Failure(errorMessage);
            _repository.DeleteUserAsync(userId).Returns(result);

            var command = new DeleteUserCommand { UserId = userId };

            // Act
            var response = await _handler.Handle(command, CancellationToken.None);

            // Assert
            response.IsSuccess.Should().BeFalse();
            response.ErrorMessage.Should().Be(errorMessage);
        }
    }
}

