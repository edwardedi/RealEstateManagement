using Domain.Entities;
using FluentAssertions;
using Infrastructure.Persistance;
using Microsoft.AspNetCore.Mvc.Testing;

namespace RealEstateManagement.IntegrationTests
{
    public class PropertyListingsIntegrationTests : IClassFixture<WebApplicationFactory<Program>>, IDisposable
    {
        private readonly WebApplicationFactory<Program> factory;
        private readonly ApplicationDbContext dbContext;
        
        private string BaseUrl = "/api/PropertyListings";

        public PropertyListingsIntegrationTests(WebApplicationFactory<Program> factory)
        {
            dbContext = TestDbContextFactory.CreateInMemoryDbContext();
            this.factory = factory;

        }
        public void Dispose()
        {
            dbContext.Database.EnsureDeleted();
            dbContext.Dispose();
            GC.SuppressFinalize(this);
        }

        [Fact]
        public async Task GivenPropertyListings_WhenGetAllIsCalled_ThenShouldReturnTheRightContentType()
        {
            // Arrange
            var client = factory.CreateClient();
            // Act
            var response = await client.GetAsync(BaseUrl);
            // Assert
            response.EnsureSuccessStatusCode();
            response.Content.Headers.ContentType!.ToString().Should().Be("application/json; charset=utf-8");

        }

        [Fact]
        public async Task GivenPropertyListings_WhenGetAllIsCalled_ThenShouldReturnSeededListings()
        {
            // Arrange
            var client = factory.CreateClient();

            // Seed the data to ensure there's something in the in-memory DB
            SeedTestData();

            // Act
            var response = await client.GetAsync(BaseUrl);
            var content = await response.Content.ReadAsStringAsync();

            // Assert
            response.EnsureSuccessStatusCode(); // Ensure 200 OK
            content.Should().NotBeNullOrEmpty();
            content.Should().Contain("Property 1");
        }
        private void SeedTestData()
        {
            dbContext.PropertyListings.Add(new PropertyListing
            {
                PropertyId = Guid.NewGuid(),
                Title = "Property 1", // Add the expected title here
                Address = "123 Main Street",
                Type = "House",
                Price = 100000,
                SquareFootage = 2000,
                NumberOfBedrooms = 3,
                NumberOfBathrooms = 2,
                Description = "A beautiful house",
                Status = "Available",
                ListingDate = DateTime.Now,
                ImageURLs = "https://www.google.com",
                UserID = Guid.NewGuid()
            });
            dbContext.SaveChanges();
        }

    }
}