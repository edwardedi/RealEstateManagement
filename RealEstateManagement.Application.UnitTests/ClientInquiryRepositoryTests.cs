using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Common;
using Domain.Entities;
using Infrastructure.Persistance;
using Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using NSubstitute;
using Xunit;

namespace Infrastructure.Tests.Repositories
{
    public class ClientInquiryRepositoryTests
    {
        private readonly ApplicationDbContext _context;
        private readonly ClientInquiryRepository _repository;

        public ClientInquiryRepositoryTests()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;
            _context = new ApplicationDbContext(options);
            _repository = new ClientInquiryRepository(_context);
        }

        [Fact]
        public async Task GetAllInquiriesAsync_ShouldReturnAllInquiries()
        {
            // Arrange
            var inquiries = new List<ClientInquiry>
            {
                new ClientInquiry { InquiryId = Guid.NewGuid(), ClientId = Guid.NewGuid(), MinPrice = 100000, MaxPrice = 200000 },
                new ClientInquiry { InquiryId = Guid.NewGuid(), ClientId = Guid.NewGuid(), MinPrice = 150000, MaxPrice = 250000 }
            };
            await _context.ClientInquiries.AddRangeAsync(inquiries);
            await _context.SaveChangesAsync();

            // Act
            var result = await _repository.GetAllInquiriesAsync();

            // Assert
            Assert.True(result.IsSuccess);
            Assert.Equal(2, result.Data.Count());
        }

        [Fact]
        public async Task GetInquiryByIdAsync_ShouldReturnInquiry_WhenInquiryExists()
        {
            // Arrange
            var inquiry = new ClientInquiry { InquiryId = Guid.NewGuid(), ClientId = Guid.NewGuid(), MinPrice = 100000, MaxPrice = 200000 };
            await _context.ClientInquiries.AddAsync(inquiry);
            await _context.SaveChangesAsync();

            // Act
            var result = await _repository.GetInquiryByIdAsync(inquiry.InquiryId);

            // Assert
            Assert.True(result.IsSuccess);
            Assert.Equal(inquiry.InquiryId, result.Data.InquiryId);
        }

        [Fact]
        public async Task GetInquiryByIdAsync_ShouldReturnFailure_WhenInquiryDoesNotExist()
        {
            // Act
            var result = await _repository.GetInquiryByIdAsync(Guid.NewGuid());

            // Assert
            Assert.False(result.IsSuccess);
            Assert.Equal("Inquiry not found.", result.ErrorMessage);
        }

        [Fact]
        public async Task GetInquiriesByClientId_ShouldReturnInquiriesForSpecificClient()
        {
            // Arrange
            var clientId = Guid.NewGuid();
            var inquiries = new List<ClientInquiry>
            {
                new ClientInquiry { InquiryId = Guid.NewGuid(), ClientId = clientId, MinPrice = 100000, MaxPrice = 200000 },
                new ClientInquiry { InquiryId = Guid.NewGuid(), ClientId = clientId, MinPrice = 150000, MaxPrice = 250000 },
                new ClientInquiry { InquiryId = Guid.NewGuid(), ClientId = Guid.NewGuid(), MinPrice = 200000, MaxPrice = 300000 }
            };
            await _context.ClientInquiries.AddRangeAsync(inquiries);
            await _context.SaveChangesAsync();

            // Act
            var result = await _repository.GetInquiriesByClientId(clientId);

            // Assert
            Assert.True(result.IsSuccess);
            Assert.Equal(2, result.Data.Count());
        }

        [Fact]
        public async Task AddInquiryAsync_ShouldAddInquirySuccessfully()
        {
            // Arrange
            var inquiry = new ClientInquiry { InquiryId = Guid.NewGuid(), ClientId = Guid.NewGuid(), MinPrice = 100000, MaxPrice = 200000 };

            // Act
            var result = await _repository.AddInquiryAsync(inquiry);

            // Assert
            Assert.True(result.IsSuccess);
            Assert.Equal(inquiry.InquiryId, result.Data);
        }

        [Fact]
        public async Task UpdateInquiryAsync_ShouldUpdateInquirySuccessfully()
        {
            // Arrange
            var inquiry = new ClientInquiry { InquiryId = Guid.NewGuid(), ClientId = Guid.NewGuid(), MinPrice = 100000, MaxPrice = 200000 };
            await _context.ClientInquiries.AddAsync(inquiry);
            await _context.SaveChangesAsync();

            inquiry.MinPrice = 120000;

            // Act
            var result = await _repository.UpdateInquiryAsync(inquiry);

            // Assert
            Assert.True(result.IsSuccess);
            Assert.Equal(inquiry.InquiryId, result.Data);
            var updatedInquiry = await _context.ClientInquiries.FindAsync(inquiry.InquiryId);
            Assert.Equal(120000, updatedInquiry.MinPrice);
        }

