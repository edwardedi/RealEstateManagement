using Application.Use_Cases.PropertyListings.Filtering;
using Application.Use_Cases.PropertyListings.Queries;
using Domain.Entities;
using FluentAssertions;

namespace RealEstateManagement.Application.UnitTests
{
    public class TypeFilterStrategyTests
    {
        [Fact]
        public void ApplyFilter_ShouldFilterByType_WhenTypeIsProvided()
        {
            // Arrange
            var query = new GetFilteredPropertyListingsQuery { Type = "Apartment" };
            var propertyListings = new List<PropertyListing>
            {
                new PropertyListing { Type = "Apartment" },
                new PropertyListing { Type = "House" },
                new PropertyListing { Type = "Apartment" }
            }.AsQueryable();

            var strategy = new TypeFilterStrategy();

            // Act
            var result = strategy.ApplyFilter(propertyListings, query);

            // Assert
            result.Should().HaveCount(2);
            result.All(x => x.Type.ToLower() == "apartment").Should().BeTrue();
        }

        [Fact]
        public void ApplyFilter_ShouldNotFilterByType_WhenTypeIsNotProvided()
        {
            // Arrange
            var query = new GetFilteredPropertyListingsQuery { Type = null };
            var propertyListings = new List<PropertyListing>
            {
                new PropertyListing { Type = "Apartment" },
                new PropertyListing { Type = "House" },
                new PropertyListing { Type = "Apartment" }
            }.AsQueryable();

            var strategy = new TypeFilterStrategy();

            // Act
            var result = strategy.ApplyFilter(propertyListings, query);

            // Assert
            result.Should().HaveCount(3);
        }

        [Fact]
        public void ApplyFilter_ShouldBeCaseInsensitive_WhenFilteringByType()
        {
            // Arrange
            var query = new GetFilteredPropertyListingsQuery { Type = "apartment" };
            var propertyListings = new List<PropertyListing>
            {
                new PropertyListing { Type = "Apartment" },
                new PropertyListing { Type = "House" },
                new PropertyListing { Type = "APARTMENT" }
            }.AsQueryable();

            var strategy = new TypeFilterStrategy();

            // Act
            var result = strategy.ApplyFilter(propertyListings, query);

            // Assert
            result.Should().HaveCount(2);
            result.All(x => x.Type.ToLower() == "apartment").Should().BeTrue();
        }
    }
}
