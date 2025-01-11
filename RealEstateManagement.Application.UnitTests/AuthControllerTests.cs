using Application.Use_Cases.Authentication.Commands;
using Domain.Common;
using FluentAssertions;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using NSubstitute;
using RealEstateManagement.Controllers;

namespace RealEstateManagement.Application.UnitTests
{
    public class AuthControllerTests
    {
        private readonly IMediator _mediator;
        private readonly AuthController _controller;

        public AuthControllerTests()
        {
            _mediator = Substitute.For<IMediator>();
            _controller = new AuthController(_mediator);
        }

        [Fact]
        public async Task Register_ShouldReturnOkResult_WhenRegistrationIsSuccessful()
        {
            // Arrange
            var userId = Guid.NewGuid();
            var command = new RegisterUserCommand
            {
                Email = "test@example.com",
                Name = "Test User",
                PhoneNumber = "1234567890",
                Password = "SecurePassword123"
            };
            var result = Result<Guid>.Success(userId);
            _mediator.Send(command).Returns(result);

            // Act
            var response = await _controller.Register(command);

            // Assert
            response.Result.Should().BeOfType<OkObjectResult>();
            var okResult = response.Result as OkObjectResult;
            okResult.Value.Should().BeEquivalentTo(new { UserId = userId });
        }

        [Fact]
        public async Task Register_ShouldReturnBadRequest_WhenRegistrationFails()
        {
            // Arrange
            var errorMessage = "Registration failed.";
            var command = new RegisterUserCommand
            {
                Email = "test@example.com",
                Name = "Test User",
                PhoneNumber = "1234567890",
                Password = "SecurePassword123"
            };
            var result = Result<Guid>.Failure(errorMessage);
            _mediator.Send(command).Returns(result);

            // Act
            var response = await _controller.Register(command);

            // Assert
            response.Result.Should().BeOfType<BadRequestObjectResult>();
            var badRequestResult = response.Result as BadRequestObjectResult;
            badRequestResult.Value.Should().Be(errorMessage);
        }

        [Fact]
        public async Task Login_ShouldReturnOkResult_WhenLoginIsSuccessful()
        {
            // Arrange
            var token = "valid.jwt.token";
            var command = new LoginUserCommand
            {
                Email = "test@example.com",
                Password = "SecurePassword123"
            };
            var result = Result<string>.Success(token);
            _mediator.Send(command).Returns(result);

            // Act
            var response = await _controller.Login(command);

            // Assert
            response.Result.Should().BeOfType<OkObjectResult>();
            var okResult = response.Result as OkObjectResult;
            okResult.Value.Should().BeEquivalentTo(new { Token = token });
        }

        [Fact]
        public async Task Login_ShouldReturnBadRequest_WhenLoginFails()
        {
            // Arrange
            var errorMessage = "Invalid credentials.";
            var command = new LoginUserCommand
            {
                Email = "test@example.com",
                Password = "WrongPassword"
            };
            var result = Result<string>.Failure(errorMessage);
            _mediator.Send(command).Returns(result);

            // Act
            var response = await _controller.Login(command);

            // Assert
            response.Result.Should().BeOfType<BadRequestObjectResult>();
            var badRequestResult = response.Result as BadRequestObjectResult;
            badRequestResult.Value.Should().Be(errorMessage);
        }
    }
}
