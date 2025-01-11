using Application.Use_Cases.Authentication.Commands;
using Domain.Common;
using Domain.Entities;
using Domain.Repositories;
using FluentAssertions;
using NSubstitute;

namespace RealEstateManagement.Application.UnitTests
{
    public class RegisterUserCommandHandlerTests
    {
        private readonly IUserAuthRepository _userRepository;
        private readonly RegisterUserCommandHandler _handler;

        public RegisterUserCommandHandlerTests()
        {
            _userRepository = Substitute.For<IUserAuthRepository>();
            _handler = new RegisterUserCommandHandler(_userRepository);
        }

        [Fact]
        public async Task Handle_Should_Return_Success_Result_When_Registration_Is_Successful()
        {
            // Arrange
            var command = new RegisterUserCommand
            {
                Email = "test@example.com",
                Name = "Test User",
                PhoneNumber = "1234567890",
                Password = "password123"
            };
            var user = new User
            {
                Email = command.Email,
                Name = command.Name,
                PhoneNumber = command.PhoneNumber,
                PasswordHash = BCrypt.Net.BCrypt.HashPassword(command.Password)
            };
            var registrationResult = Result<Guid>.Success(Guid.NewGuid());

            _userRepository.Register(Arg.Is<User>(u => u.Email == user.Email && u.Name == user.Name && u.PhoneNumber == user.PhoneNumber), Arg.Any<CancellationToken>())
                           .Returns(registrationResult);

            // Act
            var result = await _handler.Handle(command, CancellationToken.None);

            // Assert
            result.IsSuccess.Should().BeTrue();
            result.Data.Should().Be(registrationResult.Data);
            await _userRepository.Received(1).Register(Arg.Is<User>(u => u.Email == user.Email && u.Name == user.Name && u.PhoneNumber == user.PhoneNumber), Arg.Any<CancellationToken>());
        }

        [Fact]
        public async Task Handle_Should_Return_Failure_Result_When_Registration_Fails()
        {
            // Arrange
            var command = new RegisterUserCommand
            {
                Email = "test@example.com",
                Name = "Test User",
                PhoneNumber = "1234567890",
                Password = "password123"
            };
            var user = new User
            {
                Email = command.Email,
                Name = command.Name,
                PhoneNumber = command.PhoneNumber,
                PasswordHash = BCrypt.Net.BCrypt.HashPassword(command.Password)
            };
            var registrationResult = Result<Guid>.Failure("Registration failed");

            _userRepository.Register(Arg.Is<User>(u => u.Email == user.Email && u.Name == user.Name && u.PhoneNumber == user.PhoneNumber), Arg.Any<CancellationToken>())
                           .Returns(registrationResult);

            // Act
            var result = await _handler.Handle(command, CancellationToken.None);

            // Assert
            result.IsSuccess.Should().BeFalse();
            result.ErrorMessage.Should().Be("Registration failed");
            await _userRepository.Received(1).Register(Arg.Is<User>(u => u.Email == user.Email && u.Name == user.Name && u.PhoneNumber == user.PhoneNumber), Arg.Any<CancellationToken>());
        }
    }
}

