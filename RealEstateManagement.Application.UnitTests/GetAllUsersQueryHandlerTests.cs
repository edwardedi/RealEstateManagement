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
    public class GetAllUsersQueryHandlerTests
    {
        private readonly IUserRepository _repository;
        private readonly IMapper _mapper;
        private readonly GetAllUsersQueryHandler _handler;

        public GetAllUsersQueryHandlerTests()
        {
            _repository = Substitute.For<IUserRepository>();
            _mapper = Substitute.For<IMapper>();
            _handler = new GetAllUsersQueryHandler(_repository, _mapper);
        }

        [Fact]
        public async Task Handle_ShouldReturnSuccessResult_WhenRepositoryReturnsSuccess()
        {
            // Arrange
            var users = new List<User>
            {
                new User { UserId = Guid.NewGuid(), Name = "John Doe", Email = "john.doe@example.com", PhoneNumber = "1234567890" },
                new User { UserId = Guid.NewGuid(), Name = "Jane Smith", Email = "jane.smith@example.com", PhoneNumber = "0987654321" }
            };
            var result = Result<IEnumerable<User>>.Success(users);
            _repository.GetAllUsersAsync().Returns(result);

            var userDtos = users.Select(u => new UserDto { UserId = u.UserId, Name = u.Name, Email = u.Email, PhoneNumber = u.PhoneNumber }).ToList();
            _mapper.Map<UserDto>(Arg.Any<User>()).Returns(args => new UserDto { UserId = ((User)args[0]).UserId, Name = ((User)args[0]).Name, Email = ((User)args[0]).Email, PhoneNumber = ((User)args[0]).PhoneNumber });

            var query = new GetAllUsersQuery();

            // Act
            var response = await _handler.Handle(query, CancellationToken.None);

            // Assert
            response.IsSuccess.Should().BeTrue();
            response.Data.Should().BeEquivalentTo(userDtos);
        }

        [Fact]
        public async Task Handle_ShouldReturnFailureResult_WhenRepositoryReturnsFailure()
        {
            // Arrange
            var errorMessage = "Error occurred";
            var result = Result<IEnumerable<User>>.Failure(errorMessage);
            _repository.GetAllUsersAsync().Returns(result);

            var query = new GetAllUsersQuery();

            // Act
            var response = await _handler.Handle(query, CancellationToken.None);

            // Assert
            response.IsSuccess.Should().BeFalse();
            response.ErrorMessage.Should().Be(errorMessage);
        }
    }
}
