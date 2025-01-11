using Application.Use_Cases.Authentication.Commands;
using Domain.Common;
using Domain.Entities;
using Domain.Repositories;
using FluentAssertions;
using NSubstitute;

namespace RealEstateManagement.Application.UnitTests
{
    public class LoginUserCommandHandlerTests
    {
        private readonly IUserAuthRepository _userRepository;
        private readonly LoginUserCommandHandler _handler;

        public LoginUserCommandHandlerTests()
        {
            _userRepository = Substitute.For<IUserAuthRepository>();
            _handler = new LoginUserCommandHandler(_userRepository);
        }

        [Fact]
        public async Task Handle_Should_Return_Success_Result_When_Login_Is_Successful()
        {
            // Arrange
            var command = new LoginUserCommand
            {
                Email = "test@example.com",
                Password = "password123"
            };
            var user = new User
            {
                Email = command.Email,
                PasswordHash = command.Password
            };
            var loginResult = Result<string>.Success("token123");

            _userRepository.Login(Arg.Is<User>(u => u.Email == user.Email && u.PasswordHash == user.PasswordHash))
                           .Returns(loginResult);

            // Act
            var result = await _handler.Handle(command, CancellationToken.None);

            // Assert
            result.IsSuccess.Should().BeTrue();
            result.Data.Should().Be("token123");
            await _userRepository.Received(1).Login(Arg.Is<User>(u => u.Email == user.Email && u.PasswordHash == user.PasswordHash));
        }

        [Fact]
        public async Task Handle_Should_Return_Failure_Result_When_Login_Fails()
        {
            // Arrange
            var command = new LoginUserCommand
            {
                Email = "test@example.com",
                Password = "password123"
            };
            var user = new User
            {
                Email = command.Email,
                PasswordHash = command.Password
            };
            var loginResult = Result<string>.Failure("Invalid credentials");

            _userRepository.Login(Arg.Is<User>(u => u.Email == user.Email && u.PasswordHash == user.PasswordHash))
                           .Returns(loginResult);

            // Act
            var result = await _handler.Handle(command, CancellationToken.None);

            // Assert
            result.IsSuccess.Should().BeFalse();
            result.ErrorMessage.Should().Be("Invalid credentials");
            await _userRepository.Received(1).Login(Arg.Is<User>(u => u.Email == user.Email && u.PasswordHash == user.PasswordHash));
        }
    }
}
