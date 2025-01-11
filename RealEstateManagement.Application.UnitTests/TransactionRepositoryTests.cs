using Domain.Entities;
using Infrastructure.Persistance;
using Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Tests.Repositories
{
    public class TransactionRepositoryTests
    {
        private readonly ApplicationDbContext _context;
        private readonly TransactionRepository _repository;

        public TransactionRepositoryTests()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;
            _context = new ApplicationDbContext(options);
            _repository = new TransactionRepository(_context);
        }

        [Fact]
        public async Task GetAllTransactionsAsync_ShouldReturnAllTransactions()
        {
            // Arrange
            var transactions = new List<Transaction>
            {
                new Transaction { TransactionId = Guid.NewGuid(), PropertyId = Guid.NewGuid(), BuyerId = Guid.NewGuid(), SellerId = Guid.NewGuid() },
                new Transaction { TransactionId = Guid.NewGuid(), PropertyId = Guid.NewGuid(), BuyerId = Guid.NewGuid(), SellerId = Guid.NewGuid() }
            };
            await _context.Transactions.AddRangeAsync(transactions);
            await _context.SaveChangesAsync();

            // Act
            var result = await _repository.GetAllTransactionsAsync();

            // Assert
            Assert.True(result.IsSuccess);
            Assert.Equal(2, result.Data.Count());
        }

        [Fact]
        public async Task GetTransactionByIdAsync_ShouldReturnTransaction_WhenTransactionExists()
        {
            // Arrange
            var transaction = new Transaction { TransactionId = Guid.NewGuid(), PropertyId = Guid.NewGuid(), BuyerId = Guid.NewGuid(), SellerId = Guid.NewGuid() };
            await _context.Transactions.AddAsync(transaction);
            await _context.SaveChangesAsync();

            // Act
            var result = await _repository.GetTransactionByIdAsync(transaction.TransactionId);

            // Assert
            Assert.True(result.IsSuccess);
            Assert.Equal(transaction.TransactionId, result.Data.TransactionId);
        }

        [Fact]
        public async Task GetTransactionByIdAsync_ShouldReturnFailure_WhenTransactionDoesNotExist()
        {
            // Act
            var result = await _repository.GetTransactionByIdAsync(Guid.NewGuid());

            // Assert
            Assert.False(result.IsSuccess);
            Assert.Equal("Transaction not found.", result.ErrorMessage);
        }

        [Fact]
        public async Task GetTransactionsByBuyerId_ShouldReturnTransactionsForSpecificBuyer()
        {
            // Arrange
            var buyerId = Guid.NewGuid();
            var transactions = new List<Transaction>
            {
                new Transaction { TransactionId = Guid.NewGuid(), PropertyId = Guid.NewGuid(), BuyerId = buyerId, SellerId = Guid.NewGuid() },
                new Transaction { TransactionId = Guid.NewGuid(), PropertyId = Guid.NewGuid(), BuyerId = buyerId, SellerId = Guid.NewGuid() },
                new Transaction { TransactionId = Guid.NewGuid(), PropertyId = Guid.NewGuid(), BuyerId = Guid.NewGuid(), SellerId = Guid.NewGuid() }
            };
            await _context.Transactions.AddRangeAsync(transactions);
            await _context.SaveChangesAsync();

            // Act
            var result = await _repository.GetTransactionsByBuyerId(buyerId);

            // Assert
            Assert.True(result.IsSuccess);
            Assert.Equal(2, result.Data.Count());
        }

        [Fact]
        public async Task GetTransactionsBySellerId_ShouldReturnTransactionsForSpecificSeller()
        {
            // Arrange
            var sellerId = Guid.NewGuid();
            var transactions = new List<Transaction>
            {
                new Transaction { TransactionId = Guid.NewGuid(), PropertyId = Guid.NewGuid(), BuyerId = Guid.NewGuid(), SellerId = sellerId },
                new Transaction { TransactionId = Guid.NewGuid(), PropertyId = Guid.NewGuid(), BuyerId = Guid.NewGuid(), SellerId = sellerId },
                new Transaction { TransactionId = Guid.NewGuid(), PropertyId = Guid.NewGuid(), BuyerId = Guid.NewGuid(), SellerId = Guid.NewGuid() }
            };
            await _context.Transactions.AddRangeAsync(transactions);
            await _context.SaveChangesAsync();

            // Act
            var result = await _repository.GetTransactionsBySellerId(sellerId);

            // Assert
            Assert.True(result.IsSuccess);
            Assert.Equal(2, result.Data.Count());
        }

        [Fact]
        public async Task AddTransactionAsync_ShouldAddTransactionSuccessfully()
        {
            // Arrange
            var transaction = new Transaction { TransactionId = Guid.NewGuid(), PropertyId = Guid.NewGuid(), BuyerId = Guid.NewGuid(), SellerId = Guid.NewGuid() };

            // Act
            var result = await _repository.AddTransactionAsync(transaction);

            // Assert
            Assert.True(result.IsSuccess);
            Assert.Equal(transaction.TransactionId, result.Data);
        }

        [Fact]
        public async Task UpdateTransactionAsync_ShouldUpdateTransactionSuccessfully()
        {
            // Arrange
            var transaction = new Transaction { TransactionId = Guid.NewGuid(), PropertyId = Guid.NewGuid(), BuyerId = Guid.NewGuid(), SellerId = Guid.NewGuid() };
            await _context.Transactions.AddAsync(transaction);
            await _context.SaveChangesAsync();

            transaction.SellerId = Guid.NewGuid();

            // Act
            var result = await _repository.UpdateTransactionAsync(transaction);

            // Assert
            Assert.True(result.IsSuccess);
            Assert.Equal(transaction.TransactionId, result.Data);
            var updatedTransaction = await _context.Transactions.FindAsync(transaction.TransactionId);
            Assert.Equal(transaction.SellerId, updatedTransaction.SellerId);
        }

        [Fact]
        public async Task UpdateTransactionAsync_ShouldReturnFailure_WhenTransactionDoesNotExist()
        {
            // Arrange
            var transaction = new Transaction { TransactionId = Guid.NewGuid(), PropertyId = Guid.NewGuid(), BuyerId = Guid.NewGuid(), SellerId = Guid.NewGuid() };

            // Act
            var result = await _repository.UpdateTransactionAsync(transaction);

            // Assert
            Assert.False(result.IsSuccess);
            Assert.Equal("Transaction not found.", result.ErrorMessage);
        }

        [Fact]
        public async Task DeleteTransactionAsync_ShouldDeleteTransactionSuccessfully()
        {
            // Arrange
            var transaction = new Transaction { TransactionId = Guid.NewGuid(), PropertyId = Guid.NewGuid(), BuyerId = Guid.NewGuid(), SellerId = Guid.NewGuid() };
            await _context.Transactions.AddAsync(transaction);
            await _context.SaveChangesAsync();

            // Act
            var result = await _repository.DeleteTransactionAsync(transaction.TransactionId);

            // Assert
            Assert.True(result.IsSuccess);
            Assert.Null(await _context.Transactions.FindAsync(transaction.TransactionId));
        }

        [Fact]
        public async Task DeleteTransactionAsync_ShouldReturnFailure_WhenTransactionDoesNotExist()
        {
            // Act
            var result = await _repository.DeleteTransactionAsync(Guid.NewGuid());

            // Assert
            Assert.False(result.IsSuccess);
            Assert.Equal("Transaction not found.", result.ErrorMessage);
        }

        [Fact]
        public async Task GetTransactionByPropertyIdAsync_ShouldReturnTransaction_WhenTransactionExistsForPropertyId()
        {
            // Arrange
            var propertyId = Guid.NewGuid();
            var transaction = new Transaction { TransactionId = Guid.NewGuid(), PropertyId = propertyId, BuyerId = Guid.NewGuid(), SellerId = Guid.NewGuid() };
            await _context.Transactions.AddAsync(transaction);
            await _context.SaveChangesAsync();

            // Act
            var result = await _repository.GetTransactionByPropertyIdAsync(propertyId);

            // Assert
            Assert.True(result.IsSuccess);
            Assert.Equal(transaction.TransactionId, result.Data.TransactionId);
        }

        [Fact]
        public async Task GetTransactionByPropertyIdAsync_ShouldReturnFailure_WhenTransactionDoesNotExistForPropertyId()
        {
            // Act
            var result = await _repository.GetTransactionByPropertyIdAsync(Guid.NewGuid());

            // Assert
            Assert.False(result.IsSuccess);
            Assert.Equal("Transaction not found.", result.ErrorMessage);
        }
    }
}