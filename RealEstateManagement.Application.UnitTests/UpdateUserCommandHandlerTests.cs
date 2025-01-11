using Application.Use_Cases.Users.Commands;
using Application.Use_Cases.Users.CommandHandlers;
using AutoMapper;
using Domain.Common;
using Domain.Entities;
using Domain.Repositories;
using FluentAssertions;
using NSubstitute;

namespace RealEstateManagement.Application.UnitTests
{
    public class UpdateUserCommandHandlerTests
    {
        private readonly IUserRepository _repository;
        private readonly IMapper _mapper;
        private readonly UpdateUserCommandHandler _handler;

        public UpdateUserCommandHandlerTests()
        {
            _repository = Substitute.For<IUserRepository>();
            _mapper = Substitute.For<IMapper>();
            _handler = new UpdateUserCommandHandler(_repository, _mapper);
        }

        [Fact]
        public async Task Handle_ShouldReturnSuccessResult_WhenRepositoryReturnsSuccess()
        {
            // Arrange
            var command = new UpdateUserCommand
            {
                UserId = Guid.NewGuid(),
                Name = "John Doe",
                Email = "john.doe@example.com",
                PhoneNumber = "1234567890",
                Password = "newpassword"
            };
            var user = new User
            {
                UserId = command.UserId,
                Name = command.Name,
                Email = command.Email,
                PhoneNumber = command.PhoneNumber,
                PasswordHash = BCrypt.Net.BCrypt.HashPassword(command.Password)
            };
            _mapper.Map<User>(command).Returns(user);

            var result = Result<Guid>.Success(user.UserId);
            _repository.UpdateUserAsync(user).Returns(result);

            // Act
            var response = await _handler.Handle(command, CancellationToken.None);

            // Assert
            response.IsSuccess.Should().BeTrue();
            response.Data.Should().Be(user.UserId);
        }

        [Fact]
        public async Task Handle_ShouldReturnFailureResult_WhenRepositoryReturnsFailure()
        {
            // Arrange
            var command = new UpdateUserCommand
            {
                UserId = Guid.NewGuid(),
                Name = "John Doe",
                Email = "john.doe@example.com",
                PhoneNumber = "1234567890",
                Password = "newpassword"
            };
            var user = new User
            {
                UserId = command.UserId,
                Name = command.Name,
                Email = command.Email,
                PhoneNumber = command.PhoneNumber,
                PasswordHash = BCrypt.Net.BCrypt.HashPassword(command.Password)
            };
            _mapper.Map<User>(command).Returns(user);

            var errorMessage = "Error occurred";
            var result = Result<Guid>.Failure(errorMessage);
            _repository.UpdateUserAsync(user).Returns(result);

            // Act
            var response = await _handler.Handle(command, CancellationToken.None);

            // Assert
            response.IsSuccess.Should().BeFalse();
            response.ErrorMessage.Should().Be(errorMessage);
        }
    }
}


