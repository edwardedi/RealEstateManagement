using Application.Use_Cases.PropertyListings.Filtering;
using Application.Use_Cases.PropertyListings.Queries;
using Domain.Entities;
using FluentAssertions;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace RealEstateManagement.Application.UnitTests
{
    public class TypeFilterStrategyTests
    {
        [Fact]
        public void ApplyFilter_ShouldFilterByType_WhenTypeIsProvided()
        {
            // Arrange
            var listings = new List<PropertyListing>
            {
                new PropertyListing { Type = "House" },
                new PropertyListing { Type = "Apartment" },
                new PropertyListing { Type = "House" }
            }.AsQueryable();

            var query = new GetFilteredPropertyListingsQuery { Type = "House" };
            var strategy = new TypeFilterStrategy();

            // Act
            var result = strategy.ApplyFilter(listings, query);

            // Assert
            result.Should().HaveCount(2);
            result.All(x => x.Type == "House").Should().BeTrue();
        }

        [Fact]
        public void ApplyFilter_ShouldNotFilterByType_WhenTypeIsNotProvided()
        {
            // Arrange
            var listings = new List<PropertyListing>
            {
                new PropertyListing { Type = "House" },
                new PropertyListing { Type = "Apartment" },
                new PropertyListing { Type = "House" }
            }.AsQueryable();

            var query = new GetFilteredPropertyListingsQuery { Type = null };
            var strategy = new TypeFilterStrategy();

            // Act
            var result = strategy.ApplyFilter(listings, query);

            // Assert
            result.Should().HaveCount(3);
        }
    }

    public class PriceFilterStrategyTests
    {
        [Fact]
        public void ApplyFilter_ShouldFilterByPrice_WhenPriceIsProvided()
        {
            // Arrange
            var listings = new List<PropertyListing>
            {
                new PropertyListing { Price = 100000 },
                new PropertyListing { Price = 200000 },
                new PropertyListing { Price = 100000 }
            }.AsQueryable();

            var query = new GetFilteredPropertyListingsQuery { Price = 100000 };
            var strategy = new PriceFilterStrategy();

            // Act
            var result = strategy.ApplyFilter(listings, query);

            // Assert
            result.Should().HaveCount(2);
            result.All(x => x.Price == 100000).Should().BeTrue();
        }

        [Fact]
        public void ApplyFilter_ShouldNotFilterByPrice_WhenPriceIsNotProvided()
        {
            // Arrange
            var listings = new List<PropertyListing>
            {
                new PropertyListing { Price = 100000 },
                new PropertyListing { Price = 200000 },
                new PropertyListing { Price = 100000 }
            }.AsQueryable();

            var query = new GetFilteredPropertyListingsQuery { Price = 0 };
            var strategy = new PriceFilterStrategy();

            // Act
            var result = strategy.ApplyFilter(listings, query);

            // Assert
            result.Should().HaveCount(3);
        }
    }

    public class SquareFootageFilterStrategyTests
    {
        [Fact]
        public void ApplyFilter_ShouldFilterBySquareFootage_WhenSquareFootageIsProvided()
        {
            // Arrange
            var listings = new List<PropertyListing>
            {
                new PropertyListing { SquareFootage = 1500 },
                new PropertyListing { SquareFootage = 2000 },
                new PropertyListing { SquareFootage = 1500 }
            }.AsQueryable();

            var query = new GetFilteredPropertyListingsQuery { SquareFootage = 1500 };
            var strategy = new SquareFootageFilterStrategy();

            // Act
            var result = strategy.ApplyFilter(listings, query);

            // Assert
            result.Should().HaveCount(2);
            result.All(x => x.SquareFootage == 1500).Should().BeTrue();
        }

        [Fact]
        public void ApplyFilter_ShouldNotFilterBySquareFootage_WhenSquareFootageIsNotProvided()
        {
            // Arrange
            var listings = new List<PropertyListing>
            {
                new PropertyListing { SquareFootage = 1500 },
                new PropertyListing { SquareFootage = 2000 },
                new PropertyListing { SquareFootage = 1500 }
            }.AsQueryable();

            var query = new GetFilteredPropertyListingsQuery { SquareFootage = 0 };
            var strategy = new SquareFootageFilterStrategy();

            // Act
            var result = strategy.ApplyFilter(listings, query);

            // Assert
            result.Should().HaveCount(3);
        }
    }

    public class NumberOfBedroomsFilterStrategyTests
    {
        [Fact]
        public void ApplyFilter_ShouldFilterByNumberOfBedrooms_WhenNumberOfBedroomsIsProvided()
        {
            // Arrange
            var listings = new List<PropertyListing>
            {
                new PropertyListing { NumberOfBedrooms = 3 },
                new PropertyListing { NumberOfBedrooms = 4 },
                new PropertyListing { NumberOfBedrooms = 3 }
            }.AsQueryable();

            var query = new GetFilteredPropertyListingsQuery { NumberOfBedrooms = 3 };
            var strategy = new NumberOfBedroomsFilterStrategy();

            // Act
            var result = strategy.ApplyFilter(listings, query);

            // Assert
            result.Should().HaveCount(2);
            result.All(x => x.NumberOfBedrooms == 3).Should().BeTrue();
        }

        [Fact]
        public void ApplyFilter_ShouldNotFilterByNumberOfBedrooms_WhenNumberOfBedroomsIsNotProvided()
        {
            // Arrange
            var listings = new List<PropertyListing>
            {
                new PropertyListing { NumberOfBedrooms = 3 },
                new PropertyListing { NumberOfBedrooms = 4 },
                new PropertyListing { NumberOfBedrooms = 3 }
            }.AsQueryable();

            var query = new GetFilteredPropertyListingsQuery { NumberOfBedrooms = 0 };
            var strategy = new NumberOfBedroomsFilterStrategy();

            // Act
            var result = strategy.ApplyFilter(listings, query);

            // Assert
            result.Should().HaveCount(3);
        }
    }

    public class NumberOfBathroomsFilterStrategyTests
    {
        [Fact]
        public void ApplyFilter_ShouldFilterByNumberOfBathrooms_WhenNumberOfBathroomsIsProvided()
        {
            // Arrange
            var listings = new List<PropertyListing>
            {
                new PropertyListing { NumberOfBathrooms = 2 },
                new PropertyListing { NumberOfBathrooms = 3 },
                new PropertyListing { NumberOfBathrooms = 2 }
            }.AsQueryable();

            var query = new GetFilteredPropertyListingsQuery { NumberOfBathrooms = 2 };
            var strategy = new NumberOfBathroomsFilterStrategy();

            // Act
            var result = strategy.ApplyFilter(listings, query);

            // Assert
            result.Should().HaveCount(2);
            result.All(x => x.NumberOfBathrooms == 2).Should().BeTrue();
        }

        [Fact]
        public void ApplyFilter_ShouldNotFilterByNumberOfBathrooms_WhenNumberOfBathroomsIsNotProvided()
        {
            // Arrange
            var listings = new List<PropertyListing>
            {
                new PropertyListing { NumberOfBathrooms = 2 },
                new PropertyListing { NumberOfBathrooms = 3 },
                new PropertyListing { NumberOfBathrooms = 2 }
            }.AsQueryable();

            var query = new GetFilteredPropertyListingsQuery { NumberOfBathrooms = 0 };
            var strategy = new NumberOfBathroomsFilterStrategy();

            // Act
            var result = strategy.ApplyFilter(listings, query);

            // Assert
            result.Should().HaveCount(3);
        }
    }

    public class StatusFilterStrategyTests
    {
        [Fact]
        public void ApplyFilter_ShouldFilterByStatus_WhenStatusIsProvided()
        {
            // Arrange
            var listings = new List<PropertyListing>
            {
                new PropertyListing { Status = "Available" },
                new PropertyListing { Status = "Sold" },
                new PropertyListing { Status = "Available" }
            }.AsQueryable();

            var query = new GetFilteredPropertyListingsQuery { Status = "Available" };
            var strategy = new StatusFilterStrategy();

            // Act
            var result = strategy.ApplyFilter(listings, query);

            // Assert
            result.Should().HaveCount(2);
            result.All(x => x.Status == "Available").Should().BeTrue();
        }

        [Fact]
        public void ApplyFilter_ShouldNotFilterByStatus_WhenStatusIsNotProvided()
        {
            // Arrange
            var listings = new List<PropertyListing>
            {
                new PropertyListing { Status = "Available" },
                new PropertyListing { Status = "Sold" },
                new PropertyListing { Status = "Available" }
            }.AsQueryable();

            var query = new GetFilteredPropertyListingsQuery { Status = null };
            var strategy = new StatusFilterStrategy();

            // Act
            var result = strategy.ApplyFilter(listings, query);

            // Assert
            result.Should().HaveCount(3);
        }
    }
}

