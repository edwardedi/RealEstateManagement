using Domain.Entities;
using Infrastructure.Persistance;
using Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Tests.Repositories
{
    public class PropertyListingRepositoryTests
    {
        private readonly ApplicationDbContext _context;
        private readonly PropertyListingRepository _repository;

        public PropertyListingRepositoryTests()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;
            _context = new ApplicationDbContext(options);
            _repository = new PropertyListingRepository(_context);
        }

        [Fact]
        public async Task GetAllListingsAsync_ShouldReturnAllListings()
        {
            // Arrange
            var listings = new List<PropertyListing>
            {
                new PropertyListing
                {
                    PropertyId = Guid.NewGuid(),
                    Title = "Property 1",
                    Address = "123 Main St",
                    Type = "Apartment",
                    Price = 250000,
                    SquareFootage = 1200,
                    NumberOfBedrooms = 2,
                    NumberOfBathrooms = 2,
                    Description = "A beautiful apartment",
                    Status = "Available",
                    ListingDate = DateTime.Now,
                    ImageURLs = "image1.jpg",
                    UserID = Guid.NewGuid()
                },
                new PropertyListing
                {
                    PropertyId = Guid.NewGuid(),
                    Title = "Property 2",
                    Address = "456 Oak St",
                    Type = "House",
                    Price = 350000,
                    SquareFootage = 2000,
                    NumberOfBedrooms = 4,
                    NumberOfBathrooms = 3,
                    Description = "A spacious house",
                    Status = "Sold",
                    ListingDate = DateTime.Now,
                    ImageURLs = "image2.jpg",
                    UserID = Guid.NewGuid()
                }
            };
            await _context.PropertyListings.AddRangeAsync(listings);
            await _context.SaveChangesAsync();

            // Act
            var result = await _repository.GetAllListingsAsync();

            // Assert
            Assert.True(result.IsSuccess);
            Assert.Equal(2, result.Data.Count());
        }

        [Fact]
        public async Task GetListingByIdAsync_ShouldReturnListing_WhenListingExists()
        {
            // Arrange
            var listing = new PropertyListing
            {
                PropertyId = Guid.NewGuid(),
                Title = "Property 1",
                Address = "123 Main St",
                Type = "Apartment",
                Price = 250000,
                SquareFootage = 1200,
                NumberOfBedrooms = 2,
                NumberOfBathrooms = 2,
                Description = "A beautiful apartment",
                Status = "Available",
                ListingDate = DateTime.Now,
                ImageURLs = "image1.jpg",
                UserID = Guid.NewGuid()
            };
            await _context.PropertyListings.AddAsync(listing);
            await _context.SaveChangesAsync();

            // Act
            var result = await _repository.GetListingByIdAsync(listing.PropertyId);

            // Assert
            Assert.True(result.IsSuccess);
            Assert.Equal(listing.PropertyId, result.Data.PropertyId);
        }

        [Fact]
        public async Task GetListingByIdAsync_ShouldReturnFailure_WhenListingDoesNotExist()
        {
            // Act
            var result = await _repository.GetListingByIdAsync(Guid.NewGuid());

            // Assert
            Assert.False(result.IsSuccess);
            Assert.Equal("Property listing not found.", result.ErrorMessage);
        }

        [Fact]
        public async Task GetListingsByUserId_ShouldReturnListings_WhenListingsExistForUser()
        {
            // Arrange
            var userId = Guid.NewGuid();
            var listings = new List<PropertyListing>
            {
                new PropertyListing
                {
                    PropertyId = Guid.NewGuid(),
                    Title = "Property 1",
                    Address = "123 Main St",
                    Type = "Apartment",
                    Price = 250000,
                    SquareFootage = 1200,
                    NumberOfBedrooms = 2,
                    NumberOfBathrooms = 2,
                    Description = "A beautiful apartment",
                    Status = "Available",
                    ListingDate = DateTime.Now,
                    ImageURLs = "image1.jpg",
                    UserID = userId
                },
                new PropertyListing
                {
                    PropertyId = Guid.NewGuid(),
                    Title = "Property 2",
                    Address = "456 Oak St",
                    Type = "House",
                    Price = 350000,
                    SquareFootage = 2000,
                    NumberOfBedrooms = 4,
                    NumberOfBathrooms = 3,
                    Description = "A spacious house",
                    Status = "Sold",
                    ListingDate = DateTime.Now,
                    ImageURLs = "image2.jpg",
                    UserID = userId
                }
            };
            await _context.PropertyListings.AddRangeAsync(listings);
            await _context.SaveChangesAsync();

            // Act
            var result = await _repository.GetListingsByUserId(userId);

            // Assert
            Assert.True(result.IsSuccess);
            Assert.Equal(2, result.Data.Count());
        }

        [Fact]
        public async Task AddListingAsync_ShouldAddListingSuccessfully()
        {
            // Arrange
            var listing = new PropertyListing
            {
                PropertyId = Guid.NewGuid(),
                Title = "New Property",
                Address = "789 Pine St",
                Type = "Condo",
                Price = 180000,
                SquareFootage = 950,
                NumberOfBedrooms = 1,
                NumberOfBathrooms = 1,
                Description = "A cozy condo",
                Status = "Available",
                ListingDate = DateTime.Now,
                ImageURLs = "image3.jpg",
                UserID = Guid.NewGuid()
            };

            // Act
            var result = await _repository.AddListingAsync(listing);

            // Assert
            Assert.True(result.IsSuccess);
            var addedListing = await _context.PropertyListings.FindAsync(result.Data);
            Assert.NotNull(addedListing);
            Assert.Equal(listing.Title, addedListing.Title);
        }

        [Fact]
        public async Task UpdateListingAsync_ShouldUpdateListingSuccessfully()
        {
            // Arrange
            var listing = new PropertyListing
            {
                PropertyId = Guid.NewGuid(),
                Title = "Old Property",
                Address = "1010 Elm St",
                Type = "Townhouse",
                Price = 300000,
                SquareFootage = 1500,
                NumberOfBedrooms = 3,
                NumberOfBathrooms = 2,
                Description = "An old townhouse",
                Status = "Available",
                ListingDate = DateTime.Now,
                ImageURLs = "image4.jpg",
                UserID = Guid.NewGuid()
            };
            await _context.PropertyListings.AddAsync(listing);
            await _context.SaveChangesAsync();

            // Act
            listing.Title = "Updated Property";
            listing.Price = 320000;
            var result = await _repository.UpdateListingAsync(listing);

            // Assert
            Assert.True(result.IsSuccess);
            var updatedListing = await _context.PropertyListings.FindAsync(listing.PropertyId);
            Assert.Equal("Updated Property", updatedListing.Title);
            Assert.Equal(320000, updatedListing.Price);
        }

        [Fact]
        public async Task DeleteListingAsync_ShouldDeleteListingSuccessfully()
        {
            // Arrange
            var listing = new PropertyListing
            {
                PropertyId = Guid.NewGuid(),
                Title = "Property to Delete",
                Address = "123 Maple St",
                Type = "Villa",
                Price = 400000,
                SquareFootage = 2500,
                NumberOfBedrooms = 5,
                NumberOfBathrooms = 4,
                Description = "A beautiful villa",
                Status = "Available",
                ListingDate = DateTime.Now,
                ImageURLs = "image5.jpg",
                UserID = Guid.NewGuid()
            };
            await _context.PropertyListings.AddAsync(listing);
            await _context.SaveChangesAsync();

            // Act
            var result = await _repository.DeleteListingAsync(listing.PropertyId);

            // Assert
            Assert.True(result.IsSuccess);
            var deletedListing = await _context.PropertyListings.FindAsync(listing.PropertyId);
            Assert.Null(deletedListing);
        }

        [Fact]
        public async Task DeleteListingAsync_ShouldReturnFailure_WhenListingDoesNotExist()
        {
            // Act
            var result = await _repository.DeleteListingAsync(Guid.NewGuid());

            // Assert
            Assert.False(result.IsSuccess);
            Assert.Equal("Property Listing not found", result.ErrorMessage);
        }
    }
}