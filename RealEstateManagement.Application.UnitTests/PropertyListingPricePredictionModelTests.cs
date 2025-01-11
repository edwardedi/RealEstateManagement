using Application.AIML;
using FluentAssertions;

namespace RealEstateManagement.Application.UnitTests
{
    public class PropertyListingPricePredictionModelTests
    {
        private readonly PropertyListingPricePredictionModel _model;

        public PropertyListingPricePredictionModelTests()
        {
            _model = new PropertyListingPricePredictionModel();
        }

        [Fact]
        public void Train_ShouldTrainModelSuccessfully()
        {
            // Arrange
            var dataPoints = new List<PropertyListingData>
            {
                new PropertyListingData { Label = 100000, Features = new float[] { 3, 1500 } },
                new PropertyListingData { Label = 200000, Features = new float[] { 4, 2000 } }
            };

            // Act
            Action act = () => _model.Train(dataPoints);

            // Assert
            act.Should().NotThrow();
        }

        [Fact]
        public void Predict_ShouldReturnPrediction()
        {
            // Arrange
            var dataPoints = new List<PropertyListingData>
            {
                new PropertyListingData { Label = 100000, Features = new float[] { 3, 1500 } },
                new PropertyListingData { Label = 200000, Features = new float[] { 4, 2000 } }
            };
            _model.Train(dataPoints);

            var testData = new PropertyListingData { Label = 150000, Features = new float[] { 3.5f, 1750 } };

            // Act
            var prediction = _model.Predict(testData);

            // Assert
            prediction.Should().BeGreaterThan(100000).And.BeLessThan(200000);
        }

        [Fact]
        public void Evaluate_ShouldReturnRSquared()
        {
            // Arrange
            var dataPoints = new List<PropertyListingData>
            {
                new PropertyListingData { Label = 100000, Features = new float[] { 3, 1500 } },
                new PropertyListingData { Label = 200000, Features = new float[] { 4, 2000 } }
            };
            _model.Train(dataPoints);

            // Act
            var rSquared = _model.Evaluate(dataPoints);

            // Assert
            rSquared.Should().BeGreaterThan((float)0.8);
        }
    }
}