        [Fact]
        public async Task UpdateInquiryAsync_ShouldReturnFailure_WhenInquiryDoesNotExist()
        {
            // Arrange
            var inquiry = new ClientInquiry { InquiryId = Guid.NewGuid(), ClientId = Guid.NewGuid(), MinPrice = 100000, MaxPrice = 200000 };

            // Act
            var result = await _repository.UpdateInquiryAsync(inquiry);

            // Assert
            Assert.False(result.IsSuccess);
            Assert.Equal("Inquiry not found.", result.ErrorMessage);
        }

        [Fact]
        public async Task DeleteInquiryAsync_ShouldDeleteInquirySuccessfully()
        {
            // Arrange
            var inquiry = new ClientInquiry { InquiryId = Guid.NewGuid(), ClientId = Guid.NewGuid(), MinPrice = 100000, MaxPrice = 200000 };
            await _context.ClientInquiries.AddAsync(inquiry);
            await _context.SaveChangesAsync();

            // Act
            var result = await _repository.DeleteInquiryAsync(inquiry.InquiryId);

            // Assert
            Assert.True(result.IsSuccess);
            Assert.Null(await _context.ClientInquiries.FindAsync(inquiry.InquiryId));
        }

        [Fact]
        public async Task DeleteInquiryAsync_ShouldReturnFailure_WhenInquiryDoesNotExist()
        {
            // Act
            var result = await _repository.DeleteInquiryAsync(Guid.NewGuid());

            // Assert
            Assert.False(result.IsSuccess);
            Assert.Equal("Inquiry not found", result.ErrorMessage);
        }

        [Fact]
        public async Task SearchAllPropertiesAsync_ShouldReturnPropertiesMatchingQuery()
        {
            // Arrange
            var properties = new List<PropertyListing>
            {
                new PropertyListing
                {
                    PropertyId = Guid.NewGuid(),
                    Title = "Cozy Apartment",
                    Description = "Great view and affordable.",
                    Price = 100000,
                    Address = "123 Main St",
                    Type = "Apartment",
                    Status = "Available",
                    ImageURLs = "image1.jpg,image2.jpg",
                    SquareFootage = 800,
                    NumberOfBedrooms = 2,
                    NumberOfBathrooms = 1,
                    ListingDate = DateTime.UtcNow,
                    UserID = Guid.NewGuid()
                },
                new PropertyListing
                {
                    PropertyId = Guid.NewGuid(),
                    Title = "Luxury Villa",
                    Description = "Spacious and modern design.",
                    Price = 500000,
                    Address = "456 Elm St",
                    Type = "Villa",
                    Status = "Available",
                    ImageURLs = "image3.jpg,image4.jpg",
                    SquareFootage = 5000,
                    NumberOfBedrooms = 5,
                    NumberOfBathrooms = 4,
                    ListingDate = DateTime.UtcNow,
                    UserID = Guid.NewGuid()
                },
                new PropertyListing
                {
                    PropertyId = Guid.NewGuid(),
                    Title = "Small House",
                    Description = "Affordable living space.",
                    Price = 80000,
                    Address = "789 Oak St",
                    Type = "House",
                    Status = "Available",
                    ImageURLs = "image5.jpg,image6.jpg",
                    SquareFootage = 1200,
                    NumberOfBedrooms = 3,
                    NumberOfBathrooms = 2,
                    ListingDate = DateTime.UtcNow,
                    UserID = Guid.NewGuid()
                }
            };
            await _context.PropertyListings.AddRangeAsync(properties);
            await _context.SaveChangesAsync();

            // Act
            var result = await _repository.SearchAllPropertiesAsync("affordable");

            // Assert
            Assert.True(result.IsSuccess);
            Assert.Equal(1, result.Data.Count());
        }


        [Fact]
        public async Task SearchAllPropertiesAsync_ShouldReturnFailure_WhenQueryIsEmpty()
        {
            // Act
            var result = await _repository.SearchAllPropertiesAsync("");

            // Assert
            Assert.False(result.IsSuccess);
            Assert.Equal("Search query cannot be null or empty.", result.ErrorMessage);
        }
    }
}