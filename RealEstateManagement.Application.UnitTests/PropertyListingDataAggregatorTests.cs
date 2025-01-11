using Application.AIML;
using FluentAssertions;

namespace RealEstateManagement.Application.UnitTests
{
    public class PropertyListingDataAggregatorTests
    {
        [Fact]
        public void GetPropertyListingData_ShouldReturnTrainingData_WhenTrainingDataIsTrue()
        {
            // Arrange
            var filePath = "data.csv";
            var fileLines = new List<string>
            {
                "100000,3,1500",
                "200000,4,2000",
                "300000,5,2500",
                "400000,6,3000"
            };
            File.WriteAllLines(filePath, fileLines);

            var aggregator = new PropertyListingDataAggregator();

            // Act
            var result = aggregator.GetPropertyListingData(true);

            // Assert
            result.Should().HaveCount(3);
            result[0].Label.Should().Be(100000);
            result[0].Features.Should().Equal(new float[] { 3, 139.3545f });
            result[1].Label.Should().Be(200000);
            result[1].Features.Should().Equal(new float[] { 4, 185.806f });
            result[2].Label.Should().Be(300000);
            result[2].Features.Should().Equal(new float[] { 5, 232.2575f });

            // Cleanup
            File.Delete(filePath);
        }

        [Fact]
        public void GetPropertyListingData_ShouldReturnTestData_WhenTrainingDataIsFalse()
        {
            // Arrange
            var filePath = "data.csv";
            var fileLines = new List<string>
            {
                "100000,3,1500",
                "200000,4,2000",
                "300000,5,2500",
                "400000,6,3000"
            };
            File.WriteAllLines(filePath, fileLines);

            var aggregator = new PropertyListingDataAggregator();

            // Act
            var result = aggregator.GetPropertyListingData(false);

            // Assert
            result.Should().HaveCount(1);
            result[0].Label.Should().Be(400000);
            result[0].Features.Should().Equal(new float[] { 6, 278.709f });

            // Cleanup
            File.Delete(filePath);
        }

        [Fact]
        public void GetPropertyListingDataCount_ShouldReturnCorrectCount()
        {
            // Arrange
            var filePath = "data.csv";
            var fileLines = new List<string>
            {
                "100000,3,1500",
                "200000,4,2000",
                "300000,5,2500",
                "400000,6,3000"
            };
            File.WriteAllLines(filePath, fileLines);

            var aggregator = new PropertyListingDataAggregator();
            aggregator.GetPropertyListingData(true);

            // Act
            var count = aggregator.GetPropertyListingDataCount();

            // Assert
            count.Should().Be(3);

            // Cleanup
            File.Delete(filePath);
        }
    }
}
