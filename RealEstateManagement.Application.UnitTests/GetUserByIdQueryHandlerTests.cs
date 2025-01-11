using Application.DTOs;
using Application.Use_Cases.Users.Queries;
using Application.Use_Cases.Users.QueryHandlers;
using AutoMapper;
using Domain.Common;
using Domain.Entities;
using Domain.Repositories;
using FluentAssertions;
using NSubstitute;

namespace RealEstateManagement.Application.UnitTests
{
    public class GetUserByIdQueryHandlerTests
    {
        private readonly IUserRepository _repository;
        private readonly IMapper _mapper;
        private readonly GetUserByIdQueryHandler _handler;

        public GetUserByIdQueryHandlerTests()
        {
            _repository = Substitute.For<IUserRepository>();
            _mapper = Substitute.For<IMapper>();
            _handler = new GetUserByIdQueryHandler(_repository, _mapper);
        }

        [Fact]
        public async Task Handle_ShouldReturnSuccessResult_WhenRepositoryReturnsSuccess()
        {
            // Arrange
            var userId = Guid.NewGuid();
            var user = new User
            {
                UserId = userId,
                Name = "John Doe",
                Email = "john.doe@example.com",
                PhoneNumber = "1234567890"
            };
            var result = Result<User>.Success(user);
            _repository.GetUserByIdAsync(userId).Returns(result);

            var userDto = new UserDto
            {
                UserId = user.UserId,
                Name = user.Name,
                Email = user.Email,
                PhoneNumber = user.PhoneNumber
            };
            _mapper.Map<UserDto>(user).Returns(userDto);

            var query = new GetUserByIdQuery { UserId = userId };

            // Act
            var response = await _handler.Handle(query, CancellationToken.None);

            // Assert
            response.IsSuccess.Should().BeTrue();
            response.Data.Should().BeEquivalentTo(userDto);
        }

        [Fact]
        public async Task Handle_ShouldReturnFailureResult_WhenRepositoryReturnsFailure()
        {
            // Arrange
            var userId = Guid.NewGuid();
            var errorMessage = "User with ID not found.";
            var result = Result<User>.Failure(errorMessage);
            _repository.GetUserByIdAsync(userId).Returns(result);

            var query = new GetUserByIdQuery { UserId = userId };

            // Act
            var response = await _handler.Handle(query, CancellationToken.None);

            // Assert
            response.IsSuccess.Should().BeFalse();
            response.ErrorMessage.Should().Be($"User with ID {userId} not found.");
        }
    }
}
