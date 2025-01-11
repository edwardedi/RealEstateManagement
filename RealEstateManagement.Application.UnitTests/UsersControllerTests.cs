using Application.DTOs;
using Application.Use_Cases.Users.Commands;
using Application.Use_Cases.Users.Queries;
using Application.Utils;
using Domain.Common;
using FluentAssertions;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using NSubstitute;
using RealEstateManagement.Controllers;
using Xunit;

namespace RealEstateManagement.Application.UnitTests
{
    public class UsersControllerTests
    {
        private readonly IMediator _mediator;
        private readonly UsersController _controller;

        public UsersControllerTests()
        {
            _mediator = Substitute.For<IMediator>();
            _controller = new UsersController(_mediator);
        }


        [Fact]
        public async Task GetAllUsers_ShouldReturnOkResult_WhenUsersAreFound()
        {
            // Arrange
            var users = new List<UserDto> { new UserDto { UserId = Guid.NewGuid() } };
            var result = Result<List<UserDto>>.Success(users);
            _mediator.Send(Arg.Any<GetAllUsersQuery>()).Returns(result);

            // Act
            var response = await _controller.GetAllUsers();

            // Assert
            response.Result.Should().BeOfType<OkObjectResult>();
        }

        [Fact]
        public async Task GetUserById_ShouldReturnOkResult_WhenUserIsFound()
        {
            // Arrange
            var userId = Guid.NewGuid();
            var user = new UserDto { UserId = userId };
            var result = Result<UserDto>.Success(user);
            _mediator.Send(Arg.Any<GetUserByIdQuery>()).Returns(result);

            // Act
            var response = await _controller.GetUserById(userId);

            // Assert
            response.Result.Should().BeOfType<OkObjectResult>();
        }

        [Fact]
        public async Task GetUserById_ShouldReturnNotFound_WhenUserIsNotFound()
        {
            // Arrange
            var userId = Guid.NewGuid();
            var result = Result<UserDto>.Success(null);
            _mediator.Send(Arg.Any<GetUserByIdQuery>()).Returns(result);

            // Act
            var response = await _controller.GetUserById(userId);

            // Assert
            response.Result.Should().BeOfType<NotFoundObjectResult>();
        }
    }
}

