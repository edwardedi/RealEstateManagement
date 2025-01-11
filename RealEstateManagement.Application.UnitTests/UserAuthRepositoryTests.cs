using Domain.Entities;
using Identity.Repositories;
using Infrastructure.Persistance;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace Infrastructure.Tests.Repositories
{
    public class UserAuthRepositoryTests
    {
        private readonly ApplicationDbContext _context;
        private readonly UserAuthRepository _repository;

        public UserAuthRepositoryTests()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;
            _context = new ApplicationDbContext(options);

            var configuration = new ConfigurationBuilder()
                .AddInMemoryCollection(new Dictionary<string, string?>
                {
                    { "Jwt:Key", "ThisIsASecretKeyForJwtThatIsLongEnoughToMeetThe256BitRequirement" }
                })
                .Build();


            _repository = new UserAuthRepository(_context, configuration);
        }

        [Fact]
        public async Task Register_ShouldReturnSuccess_WhenEmailIsUnique()
        {
            // Arrange
            var user = new User
            {
                UserId = Guid.NewGuid(),
                Name = "John Doe",
                Email = "john.doe@example.com",
                PhoneNumber = "1234567890",
                PasswordHash = BCrypt.Net.BCrypt.HashPassword("password123")
            };

            // Act
            var result = await _repository.Register(user, CancellationToken.None);

            // Assert
            Assert.True(result.IsSuccess);
            Assert.Equal(user.UserId, result.Data);
        }

        [Fact]
        public async Task Register_ShouldReturnFailure_WhenEmailAlreadyExists()
        {
            // Arrange
            var user = new User
            {
                UserId = Guid.NewGuid(),
                Name = "John Doe",
                Email = "john.doe@example.com",
                PhoneNumber = "1234567890",
                PasswordHash = BCrypt.Net.BCrypt.HashPassword("password123")
            };

            // Adding the user to the context
            await _context.Users.AddAsync(user);
            await _context.SaveChangesAsync();

            // Act
            var result = await _repository.Register(user, CancellationToken.None);

            // Assert
            Assert.False(result.IsSuccess);
            Assert.Equal("Email already in use", result.ErrorMessage);
        }

        [Fact]
        public async Task Login_ShouldReturnSuccess_WhenCredentialsAreValid()
        {
            // Arrange
            var user = new User
            {
                UserId = Guid.NewGuid(),
                Name = "John Doe",
                Email = "john.doe@example.com",
                PhoneNumber = "1234567890",
                PasswordHash = BCrypt.Net.BCrypt.HashPassword("password123")
            };

            // Adding the user to the context
            await _context.Users.AddAsync(user);
            await _context.SaveChangesAsync();

            var loginUser = new User
            {
                Email = "john.doe@example.com",
                PasswordHash = "password123" // Password will be hashed inside Login method
            };

            // Act
            var result = await _repository.Login(loginUser);

            // Assert
            Assert.True(result.IsSuccess);
            Assert.NotNull(result.Data);
        }

        [Fact]
        public async Task Login_ShouldReturnFailure_WhenCredentialsAreInvalid()
        {
            // Arrange
            var user = new User
            {
                UserId = Guid.NewGuid(),
                Name = "John Doe",
                Email = "john.doe@example.com",
                PhoneNumber = "1234567890",
                PasswordHash = BCrypt.Net.BCrypt.HashPassword("password123")
            };

            // Adding the user to the context
            await _context.Users.AddAsync(user);
            await _context.SaveChangesAsync();

            var loginUser = new User
            {
                Email = "john.doe@example.com",
                PasswordHash = "wrongpassword" // Incorrect password
            };

            // Act
            var result = await _repository.Login(loginUser);

            // Assert
            Assert.False(result.IsSuccess);
            Assert.Equal("Invalid email or password", result.ErrorMessage);
        }

        [Fact]
        public async Task Login_ShouldReturnFailure_WhenEmailDoesNotExist()
        {
            // Arrange
            var loginUser = new User
            {
                Email = "nonexistent.user@example.com",
                PasswordHash = "password123"
            };

            // Act
            var result = await _repository.Login(loginUser);

            // Assert
            Assert.False(result.IsSuccess);
            Assert.Equal("Invalid email or password", result.ErrorMessage);
        }
    }
}
